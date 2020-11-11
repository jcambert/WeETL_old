using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace WeETL.Core
{
    public abstract class ETLStartableComponent<TInputSchema, TOutputSchema> : ETLComponent<TInputSchema, TOutputSchema>, IStartable
          where TInputSchema : class//, new()
        where TOutputSchema : class, new()
    {
        #region private vars
        private readonly CancellationTokenSource tokenSource = new CancellationTokenSource();
       // private readonly ISubject<IStartable> _onStart = new Subject<IStartable>();
        //private readonly ISubject<IStartable> _onCompleted = new Subject<IStartable>();
        private readonly Stopwatch _timeWatcher = new Stopwatch();
        private readonly IDisposable _onStartObserver;
        private readonly IDisposable _onCompletedObserver;
        #endregion

        #region ctor
        public ETLStartableComponent()
        {
            _onStartObserver = OnStart.Subscribe(j => _timeWatcher.Start());
            _onCompletedObserver = OnCompleted.Subscribe(j => _timeWatcher.Stop());
        }
        #endregion

        #region public properties
        

        public TimeSpan TimeElapsed => _timeWatcher.Elapsed.Duration();

        public bool IsCancellationRequested => tokenSource.IsCancellationRequested;
        #endregion

        #region public methods

        public Task Start()
        {
            if (!Enabled)
            {
                OutputHandler.OnCompleted();
                return Task.CompletedTask;
            }
            CancellationToken token = tokenSource.Token;
            var task = Task.Run(async () =>
            {
                
                StartHandler.OnNext((this,DateTime.Now));
                token.ThrowIfCancellationRequested();
                try
                {
                    if (!token.IsCancellationRequested)
                    {
                        await InternalStart();
                        
                        OutputHandler.OnCompleted();
                    }
                    else
                    {

                    }
                }
                catch (OperationCanceledException)
                {

                }
                catch (Exception e)
                {
                    ErrorHandler.OnNext(new ConnectorException($"An error occurs while processing {this.GetType().Name}. See Inner Exception", e));
                }
            }, token).ContinueWith(t => CompletedHandler.OnNext((this,DateTime.Now)));
            return task;
        }

        public void Stop()
        {
            tokenSource.Cancel();
        }
        #endregion

        #region protected methods
        protected abstract Task InternalStart();

        protected override void InternalDispose()
        {
            base.InternalDispose();
            _onStartObserver?.Dispose();
            _onCompletedObserver?.Dispose();
        }
        #endregion
    }
}
