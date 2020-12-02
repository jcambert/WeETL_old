using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace WeETL.Core
{
    public interface IETLWatchableComponent<T>
    {
        public IObservable<(T, DateTime)> OnStart { get; }

        public IObservable<(T, DateTime)> OnCompleted { get; }

    }

    public abstract class ETLWatchable<T> : IETLWatchableComponent<T>, IDisposable
    {
        private readonly ISubject<(T, DateTime)> _onStart = new Subject<(T, DateTime)>();
        private readonly ISubject<(T, DateTime)> _onCompleted = new Subject<(T, DateTime)>();
        private readonly ISubject<ConnectorException> _onError = new Subject<ConnectorException>();

        private readonly Stopwatch _timeWatcher = new Stopwatch();
        private readonly IDisposable _startDisposable;
        private readonly IDisposable _onCompletedDisposable;
        private bool disposedValue;
        private bool _isCompleted;
        private object o = new object();
        public ETLWatchable()
        {
            _startDisposable = OnStart.Subscribe(c =>
            {
                lock (o)
                {
                    StartTime = c.Item2;
                    _timeWatcher.Start();
                    _isCompleted = false;

                }
            });
            _onCompletedDisposable = OnCompleted.Subscribe(c =>
            {
                lock (o)
                {
                    _timeWatcher.Stop();
                    ElapsedTime = _timeWatcher.Elapsed;
                    _isCompleted = true;

                }
            });
        }
        public Guid Id { get; } = Guid.NewGuid();
        public IObservable<(T, DateTime)> OnStart => _onStart.AsObservable();

        public IObservable<(T, DateTime)> OnCompleted => _onCompleted.AsObservable();
        public IObservable<ConnectorException> OnError => _onError.AsObservable();

        public ISubject<(T, DateTime)> StartHandler => _onStart;
        public ISubject<(T, DateTime)> CompletedHandler => _onCompleted;
        protected ISubject<ConnectorException> ErrorHandler => _onError;
        public TimeSpan ElapsedTime { get; private set; }
        public DateTime StartTime { get; private set; }

        public bool IsRunning => _timeWatcher.IsRunning;
        public bool IsCompleted => _isCompleted;
        protected virtual void InternalDispose() { }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés)
                    _startDisposable?.Dispose();
                    _onCompletedDisposable?.Dispose();
                    InternalDispose();
                }

                // TODO: libérer les ressources non managées (objets non managés) et substituer le finaliseur
                // TODO: affecter aux grands champs une valeur null
                disposedValue = true;
            }
        }

        // // TODO: substituer le finaliseur uniquement si 'Dispose(bool disposing)' a du code pour libérer les ressources non managées
        // ~ETLWatchable()
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
