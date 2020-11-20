using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using WeETL.Core;

namespace WeETL
{

    public enum TLogRowMode
    {
        Basic,
        Table
    }
    public enum TLogRowAlignment
    {
        Left,
        Center,
        Right
    }
    public class TLogRow<TSchema> : ETLComponent<TSchema, TSchema>
        where TSchema : class, new()
    {

        private ILogger<TLogRow<TSchema>> _logger;
        private List<string[]> _buffer = new List<string[]>();
        private int[] _widthes;

        private readonly List<PropertyInfo> _properties;
        private const char ROW_SEPARATOR = '|';
        private const char COL_SEPARATOR = '+';
        private const char LINE_SEPARATOR = '-';
        private readonly int _numberOfColumn;
        private static Func<string, int, string> padright = (s, l) => s.PadRight(l);
        private static Func<string, int, string> padleft = (s, l) => s.PadLeft(l);
        private static Func<string, int, string> padboth = (s, l) => s.PadBoth(l);
        public TLogRow(ILogger<TLogRow<TSchema>> logger, IConfiguration cfg, ETLContext ctx)
        {
            Contract.Requires(logger != null, "logger cannot be null. Use the DI ");
            Contract.Requires(cfg != null, "configuration cannot be null. Use the DI ");
            Contract.Requires(ctx != null, "context cannot be null. Use the DI ");
            this._logger = logger;

            _properties = typeof(TSchema).GetProperties().Where(p => p.GetCustomAttribute<LogIgnoreAttribute>() == null).OrderBy(p => p.Name).ToList();
            _numberOfColumn = _properties.Count();
            _widthes = new int[_numberOfColumn];
            var row = new List<string>();

            foreach (var (item, index) in _properties.WithIndex())
            {
                var value = item.GetCustomAttribute<LogHeaderAttribute>()?.Title ?? item.Name;
                row.Add(value);
                _widthes[index] = Math.Max(_widthes[index], value.Length);
            }
            _buffer.Add(row.ToArray());
        }
        public char RowSeparator { get; set; } = ROW_SEPARATOR;
        public char ColumnSeparator { get; set; } = COL_SEPARATOR;
        public char LineSeparator { get; set; } = LINE_SEPARATOR;
        public int AdditionalSpace { get; set; } = 0;
        public TLogRowAlignment Alignment { get; set; } = TLogRowAlignment.Left;
        public TLogRowMode Mode { get; set; } = TLogRowMode.Basic;
        public bool ShowHeader { get; set; } = true;
        public bool ShowItemNumber { get; set; } = false;

        public LogLevel Level { get; set; } = LogLevel.Critical;

        protected override void InternalOnInputAfterTransform(int index, TSchema row)
        {
            base.InternalOnInputAfterTransform(index, row);
            if (!Enabled) return;

            var rowLog = new List<string>();
            foreach (var (prop, i) in _properties.WithIndex())
            {

                var value = prop.GetGetMethod().Invoke(row, null)?.ToString() ?? string.Empty;
                rowLog.Add(value);
                _widthes[i] = Math.Max(_widthes[i], value.Length);
            }
            _buffer.Add(rowLog.ToArray());
        }

        protected override void InternalOnException(Exception e)
        {
            base.InternalOnException(e);
            Console.Error.WriteLine(e.Message);
        }

        protected override void InternalOnInputCompleted()
        {
            base.InternalOnInputCompleted();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            string rowSeparator = ColumnSeparator.ToString();
            int nbreColWidth = _buffer.Count.ToString().Length;
            Func<string, int, string> pad = Alignment switch
            {
                TLogRowAlignment.Left => padright,
                TLogRowAlignment.Right => padleft,
                _ => padboth
            };
            Func<string[], string> row = Mode switch
            {

                TLogRowMode.Basic => (s => string.Join(RowSeparator, s)),
                _ => (s =>
                {
                    return RowSeparator + string.Join(RowSeparator, s) + RowSeparator + "\n" + rowSeparator;
                }),
            };

            if (ShowItemNumber)
            {
                rowSeparator += new string(LineSeparator, nbreColWidth + (AdditionalSpace > 0 ? AdditionalSpace : 0)) + ColumnSeparator;
            }
            foreach (var (width, index) in _widthes.WithIndex())
            {
                rowSeparator += new string(LineSeparator, width + (AdditionalSpace > 0 ? AdditionalSpace : 0)) + ColumnSeparator;
            }
            if (!ShowHeader && Mode == TLogRowMode.Table)
                sb.AppendLine(rowSeparator);
            using (_logger.BeginScope("TLogRow"))
            {
                foreach (var (items, col) in _buffer.WithIndex())
                {
                    if (!ShowHeader && col == 0) continue;
                    if (ShowHeader && col == 0 && Mode == TLogRowMode.Table) sb.AppendLine(rowSeparator);

                    foreach (var (value, index) in items.WithIndex())
                    {
                        items[index] = pad(value, _widthes[index] + (AdditionalSpace > 0 ? AdditionalSpace : 0));
                    }
                    if (ShowItemNumber)
                    {
                        sb.Append(RowSeparator + pad(col == 0 ? "#" : col.ToString(), nbreColWidth + (AdditionalSpace > 0 ? AdditionalSpace : 0)));
                        if (Mode == TLogRowMode.Basic) sb.Append(RowSeparator);
                    }
                    sb.AppendLine(row(items));
                }
                _logger.LogInformation(sb.ToString());
            }
        }

        protected virtual void InternalShowHeader()
        {

        }


    }
}
