using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Core;
namespace WeETL
{
    public interface IJob : IDisposable, IStartable
    {
        // public string Name { get;  }
    }
    public class Job : ETLWatchable<Job>, IJob
    {
        #region private vars
        private readonly CancellationTokenSource tokenSource = new CancellationTokenSource();
        private readonly Stopwatch watcher = new Stopwatch();
        private List<IStartable> _jobs = new List<IStartable>();
        private List<ETLCoreComponent> _components = new List<ETLCoreComponent>();

        #endregion

        #region ctor
        /// <summary>
        /// Force to use ETLContext
        /// </summary>
        public Job(ETLContext ctx)
        {
            Contract.Requires(ctx != null, "ETLContext cannot be null. Use the DI");
            this.Context = ctx;
        }
        #endregion

        #region public methods
        public dynamic Bag(string name) => Context.Bags[name];
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

        internal void AddComponent(ETLCoreComponent component)
        {
#if DEBUG
            component.OnCompleted.Subscribe(c => { Debug.WriteLine($"The Component {c.Item1.Id} in Job:{this.Id} is completed"); });
#endif
            _components.Add(component);
        }
        public Task Start(CancellationToken token)
        {
            StartHandler.OnNext((this, DateTime.Now));
            return Task.Run(async () =>
            {
                int jobsCount = _jobs.Count;
                var activeComponents = _components.Where(c => !c.IsCompleted).ToList();
                while (jobsCount > 0 || activeComponents.Any())
                {
                    await Task.Run(() => Task.WhenAll(_jobs.Where(j => !j.IsRunning).Select(j => j.Start(token))), token);
                    _jobs = _jobs.Where(t => !t.IsCompleted).ToList();
                    Thread.Sleep(100);
                    jobsCount = _jobs.Count;
                    activeComponents = _components.Where(c => !c.IsCompleted).ToList();
                }
            }).ContinueWith(t =>
            {

                CompletedHandler.OnNext((this, DateTime.Now));
            });



        }
        public Task Start() => Start(tokenSource.Token);
        public void Stop() => tokenSource.Cancel();
        public CancellationTokenSource TokenSource => tokenSource;
        #endregion

        #region public properties

        public ETLContext Context { get; }

        public bool IsCancellationRequested => tokenSource.IsCancellationRequested;

        #endregion

        protected override void InternalDispose()
        {
            base.InternalDispose();
        }

    }
}
