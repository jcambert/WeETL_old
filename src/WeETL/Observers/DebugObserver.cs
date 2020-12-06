using System;
using System.Diagnostics;

namespace WeETL.Observers
{
    public interface IDebugObserver<T> : IObserver<T>
    {

    }
    public class DebugObserver<T> : IDebugObserver<T>
    {
        private readonly string _name;
        public DebugObserver(string name = "")
        {
            _name = name;
        }
        public void OnNext(T value)
        {
            Debug.WriteLine($"{_name} - OnNext({value})");
        }
        public void OnError(Exception error)
        {
            Debug.WriteLine($"{_name} - OnError:");
            Debug.WriteLine($"\t {error}" );
        }
        public void OnCompleted()
        {
            Debug.WriteLine($"{_name} - OnCompleted()");
        }
    }
}
