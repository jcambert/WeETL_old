using System;
using System.Threading;
using System.Threading.Tasks;

namespace WeETL.Core
{
    public interface IStartable
    {
        Task Start(CancellationToken cts);

        // Stop();


        //bool IsCancellationRequested { get; }

        bool IsCompleted { get; }
        bool IsRunning { get; }
    }
}
