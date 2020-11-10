using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using WeETL.Core;

namespace WeETL
{
    
    public enum TLogRowMode
    {
        Basic,
        Table,
        Vertical
    }
    public class TLogRow<TSchema>:ETLComponent<TSchema,TSchema> 
        where TSchema : class, new()
    {
        private bool _showHeader;
        private bool _headerShown;
        private TLogRowMode _mode=TLogRowMode.Basic;
        private ILogger<TLogRow<TSchema>> _logger;
        private List< string[]> _buffer = new List< string[]>();
        private int[] _widthes ;
        private readonly List<PropertyInfo> _properties;
        private const string SEPARATOR = "|";
        private readonly int _numberOfColumn;
        public TLogRow(ILogger<TLogRow<TSchema>> logger)
        {
            Contract.Requires(logger != null, "logger cannot be null. Use the DI ");
            this._logger = logger;

            _properties = typeof(TSchema).GetProperties().Where(p => p.GetCustomAttribute<LogIgnoreAttribute>() == null).OrderBy(p=>p.Name).ToList();
            _numberOfColumn = _properties.Count();
            _widthes = new int[_numberOfColumn];
            var row = new List<string>();

            foreach (var (item,index) in _properties.WithIndex())
            {
                var value = item.GetCustomAttribute<LogHeaderAttribute>()?.Title ?? item.Name;
                row.Add(value);
                _widthes[index] = Math.Max(_widthes[index], value.Length);
            }
            _buffer.Add(row.ToArray());
        }
        public string Separator { get; set; } = SEPARATOR;
        public bool ShowHeader { get; set; } = true;
        public LogLevel Level { get; set; } = LogLevel.Critical;

        protected override void InternalOnInputAfterTransform(int index, TSchema row)
        {
            base.InternalOnInputAfterTransform(index, row);
            if (!Enabled) return;
            var rowLog = new List<string>();
            foreach (var (prop,i) in _properties.WithIndex())
            {
                var value = prop.GetGetMethod().Invoke(row, null).ToString();
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
            using (_logger.BeginScope("TLogRow"))
            {
                foreach (var item in _buffer)
                {
                    foreach (var (value, index) in item.WithIndex())
                    {
                        item[index] = value.PadRight(_widthes[index]);
                    }
                    var s = string.Join(Separator, item);
                    _logger.LogInformation(s);
                }
            }
        }

        protected virtual void InternalShowHeader()
        {
            if (!_showHeader || _headerShown) return;
            _headerShown = true;
            Console.WriteLine("HEADER");
        }

        public void Mode(TLogRowMode mode)
        {
            this._mode = mode;
        }
    }
}
