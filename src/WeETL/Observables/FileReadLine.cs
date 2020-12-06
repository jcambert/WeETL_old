using System;
using System.IO;
using System.Reactive.Linq;

namespace WeETL.Observables
{
    public interface IFileReadLine: ICommonObservable<string>
    {
        string Filename { get; set; }
    }
    public class FileReadLine : AbstractObservable<string>,IFileReadLine
    {
        public FileReadLine() : base()
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
