using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Schemas;

namespace WeETL.Components
{
    public class TWaitForFile : ETLStartableComponent<FileSystemEventArgs, FileSystemEventSchema>
    {
        #region private vars
       
        private readonly FileSystemWatcher _fileWatcher = new FileSystemWatcher();
        private int _stopOn = 0, _stopOnCounter = 0;
        private IDisposable _changed;
        private IDisposable _created;
        private IDisposable _deleted;
        private IDisposable _renamed;

        #endregion
        #region ctor
        public TWaitForFile():base()
        {
            //this.Changed = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(_fileWatcher, "Changed");
            this.Changed = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                evt => _fileWatcher.Changed += evt,
                evt => _fileWatcher.Changed -= evt);
            this.Created = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                evt => _fileWatcher.Created += evt,
                evt => _fileWatcher.Created -= evt);
            this.Deleted= Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                evt => _fileWatcher.Deleted += evt,
                evt => _fileWatcher.Deleted -= evt);
            this.Renamed= Observable.FromEventPattern<RenamedEventHandler, FileSystemEventArgs>(
                evt => _fileWatcher.Renamed += evt,
                evt => _fileWatcher.Renamed -= evt);

        }
        #endregion
        #region public properties
        public bool StopOnFirst { get => StopOn == 1; set { StopOn = value ? 1 : 0; } }

        public int StopOn { get => _stopOn; set { Contract.Requires(value >= 0, "The StopOn value must be greater or equal to 0"); _stopOn = value; } }
        public NotifyFilters NotifyFilters { get; set; } = NotifyFilters.FileName;
        public string Path { get; set; } = Assembly.GetEntryAssembly().Location;
        public string Filter { get; set; } = "*.*";
        public IObservable<EventPattern<FileSystemEventArgs>> Changed { get; private set; }
        public IObservable<EventPattern<FileSystemEventArgs>> Created { get; private set; }
        public IObservable<EventPattern<FileSystemEventArgs>> Deleted { get; private set; }
        public IObservable<EventPattern<FileSystemEventArgs>> Renamed { get; private set; }

        #endregion

        #region public methods
        protected override void InternalStart()
        {

           
                  _fileWatcher.NotifyFilter = NotifyFilters;
                  _fileWatcher.Path = Path;
                  _fileWatcher.Filter = Filter;

                  this._changed = this.Changed.Subscribe(e =>
                    {
                      
                            var transformed = InternalInputTransform(e.EventArgs);
                            InternalSendOutput(transformed);

                        

                    });
                  this._created = this.Created.Subscribe(e =>
                    {
                        
                            var transformed = InternalInputTransform(e.EventArgs);
                            InternalSendOutput(transformed);
                        

                    });
                  this._deleted = this.Deleted.Subscribe(e =>
                    {
                        
                            var transformed = InternalInputTransform(e.EventArgs);
                            InternalSendOutput(transformed);
                        

                    });
                  this._renamed = this.Renamed.Subscribe(e =>
                   {
                      
                           var transformed = InternalInputTransform(e.EventArgs);
                           InternalSendOutput(transformed);
                       

                   });


                  _fileWatcher.EnableRaisingEvents = true;

             
        }



        #endregion
        protected override void InternalSendOutput(FileSystemEventSchema row)
        {
            base.InternalSendOutput(row);
            _stopOnCounter++;
            if (_stopOnCounter >= _stopOn) Stop();
        }
        protected override void InternalDispose()
        {
            base.InternalDispose();
            this._changed?.Dispose();
            this._created?.Dispose();
            this._deleted?.Dispose();
            this._renamed?.Dispose();

        }

    }
}
