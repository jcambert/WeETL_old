using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace WeETL.Core
{
    public abstract class ETLStartableComponent<TInputSchema, TOutputSchema> : ETLComponent<TInputSchema, TOutputSchema>, IStartable
          where TInputSchema : class
        where TOutputSchema : class, new()
    {
        #region private vars
        //protected readonly CancellationTokenSource TokenSource = new CancellationTokenSource();
        
        private readonly Stopwatch _timeWatcher = new Stopwatch();
        private readonly IDisposable _onStartObserver;
        private readonly IDisposable _onCompletedObserver;
        #endregion

        #region ctor
        public ETLStartableComponent():base()
        {
            _onStartObserver = OnStart.Subscribe(j =>
            {
                _timeWatcher.Start();
            });
            _onCompletedObserver = OnCompleted.Subscribe(j =>
            {
                _timeWatcher.Stop();
            });
        }
        #endregion

        #region public properties


        public TimeSpan TimeElapsed => _timeWatcher.Elapsed.Duration();

//public bool IsCancellationRequested => TokenSource.IsCancellationRequested;
        
        #endregion

        #region public methods

        public Task Start(CancellationToken token)
        {
            if (!Enabled)
            {
                OutputHandler.OnCompleted();
                CompletedHandler.OnNext((this, DateTime.Now));
                return Task.CompletedTask;
            }
            

            var task = Task.Run(async () =>
            {

                StartHandler.OnNext((this, DateTime.Now));
                try
                {
                    token.ThrowIfCancellationRequested();
                    await InternalStart(token);
                    
                }
                catch (OperationCanceledException) { }
                catch (Exception e)
                {
                    ErrorHandler.OnNext(new ConnectorException($"An error occurs while processing {this.GetType().Name}. See Inner Exception", e));
                }
            }, token)
                .ContinueWith(t => { 
                    CompletedHandler.OnNext((this, DateTime.Now)); 
                });
            return task;
        }

      /*  public void Stop()
        {
            TokenSource.Cancel();
        }*/
        #endregion

        #region protected methods
        protected abstract Task InternalStart(CancellationToken token);

        protected override void InternalDispose()
        {
            base.InternalDispose();
            _onStartObserver?.Dispose();
            _onCompletedObserver?.Dispose();
        }
        #endregion
    }
}
