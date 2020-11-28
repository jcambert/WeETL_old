using System;
using System.IO;
using System.Reactive.Linq;
using System.Threading;

namespace WeETL.Observables
{
    public class FileReadLine : AbstractObservable<string>
    {
        public FileReadLine(CancellationTokenSource cts=null) : base(cts)
        {

        }

        public string Filename { get; set; }

        protected override IObservable<string> CreateOutputObservable()
       => Observable.Defer(() =>
       {
           return Observable.Using(
               () => File.OpenText(Filename),
               stream => Observable.Generate(
                   stream,
                   s => !s.EndOfStream,
                   s => s,
                   s => s.ReadLine()
                   )
               );
       });

    }
}
