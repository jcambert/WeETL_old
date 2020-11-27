using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace WeETL.ConsoleApp
{
    public class NumbersObservable : IObservable<int>
    {
        private readonly int _amount;
        private IObservable<int> _obs;
        public NumbersObservable(int amount)
        {
            _amount = amount;
            _obs = Observable.Create<int>(observer =>
             {
                 for (int i = 0; i < _amount; i++)
                 {
                     observer.OnNext(i);
                 }
                 observer.OnCompleted();
                 observer.OnNext(_amount);
                 observer.OnNext(_amount + 1);
                 return Disposable.Empty;
             });
        }
        public IDisposable Subscribe(IObserver<int> observer)
        {
            return _obs.Subscribe(observer);
           
        }
    }
}
