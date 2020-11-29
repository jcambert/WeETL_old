using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Core;
using WeETL.Observables;
using WeETL.Schemas;

namespace WeETL.Components
{/*
    internal struct WaitForFileCounter
    {
        internal int Changed, Created, Deleted, Renamed;
        internal WaitForFileCounter(int init)
        {
            Changed = Created = Deleted = Renamed = init;
        }
    }*/
    public class TWaitForFile : ETLStartableComponent<FileSystemEventArgs, FileSystemEventSchema>
    {
        #region private vars
        public const string TWAIT_FOR_FILE_SECTION = nameof(TWaitForFile);
        private readonly FileSystemWatcher _fileWatcher = new FileSystemWatcher();
        
        #endregion
        #region ctor
        public TWaitForFile(IConfiguration cfg) : base()
        {
            Options = new WaitFileOptions();
            cfg.GetSection(TWAIT_FOR_FILE_SECTION).Bind(Options);
            
        }
        #endregion
        #region public properties
      
        public WaitFileOptions Options { get; }

        #endregion

        #region public methods
        protected override Task InternalStart(CancellationTokenSource token)
        {
            bool completed = false;
            WaitFile wf = new WaitFile(Options,token);
            System.ObservableExtensions.Subscribe(wf.Output.Select(x => new FileSystemEventSchema(x.EventArgs)),row=> { }, () => { completed = true; });

            return Task.CompletedTask;
        }



        #endregion
    
        protected override void InternalDispose()
        {
            base.InternalDispose();

        }

    }
}
