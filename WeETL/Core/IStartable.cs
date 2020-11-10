using System;
using System.Threading.Tasks;

namespace WeETL.Core
{
    public interface IStartable
    {
        Task Start();

        void Stop();

        

        TimeSpan TimeElapsed { get; }

        bool IsCancellationRequested { get; }
    }
}
