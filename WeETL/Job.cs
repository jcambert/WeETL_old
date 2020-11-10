using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Core;
namespace WeETL
{
    public class Job : IDisposable, IStartable
    {
        #region private vars
        private readonly CancellationTokenSource tokenSource = new CancellationTokenSource();
        private readonly Stopwatch watcher = new Stopwatch();
        private readonly List<IStartable> _jobs = new List<IStartable>();
        private readonly ISubject<Job> _onStart = new Subject<Job>();
        private readonly ISubject<Job> _onCompleted = new Subject<Job>();
        private bool disposedValue;
        private IDisposable _onStartObserver;
        private IDisposable _onCompletedObserver;
        #endregion

        #region ctor
        /// <summary>
        /// Force to use ETLContext
        /// </summary>
        public Job(ETLContext ctx)
        {
            Contract.Requires(ctx != null, "ETLContext cannot be null. Use the DI");
            _onStartObserver = OnStart.Subscribe(j => watcher.Start());
            _onCompletedObserver = OnCompleted.Subscribe(j => watcher.Stop());
        }
        #endregion

        #region public methods
        public void Add(IStartable startable)
        {
            _jobs.Add(startable);
        }

        public void AddRange(IEnumerable<IStartable> jobs)
        {
            _jobs.AddRange(jobs);
        }

        public void Remove(IStartable startable)
        {
            _jobs.Remove(startable);
        }
        public async Task Start()
        {
            _onStart.OnNext(this);
            CancellationToken token = tokenSource.Token;
            await Task.Run(() => Task.WhenAll(_jobs.Select(j => j.Start())),token).ContinueWith(t => _onCompleted.OnNext(this));

        }
        public void Stop()
        {
            tokenSource.Cancel();
        }
        #endregion

        #region public properties
        public IObservable<IStartable> OnStart => _onStart.AsObservable();

        public IObservable<IStartable> OnCompleted => _onCompleted.AsObservable();

        public TimeSpan TimeElapsed => watcher.Elapsed.Duration();

        public bool IsCancellationRequested => tokenSource.IsCancellationRequested;
        #endregion

        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés)
                    _onStartObserver?.Dispose();
                    _onCompletedObserver?.Dispose();
                }

                // TODO: libérer les ressources non managées (objets non managés) et substituer le finaliseur
                // TODO: affecter aux grands champs une valeur null
                disposedValue = true;
            }
        }

        // // TODO: substituer le finaliseur uniquement si 'Dispose(bool disposing)' a du code pour libérer les ressources non managées
        // ~Job()
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
        #endregion

    }
}
