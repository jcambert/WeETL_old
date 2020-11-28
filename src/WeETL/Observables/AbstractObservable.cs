using System;
using System.Diagnostics;
using System.Threading;
using WeETL.Utilities;

namespace WeETL.Observables
{
    public abstract class AbstractObservable<T> : IObservable<T>
    {
        
      

        public AbstractObservable(CancellationTokenSource cts=null)
        {
            TokenSource = cts ?? new CancellationTokenSource();
            Output = CreateOutputObservable();
            Check.NotNull(Output,nameof(Output));
        }

        protected abstract IObservable<T> CreateOutputObservable();

        [DebuggerStepThrough]
        public IDisposable Subscribe(IObserver<T> observer) => Output.Subscribe(observer);

        public IObservable<T> Output { get; protected set; }
        public CancellationTokenSource TokenSource { get; private set; }
    }
}
