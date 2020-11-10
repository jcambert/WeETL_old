using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace WeETL.Core
{
    public abstract class ETLComponent<TInputSchema, TOutputSchema> : ETLCoreComponent, IETLWatchableComponent<ETLComponent<TInputSchema, TOutputSchema>>, IDisposable
        where TInputSchema : class//, new()
        where TOutputSchema : class, new()
    {
        #region private vars

        private int _outputCounter;
        private bool disposedValue;
        private IDisposable _inputDisposable = null;
        private Action<TOutputSchema> _transform;
        private readonly ISubject<ConnectorException> _onError = new Subject<ConnectorException>();
        private readonly ISubject<ETLComponent<TInputSchema, TOutputSchema>> _onSetInput = new Subject<ETLComponent<TInputSchema, TOutputSchema>>();
        private readonly ISubject<ETLComponent<TInputSchema, TOutputSchema>> _onRemoveInput = new Subject<ETLComponent<TInputSchema, TOutputSchema>>();
        private readonly ISubject<TInputSchema> _inputSubject = new Subject<TInputSchema>();
        private readonly ISubject<TOutputSchema> _outputSubject = new Subject<TOutputSchema>();
        private readonly ISubject<ETLComponent<TInputSchema, TOutputSchema>> _onStart = new Subject<ETLComponent<TInputSchema, TOutputSchema>>();
        private readonly ISubject<ETLComponent<TInputSchema, TOutputSchema>> _onCompleted = new Subject<ETLComponent<TInputSchema, TOutputSchema>>();
        private readonly Stopwatch _timeWatcher = new Stopwatch();
        #endregion
        #region ctor
        public ETLComponent()
        {
            CreateMapper();

        }
        #endregion
        protected bool DeferSendingOutput { get; set; }
        public IObservable<ETLComponent<TInputSchema, TOutputSchema>> OnStart => _onStart.AsObservable();

        public IObservable<ETLComponent<TInputSchema, TOutputSchema>> OnCompleted => _onCompleted.AsObservable();
        protected ISubject<ETLComponent<TInputSchema, TOutputSchema>> StartHandler => _onStart;
        protected ISubject<ETLComponent<TInputSchema, TOutputSchema>> CompleteHandler => _onCompleted;
        protected ISubject<TInputSchema> InputHandler => _inputSubject;
        protected ISubject<TOutputSchema> OutputHandler => _outputSubject;
        protected ISubject<ConnectorException> ErrorHandler => _onError;
        protected IMapper Mapper { get; set; }
        #region public methods
        public virtual bool SetInput(IObservable<TInputSchema> obs)
        {
            var result = SetObservable<TInputSchema, TOutputSchema>(
                obs,
                AcceptInput,
                $"{this.GetType().Name} is Startable. It does not accept input",
                ref _inputDisposable,
                InternalOnException,
                InternalOnInputCompleted,
                InternalOnInputBeforeTransform,
                InternalInputTransform,
                InternalOnInputAfterTransform,
                InternalSendOutput,
                DeferSendingOutput
                );
            if (result) _onSetInput.OnNext(this);
            return result;

        }
        protected bool SetObservable<TIn, TOut>(
            IObservable<TIn> obs,
            bool accept,
            string acceptErrorMessage,
            ref IDisposable disposable,
            Action<Exception> exception,
            Action completed,
            Action<int, TIn> beforeTransform = null,
            Func<TIn, TOut> transform = null,
            Action<int, TOut> afterTransform = null,
            Action<int, TOut> sendOutput = null,
            bool deferOutput = false)
             where TIn : class//, new() 
            where TOut : class, new()

        {
            if (!accept)
            {
                _onError.OnNext(new ConnectorException(acceptErrorMessage));
                return false;
            }
            //_input = component;
            disposable = obs.TimeInterval().Subscribe(
               row =>
               {
                   if (!_timeWatcher.IsRunning)
                   {
                       StartTime = DateTime.Now;
                       _timeWatcher.Start();
                       _onStart.OnNext(this);
                   }
                   if ((Enabled || !Passthrue) && beforeTransform != null && transform != null && afterTransform != null && sendOutput != null)
                   {


                       if (!Passthrue) beforeTransform(_outputCounter++, row.Value);
                       TOut transformed = transform(row.Value);
                       if (!Passthrue) afterTransform(_outputCounter, transformed);

                       if (!deferOutput) sendOutput(_outputCounter, transformed);
                   }
               },
               exception,
               completed);
            //set.OnNext(this);
            return true;
        }
        public void Transform(Action<TOutputSchema> p)
        {
            this._transform = p;
        }
        #endregion

        #region protected methods
        protected virtual void CreateMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TInputSchema, TOutputSchema>());
            Mapper = config.CreateMapper();
        }
        protected virtual void InternalSendOutput(int index, TOutputSchema row)
        {
            Contract.Requires(index > 0, "InternalOnInputBeforeTransform require index >0");
            Contract.Requires(row != null, "InternalSendOutput row cannot be null");
            _outputSubject.OnNext(row);
        }
        protected virtual void InternalOnInputBeforeTransform(int index, TInputSchema row)
        {
            Contract.Requires(index > 0, "InternalOnInputBeforeTransform require index >0");
            Contract.Requires(row != null, "InternalOnInputBeforeTransform row cannot be null");
        }
        protected virtual TOutputSchema InternalInputTransform(TInputSchema row)
        {
            Contract.Requires(Mapper != null, "You must create Automapper configuration before using Transforming Schema");
            Contract.Requires(row != null, "InternalInputTransform row cannot be null");
            Contract.Ensures(Contract.ValueAtReturn<TOutputSchema>(out var x) != null, "InternalInputTransform the return value cannot be null. Checkou the mapper configuration");
            var result = Mapper.Map<TOutputSchema>(row);
            this._transform?.Invoke(result);
            return result;
        }
        protected virtual void InternalOnInputAfterTransform(int index, TOutputSchema row)
        {
            Contract.Requires(index > 0, "InternalOnInputAfterTransform require index >0");
            Contract.Requires(row != null, "InternalOnInputAfterTransform row cannot be null");

        }

        protected virtual void InternalOnException(Exception e)
        {
            Contract.Requires(e != null, "InternalOnException exception cannot be null");

        }

        protected virtual void InternalOnInputCompleted()
        {
            _timeWatcher.Stop();
            ElapsedTime = _timeWatcher.Elapsed;
            _outputSubject.OnCompleted();
            _onCompleted.OnNext(this);

        }

        protected virtual void InternalDispose()
        {
            _inputDisposable?.Dispose();
        }
        #endregion

        #region public properties
        public bool AcceptInput => (_inputDisposable == null && !(this is IStartable));
        public IObservable<ConnectorException> OnError => _onError.AsObservable();
        public IObservable<ETLComponent<TInputSchema, TOutputSchema>> OnSetInput => _onSetInput.AsObservable();
        public IObservable<ETLComponent<TInputSchema, TOutputSchema>> OnRemoveInput => _onRemoveInput.AsObservable();
        public IObservable<TOutputSchema> OnOutput => OutputHandler.AsObservable();
        public TimeSpan ElapsedTime { get; private set; }
        public DateTime StartTime { get; private set; }
        #endregion


        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés)
                    InternalDispose();
                }

                // TODO: libérer les ressources non managées (objets non managés) et substituer le finaliseur
                // TODO: affecter aux grands champs une valeur null
                disposedValue = true;
            }
        }

        // // TODO: substituer le finaliseur uniquement si 'Dispose(bool disposing)' a du code pour libérer les ressources non managées
        // ~ETLComponent()
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

    public class PopulateAction<TComponent, T> : ActionRule<Func<TComponent, T, object>>
    {
    }
    public class ActionRule<T>
    {
        /// <summary>
        /// Populate action
        /// </summary>
        public T Action { get; set; }

        /// <summary>
        /// Property name, maybe null for finalize or create.
        /// </summary>
        public string PropertyName { get; set; }



        /// <summary>
        /// Prohibits the rule from being applied in strict mode.
        /// </summary>
        public bool ProhibitInStrictMode { get; set; } = false;
    }

    public class PopulateOrdering<TComponent>
         where TComponent : class, new()
    {
        public Func<TComponent, object> Order { get; internal set; }
        public SortOrder SortOrder { get; internal set; }
    }

    public class OrderingRule<T>
    {

    }
}
