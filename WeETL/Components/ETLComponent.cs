using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace WeETL
{
    public abstract class ETLComponent<TInputSchema, TOutputSchema> : IDisposable
        where TInputSchema : class, new()
        where TOutputSchema : class, new()
    {
        #region private 
        private readonly IMapper _mapper;
        private int _outputCounter;
        private bool disposedValue;
        private IDisposable _inputDisposable;
        private Action<TOutputSchema> _transform;
        private readonly ISubject<ConnectorException> _onError = new Subject<ConnectorException>();
        private readonly ISubject<ETLComponent<TInputSchema, TOutputSchema>> _onSetInput = new Subject<ETLComponent<TInputSchema, TOutputSchema>>();
        private readonly ISubject<ETLComponent<TInputSchema, TOutputSchema>> _onRemoveInput = new Subject<ETLComponent<TInputSchema, TOutputSchema>>();
        private readonly ISubject<TInputSchema> _inputSubject = new Subject<TInputSchema>();
        private readonly ISubject<TOutputSchema> _outputSubject = new Subject<TOutputSchema>();
        private readonly Stopwatch _timeWatcher = new Stopwatch();
        #endregion
        #region ctor
        public ETLComponent()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TInputSchema,TOutputSchema>());
            _mapper = config.CreateMapper();
        }
        #endregion

        protected ISubject<TInputSchema> Input => _inputSubject;
        protected ISubject<TOutputSchema> Output => _outputSubject;
        #region public methods
        public virtual bool SetInput(IObservable<TInputSchema> obs)
        {
            if (!AcceptInput)
            {
                _onError.OnNext(new ConnectorException($"{this.GetType().Name} is Startable. It does not accept input"));
                return false;
            }
            //_input = component;
            _inputDisposable = obs.TimeInterval().Subscribe(
               row =>
               {
                  
                       // _elapsed += row.Interval;
                       if (!_timeWatcher.IsRunning) _timeWatcher.Start();
                       InternalOnRowBeforeTransform(_outputCounter++, row.Value);
                       var transformed = InternalTransform(row.Value);
                       InternalOnRowAfterTransform(_outputCounter, transformed);
                       //_transform?.Invoke(row.Value);
                       _outputSubject.OnNext(transformed);
                   
               },
               InternalOnException,
               InternalOnCompleted);
            return true;
        }
        public void Transform(Action<TOutputSchema> p)
        {
            this._transform = p;
        }
        #endregion

        #region protected methods
        protected virtual TOutputSchema InternalTransform(TInputSchema row)
        {
            var result= _mapper.Map<TOutputSchema>(row);
            this._transform?.Invoke(result);
            return result;
        }
        protected virtual void InternalOnRowBeforeTransform(int index,TInputSchema row)
        {
            
        }
        protected virtual void InternalOnRowAfterTransform(int index, TOutputSchema row)
        {
            
        }

        protected virtual void InternalOnException(Exception e)
        {

        }

        protected virtual void InternalOnCompleted()
        {
            _timeWatcher.Stop();
            ElapsedTime = _timeWatcher.Elapsed;
            _outputSubject.OnCompleted();
            Console.WriteLine($"{this.GetType().Name} Completed in".PadLeft(50)+$" -> {ElapsedTime.Duration()}");
        }
        #endregion

        #region public properties
        public bool AcceptInput => (_inputDisposable == null && !(this is IStartable));
        public IObservable<ConnectorException> OnError => _onError.AsObservable();
        public IObservable<ETLComponent<TInputSchema, TOutputSchema>> OnSetInput => _onSetInput.AsObservable();
        public IObservable<ETLComponent<TInputSchema, TOutputSchema>> OnRemoveInput => _onRemoveInput.AsObservable();
        public IObservable<TOutputSchema> OnOutput => Output.AsObservable();
        public TimeSpan ElapsedTime { get; private set; }

        #endregion


        #region IDisposable

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
  /*  public abstract class ETLComponent<TSchema> : IDisposable
        where TSchema : class, new()
    {
        private IDisposable _disposable;
        private ETLComponent<TSchema> _input;
        private int _outputCounter;
        private bool disposedValue;
        private Action<TSchema> _transform;
        private readonly ISubject<TSchema> _outputSubject = new Subject<TSchema>();
        private TimeSpan _elapsed;
        public ETLComponent()
        {
        }
        protected ISubject<TSchema> Output => _outputSubject;
        public TimeSpan ElapsedTime => _elapsed;
        public bool AcceptInput=>(_disposable == null && !(this is IStartable)) ;
        public virtual bool SetInput(IObservable<TSchema> input)
        {
            if (!AcceptInput) return false;
           // _input = component;
            _disposable = input.TimeInterval().Subscribe(
               row=>
               {
                   _elapsed += row.Interval;
                   InternalOnRow(_outputCounter++, row.Value);
                   _transform?.Invoke(row.Value);
                   _outputSubject.OnNext(row.Value);
               },
               InternalOnException,
               InternalOnCompleted);
            return true;
        }
        public IObservable<TSchema> OnOutput => Output.AsObservable();

        public ETLComponent<TSchema> Transform(Action<TSchema> transform)
        {
            this._transform = transform;
            return this;
        }

        protected virtual void InternalOnRow(int index,TSchema row)
        {

        }

        protected virtual void InternalOnException(Exception e)
        {

        }

        protected virtual void InternalOnCompleted()
        {
            _outputSubject.OnCompleted();
            Console.WriteLine($"{this.GetType().Name} Completed");
        }

        #region IDisposable
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
    }*/
    public class PopulateAction<TComponent, T> : Rule<Func<TComponent, T, object>>
    {
    }
    public class Rule<T>
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
        /// The rule set this rule belongs to.
        /// </summary>
        public string RuleSet { get; set; } = string.Empty;

        /// <summary>
        /// Prohibits the rule from being applied in strict mode.
        /// </summary>
        public bool ProhibitInStrictMode { get; set; } = false;
    }
}
