using System;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
#if DEBUG
using System.Diagnostics;
#endif
namespace WeETL.Observables
{
    public class WaitFile : AbstractObservable<EventPattern<FileSystemEventArgs>>, IDisposable
    {
        
        private int _stopOn = 0, _stopOnCounter = 0;
        private readonly FileSystemWatcher _fileWatcher = new FileSystemWatcher();

        private bool disposedValue;

        public WaitFile(WaitFileOptions options, CancellationTokenSource cts = null) : base(cts)
        {
            this.Changed = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                evt => _fileWatcher.Changed += evt,
                evt => _fileWatcher.Changed -= evt);
            this.Created = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                evt => _fileWatcher.Created += evt,
                evt => _fileWatcher.Created -= evt);
            this.Deleted = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                evt => _fileWatcher.Deleted += evt,
                evt => _fileWatcher.Deleted -= evt);
            this.Renamed = Observable.FromEventPattern<RenamedEventHandler, FileSystemEventArgs>(
                evt => _fileWatcher.Renamed += evt,
                evt => _fileWatcher.Renamed -= evt);

            NotifyFilter = options.NotifyFilters;
            Path = options.Path;
            Filter = options.Filter;
            IncludeSubDirectories = options.IncludeSubDirectories;
            StopOn = options.StopOn;
        }



        public IObservable<EventPattern<FileSystemEventArgs>> Changed { get; private set; }
        public IObservable<EventPattern<FileSystemEventArgs>> Created { get; private set; }
        public IObservable<EventPattern<FileSystemEventArgs>> Deleted { get; private set; }
        public IObservable<EventPattern<FileSystemEventArgs>> Renamed { get; private set; }

        public NotifyFilters NotifyFilter { get => _fileWatcher.NotifyFilter; set { _fileWatcher.NotifyFilter = value; } }
        public string Path { get => _fileWatcher.Path; set { _fileWatcher.Path = value; } }
        public string Filter { get => _fileWatcher.Filter; set { _fileWatcher.Filter = value; } }
        public bool IncludeSubDirectories { get => _fileWatcher.IncludeSubdirectories; set { _fileWatcher.IncludeSubdirectories = value; } }
        public bool StopOnFirst { get => StopOn == 1; set { StopOn = value ? 1 : 0; } }
        public int StopOn { get => _stopOn; set { _stopOn = value; } }

        protected override IObservable<EventPattern<FileSystemEventArgs>> CreateOutputObservable()
        => Observable.Defer(() =>
        {
#if DEBUG
            Debug.WriteLine($"Constructing {nameof(WaitFile)} stream");
#endif

            TokenSource.Token.ThrowIfCancellationRequested();

            _fileWatcher.EnableRaisingEvents = true;

            return Created
            .Merge(Changed)
            .Merge(Deleted)
            .Merge(Renamed)
            .Throttle(TimeSpan.FromMilliseconds(200))
            .Do(x =>
            {
                ++_stopOnCounter;
#if DEBUG
                Debug.WriteLine($"{nameof(WaitFile)} has new changed {_stopOnCounter}");
#endif
            })
            
            .TakeWhile(x =>
            {
                return StopOn == 0 || _stopOnCounter <= StopOn;
            });

        });

        public void Stop()
        {
#if DEBUG
            Debug.WriteLine($"{nameof(WaitFile)} is stopping");
#endif
            _fileWatcher.EnableRaisingEvents = false;
        }

        public Task Start()
        {
#if DEBUG
            Debug.WriteLine($"{nameof(WaitFile)} is starting");
#endif
            _fileWatcher.EnableRaisingEvents = true;
            return Task.CompletedTask;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés)

                }

                // TODO: libérer les ressources non managées (objets non managés) et substituer le finaliseur
                // TODO: affecter aux grands champs une valeur null
                disposedValue = true;
            }
        }

        // // TODO: substituer le finaliseur uniquement si 'Dispose(bool disposing)' a du code pour libérer les ressources non managées
        // ~WaitFile()
        // {
        //     // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
