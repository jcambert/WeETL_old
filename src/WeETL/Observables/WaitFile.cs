using System;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
#if DEBUG
using System.Diagnostics;
#endif
namespace WeETL.Observables
{
    public class WaitFile : AbstractObservable<EventPattern<FileSystemEventArgs>>, IDisposable
    {
        public const string WAIT_FILE_SECTION = nameof(WaitFile);
        private int _stopOn = 0, _stopOnCounter = 0;
        protected readonly FileSystemWatcher FileWatcher = new FileSystemWatcher();

        private bool disposedValue;

        public WaitFile(IConfiguration configuration)
        {
            WaitFileOptions options = new WaitFileOptions();
            configuration.GetSection(WAIT_FILE_SECTION).Bind(options);
            SetOptions(options );
            Initialize();
        }
        public WaitFile(WaitFileOptions options) : base()
        {
/*
            NotifyFilter = options.NotifyFilters;
            Path = options.Path;
            Filter = options.Filter;
            IncludeSubDirectories = options.IncludeSubDirectories;
            StopOn = options.StopOn;*/
            SetOptions(options);
            Initialize();
        }
        protected virtual void Initialize()
        {
            this.Changed = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                evt => FileWatcher.Changed += evt,
                evt => FileWatcher.Changed -= evt);
            this.Created = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                evt => FileWatcher.Created += evt,
                evt => FileWatcher.Created -= evt);
            this.Deleted = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                evt => FileWatcher.Deleted += evt,
                evt => FileWatcher.Deleted -= evt);
            this.Renamed = Observable.FromEventPattern<RenamedEventHandler, FileSystemEventArgs>(
                evt => FileWatcher.Renamed += evt,
                evt => FileWatcher.Renamed -= evt);

        }

        private void SetOptions(WaitFileOptions options)
        {
            options = options ?? new WaitFileOptions();
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

        public NotifyFilters NotifyFilter { get => FileWatcher.NotifyFilter; set { FileWatcher.NotifyFilter = value; } }
        public string Path { get => FileWatcher.Path; set { FileWatcher.Path = value; } }
        public string Filter { get => FileWatcher.Filter; set { FileWatcher.Filter = value; } }
        public bool IncludeSubDirectories { get => FileWatcher.IncludeSubdirectories; set { FileWatcher.IncludeSubdirectories = value; } }
        public bool StopOnFirst { get => StopOn == 1; set { StopOn = value ? 1 : 0; } }
        public int StopOn { get => _stopOn; set { _stopOn = value; } }

        protected override IObservable<EventPattern<FileSystemEventArgs>> CreateOutputObservable()
        => Observable.Defer(() =>
        {
#if DEBUG
            Debug.WriteLine($"Constructing {nameof(WaitFile)} stream");
#endif

            Token.ThrowIfCancellationRequested();

           // FileWatcher.EnableRaisingEvents = true;

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
            FileWatcher.EnableRaisingEvents = false;
        }

        public Task Start(CancellationToken token)
        {
            Token = token;
#if DEBUG
            Debug.WriteLine($"{nameof(WaitFile)} is starting");
#endif
            FileWatcher.EnableRaisingEvents = true;
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
