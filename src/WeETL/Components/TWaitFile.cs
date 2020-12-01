using Microsoft.Extensions.Configuration;
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
    public class TWaitFile : WaitFile, IStartable, IETLCoreComponent
    {
        
        public TWaitFile(IConfiguration configuration) : base(configuration)
        {
            
        }

        public bool IsCancellationRequested => Token.IsCancellationRequested;

        public bool IsCompleted => false;

        public bool IsRunning => FileWatcher.EnableRaisingEvents;
        public string Name { get; internal set; }
    }
}
