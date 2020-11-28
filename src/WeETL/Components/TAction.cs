using System;
using System.Reactive.Linq;
using WeETL.Core;

namespace WeETL
{
    public class TAction<TSchema> : ETLComponent<TSchema, TSchema>
    {
        private Action<Job, TSchema> _action;
        private IDisposable _actionDisposable;
        public TAction()
        {
        }
        public void Set(Action<Job, TSchema> action)
        {
            this._action = action;
            _actionDisposable = OnOutput.Subscribe(row =>
            {
                _action?.Invoke(this.Job, row);
            });
        }
        protected override void InternalDispose()
        {
            base.InternalDispose();
            _actionDisposable?.Dispose();
        }
    }
}
