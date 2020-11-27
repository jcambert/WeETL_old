using System;
using System.Diagnostics;

namespace WeETL.Observers
{
    public class DebugObserver<T> : IObserver<T>
    {
        private readonly string _name;
        public DebugObserver(string name = "")
        {
            _name = name;
        }
        public void OnNext(T value)
        {
            Debug.WriteLine("{0} - OnNext({1})", _name, value);
        }
        public void OnError(Exception error)
        {
            Debug.WriteLine("{0} - OnError:", _name);
            Debug.WriteLine("\t {0}", error);
        }
        public void OnCompleted()
        {
            Debug.WriteLine("{0} - OnCompleted()", _name);
        }
    }
}
