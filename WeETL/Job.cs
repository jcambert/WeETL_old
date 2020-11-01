using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace WeETL
{
    public class Job : IDisposable, IStartable
    {
        private readonly Stopwatch watcher = new Stopwatch();
        private readonly List<IStartable> _jobs = new List<IStartable>();
        private readonly ISubject<Job> _onStart = new Subject<Job>();
        private readonly ISubject<Job> _onCompleted = new Subject<Job>();
        private bool disposedValue;
        private IDisposable _onStartObserver;
        private IDisposable _onCompletedObserver;
        public Job()
        {
            _onStartObserver = OnStart.Subscribe(j => watcher.Start());
            _onCompletedObserver = OnCompleted.Subscribe(j => watcher.Stop());
        }
        public void Add(IStartable startable)
        {
            _jobs.Add(startable);
        }

        public void AddRange(IEnumerable<IStartable> jobs)
        {
            _jobs.AddRange(jobs);
        }
        public async Task Start()
        {
            _onStart.OnNext(this);
            await Task.WhenAll(_jobs.Select(j => j.Start())).ContinueWith(t => _onCompleted.OnNext(this));

        }
        public IObservable<Job> OnStart => _onStart.AsObservable();

        public IObservable<Job> OnCompleted => _onCompleted.AsObservable();

        public TimeSpan TimeElapsed => watcher.Elapsed.Duration();

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
    }
}
