using System;

namespace WeETL.Observers
{
    public interface IConsoleOberver<T> : IObserver<T>
    {

    }
    public class ConsoleObserver<T> : IConsoleOberver<T>
    {

        public string Name { get; set; } = string.Empty;

        public ConsoleObserver()
        {
            Name = typeof(T).Name;
        }
        public ConsoleObserver(string name )
        {
            Name = name;
          
        }

        public void OnNext(T value)
        {
            Console.WriteLine("{0} - OnNext({1})", Name ?? string.Empty, value);
        }
        public void OnError(Exception error)
        {
            Console.WriteLine("{0} - OnError:", Name??string.Empty);
            Console.WriteLine("\t {0}", error);
        }
        public void OnCompleted()
        {
            Console.WriteLine("{0} - OnCompleted()", Name ?? string.Empty);
        }
    }
}
