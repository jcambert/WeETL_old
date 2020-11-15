﻿using System;
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
    public interface IJob : IDisposable, IStartable
    {
       // public string Name { get;  }
    }
    public class Job :ETLWatchable<Job>, IJob
    {
        #region private vars
        private readonly CancellationTokenSource tokenSource = new CancellationTokenSource();
        private readonly Stopwatch watcher = new Stopwatch();
        private  List<IStartable> _jobs = new List<IStartable>();
        private readonly IDisposable _onStartObserver;
        private readonly IDisposable _onCompletedObserver;
        #endregion

        #region ctor
        /// <summary>
        /// Force to use ETLContext
        /// </summary>
        public Job(ETLContext ctx)
        {
            Contract.Requires(ctx != null, "ETLContext cannot be null. Use the DI");
            this.Context = ctx;
            _onStartObserver=OnStart.Subscribe(j => IsCompleted = false);
            _onCompletedObserver=OnCompleted.Subscribe(j => IsCompleted = true);
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
            StartHandler.OnNext((this,DateTime.Now));
            CancellationToken token = tokenSource.Token;
            /*await Task.Run(() => Task.WhenAll(_jobs.Select(j => j.Start())),token)
                .ContinueWith(t => CompletedHandler.OnNext((this,DateTime.Now)));*/
            while (_jobs.Count > 0)
            {
                await Task.Run(() => Task.WhenAll(_jobs.Select(j => j.Start())), token);
                _jobs = _jobs.Where(t => !t.IsCompleted).ToList();
            }
            CompletedHandler.OnNext((this, DateTime.Now));
        }
        public void Stop()
        {
            tokenSource.Cancel();
        }
        #endregion

        #region public properties

        public ETLContext Context { get;  }

        public bool IsCancellationRequested => tokenSource.IsCancellationRequested;

        public bool IsCompleted { get; private set; }
        #endregion

        protected override void InternalDispose()
        {
            base.InternalDispose();
            _onStartObserver?.Dispose();
            _onCompletedObserver?.Dispose();
        }

    }
}
