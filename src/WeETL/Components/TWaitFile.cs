using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Core;
using WeETL.Observables;

namespace WeETL.Components
{
    public class TWaitFile : WaitFile, IStartable
    {
        public TWaitFile(WaitFileOptions options, CancellationTokenSource cts = null) : base(options, cts)
        {
        }

        public bool IsCancellationRequested => throw new NotImplementedException();

        public bool IsCompleted => throw new NotImplementedException();

        public bool IsRunning => throw new NotImplementedException();
    }
}
