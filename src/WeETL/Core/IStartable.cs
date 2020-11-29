using System;
using System.Threading.Tasks;

namespace WeETL.Core
{
    public interface IStartable
    {
        Task Start();

        void Stop();


        bool IsCancellationRequested { get; }

        bool IsCompleted { get; }
        bool IsRunning { get; }
    }
}
