using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using WeETL.Core;

namespace WeETL
{
    public class TForEach<TInputSchema, TOutputSchema>
        where TOutputSchema : class, new()
    {
        ISubject<(TOutputSchema, int)> _output = new Subject<(TOutputSchema, int)>();
        public void AddIteration(Job job, IObservable<TInputSchema> input, Func<TInputSchema, IEnumerable<(TOutputSchema item, int index)>> p) { 
                
            
            input.Subscribe(s=> {
                var iter=p(s);
                foreach (var (item,index) in iter.WithIndex())
                {
                    _output.OnNext(item);
                }
                _output.OnCompleted();
            });
        }
        public IObservable<(TOutputSchema, int)> OnOutput => _output.AsObservable();

    }
}
