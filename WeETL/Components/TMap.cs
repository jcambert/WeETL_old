using System;
using System.Collections.Generic;
using System.Text;

namespace WeETL
{
    public enum TMapJoin
    {
        Left,
        Inner
    }
    public class TMap<TInputSchema, TOutputSchema, TLookupSchema> : ETLComponent<TInputSchema, TOutputSchema>
        where TInputSchema : class, new()
        where TOutputSchema : class, new()
        where TLookupSchema : class, new()
    {
        private TMapJoin _join;
        private Func<TInputSchema, TLookupSchema, bool> _joinFunction;
        private IDisposable _lookupDisposable;
        public bool AcceptLookup => _lookupDisposable != null;

        private List<TLookupSchema> _lookupRow = new List<TLookupSchema>();
        public bool SetLookup(IObservable< TLookupSchema> lookup)
        {
            if (!AcceptLookup)
            {
                Error.OnNext(new ConnectorException($"a Lookup Schema has already been connected"));
                return false;
            }
            _lookupDisposable = lookup.Subscribe(row =>
            {
                _lookupRow.Add(row);
            },
            InternalOnException,
            InternalOnCompleted);
            return true;
        }
        public void Join(TMapJoin join, Func<TInputSchema,TLookupSchema, bool> fnc)
        {
            this._join = join;
            this._joinFunction = fnc;
        }
        protected override void InternalOnRowBeforeTransform(int index, TInputSchema row)
        {
            base.InternalOnRowBeforeTransform(index, row);
        }
    }
}
