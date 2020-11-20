using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace WeETL
{
    public static class ObservableExtensions
    {
        public static IDisposable SubscribeAsync<T>(this IObservable<T> source, Func<T, Task> onNextAsync) =>
        source
            .Select(number => Observable.FromAsync(() => onNextAsync(number)))
            .Concat()
            .Subscribe();

        public static IDisposable SubscribeAsyncConcurrent<T>(this IObservable<T> source, Func<T, Task> onNextAsync) =>
            source
                .Select(number => Observable.FromAsync(() => onNextAsync(number)))
                .Merge()
                .Subscribe();

        public static IDisposable SubscribeAsyncConcurrent<T>(this IObservable<T> source, Func<T, Task> onNextAsync, int maxConcurrent) =>
            source
                .Select(number => Observable.FromAsync(() => onNextAsync(number)))
                .Merge(maxConcurrent)
                .Subscribe();
    }
}
