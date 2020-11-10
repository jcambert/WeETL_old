using System;

namespace WeETL.Core
{
    public interface  IETLWatchableComponent<T>
    {
        public IObservable<T> OnStart { get; }

        public IObservable<T> OnCompleted { get; }

    }
}
