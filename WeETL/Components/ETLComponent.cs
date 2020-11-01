using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace WeETL
{
    public abstract class ETLComponent<TSchema> : IDisposable
        where TSchema : class, new()
    {
        private IDisposable _disposable;
        private ETLComponent<TSchema> _input;
        private int _outputCounter;
        private bool disposedValue;
        private Action<TSchema> _transform;
        private readonly ISubject<TSchema> _outputSubject = new Subject<TSchema>();
        private TimeSpan _elapsed;
        //private IDisposable _disposableInput;
        public ETLComponent()
        {
        }
        //public bool HasInput { get; protected set; } = true;
        protected ISubject<TSchema> Output => _outputSubject;
        public TimeSpan ElapsedTime => _elapsed;
        public bool AcceptInput=>(_disposable == null && !(this is IStartable)) ;
        public virtual bool SetInput(ETLComponent<TSchema> component)
        {
            if (!AcceptInput) return false;
            _input = component;
            _disposable = component.OnOuput.TimeInterval().Subscribe(
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
        public IObservable<TSchema> OnOuput => Output.AsObservable();

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
    }
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
