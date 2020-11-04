using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace WeETL
{
    public abstract class ETLStartableComponent<TInputSchema, TOutputSchema> : ETLComponent<TInputSchema, TOutputSchema>, IStartable
          where TInputSchema : class//, new()
        where TOutputSchema : class, new()
    {
        #region private vars
        private readonly CancellationTokenSource tokenSource = new CancellationTokenSource();
        private readonly ISubject<ETLStartableComponent<TInputSchema, TOutputSchema>> _onStart = new Subject<ETLStartableComponent<TInputSchema, TOutputSchema>>();
        private readonly ISubject<ETLStartableComponent<TInputSchema, TOutputSchema>> _onCompleted = new Subject<ETLStartableComponent<TInputSchema, TOutputSchema>>();
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
        public IObservable<IStartable> OnStart => _onStart.AsObservable();

        public IObservable<IStartable> OnCompleted => _onCompleted.AsObservable();

        public TimeSpan TimeElapsed => _timeWatcher.Elapsed.Duration();

        public bool IsCancellationRequested => tokenSource.IsCancellationRequested;
        #endregion

        #region public methods

        public Task Start()
        {
            CancellationToken token = tokenSource.Token;
            var task = Task.Run(() =>
            {
                _onStart.OnNext(this);
                token.ThrowIfCancellationRequested();
                try
                {
                    if (!token.IsCancellationRequested)
                    {
                        InternalStart();
                        Output.OnCompleted();
                    }
                }
                catch (OperationCanceledException)
                {

                }
                catch (Exception e)
                {
                    Error.OnNext(new ConnectorException("An error occurs while reading json file. See Inner Exception", e));
                }
            }, token).ContinueWith(t => _onCompleted.OnNext(this));
            return task;
        }

        public void Stop()
        {
            tokenSource.Cancel();
        }
        #endregion

        #region protected methods
        protected abstract void InternalStart();

        protected override void InternalDispose()
        {
            base.InternalDispose();
            _onStartObserver?.Dispose();
            _onCompletedObserver?.Dispose();
        }
        #endregion
    }
}
