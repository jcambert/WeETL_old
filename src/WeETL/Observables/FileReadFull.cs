using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WeETL.Observables
{
    public class FileReadFull : AbstractObservable<string>
    {
        public FileReadFull() : base()
        {
        }
        public string Filename { get; set; }
        protected override IObservable<string> CreateOutputObservable()
       => Observable.Defer(() =>
       {
           try
           {
               return Observable.Using(
                   () => File.OpenText(Filename),
                   stream => Observable.Generate(
                       stream,
                       s => !s.EndOfStream,
                       s => s,
                       s => s.ReadToEnd()
                       )
                   );
           }catch(Exception e)
           {
               return Observable.Throw<string>(e);
           }
       });
    }
}
