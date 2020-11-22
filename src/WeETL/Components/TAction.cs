using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.Core;

namespace WeETL
{
    public class TAction<TSchema> : ETLComponent<TSchema, TSchema>
        
    {
        //private const string ACTION_PROPERTY = "ACTION";
        private Action<Job, TSchema> _action;
        private IDisposable _actionDisposable;
        public TAction()
        {
            //this.OnPropertyChanged.Where(p => p.Equals(ACTION_PROPERTY)).Subscribe(p => {

            // });
        }
        public void Set(Action<Job, TSchema> action)
        {
            this._action = action;
            //this.PropertyChangedHandler.OnNext(ACTION_PROPERTY);
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
