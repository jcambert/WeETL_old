using System;

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

        public TLogRow()
        {
        }
        public TLogRow<TSchema> ShowHeader(bool show)
        {
            this._showHeader = show;
            return this;
        }

        protected override void InternalOnInputAfterTransform(int index, TSchema row)
        {
            base.InternalOnInputAfterTransform(index, row);
            ShowHeader();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{index.ToString().PadRight(3)} |{row}");
            Console.ResetColor();
        }

        protected override void InternalOnException(Exception e)
        {
            base.InternalOnException(e);
            Console.Error.WriteLine(e.Message);
        }


        protected virtual void ShowHeader()
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
