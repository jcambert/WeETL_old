using System;
using System.Threading.Tasks;

namespace WeETL
{
    public interface IStartable
    {
        Task Start();

        void Stop();

        IObservable<IStartable> OnStart { get; }

        IObservable<IStartable> OnCompleted { get; }

        TimeSpan TimeElapsed { get; }

        bool IsCancellationRequested { get; }
    }
}
