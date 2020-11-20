using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using WeETL.Core;

namespace WeETL
{
    public enum SortOrder
    {
        Ascending,
        Descending
    }
    public class TSortRow<TInputSchema, TOutputSchema> : ETLComponent<TInputSchema, TOutputSchema>
        where TInputSchema : class
        where TOutputSchema : class, new()
    {
        List<TOutputSchema> _buffer = new List<TOutputSchema>();
        protected internal readonly List< PopulateOrdering< TOutputSchema>> Orderings =new List<PopulateOrdering<TOutputSchema>>();
        public TSortRow()
        {
            DeferSendingOutput = true;
        }
        protected override void InternalOnInputAfterTransform(int index, TOutputSchema row)
        {
            base.InternalOnInputAfterTransform(index, row);
            _buffer.Add(row);

           
        }

        public void AddOrderBy(Func<TOutputSchema, object > fn,SortOrder mode=SortOrder.Ascending) {

            Orderings.Add(new PopulateOrdering<TOutputSchema>() { Order = fn, SortOrder=mode });
            
        }
        protected override void InternalOnInputCompleted()
        {
            Orderings.ForEach(sortRule =>
            {
                _buffer=_buffer.Sorting(sortRule.Order, sortRule.SortOrder);

            });
            _buffer.ForEach(row => { 
                OutputHandler.OnNext(row); 
            });
            base.InternalOnInputCompleted();
            
        }

        

    }
}
