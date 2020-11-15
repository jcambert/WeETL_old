using System;
using System.Reactive.Linq;
using WeETL.Core;

namespace WeETL
{
    public class TUnite<TSchema>: ETLCoreComponent
        where TSchema:class
    {
        private IObservable<TSchema> _observable = null;
        public void AddInput(IObservable<TSchema> input)
        {
            _observable = _observable == null ? input : _observable.Merge(input);
        }

        public IObservable<TSchema> OnOutput => _observable;

    }
}
