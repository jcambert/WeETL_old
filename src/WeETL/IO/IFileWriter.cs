using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace WeETL.IO
{
    public interface IFileWriter<TDocument>
        where TDocument : IDocument
    {
        void Write(TDocument document, string filename);


        IObservable<TDocument> OnWrited { get; }
        IObservable<Exception> OnError { get; }
    }

    public abstract class FileWriter<TDocument>
        where TDocument : IDocument
    {
        private ISubject<TDocument> _onWrited = new Subject<TDocument>();
        private ISubject<Exception> _onError = new Subject<Exception>();

        public FileWriter()
        {

        }
        public IObservable<TDocument> OnWrited => _onWrited.AsObservable();
        public IObservable<Exception> OnError => _onError.AsObservable();

        public void Write(TDocument document, string filename)
        {
            try
            {
                using (TextWriter tw = new StreamWriter(filename, false))
                {
                    InternalWrite(tw,document, filename);
                }
                _onWrited.OnNext(document);

            }
            catch (Exception e)
            {
                _onError.OnNext(e);
            }
        }

        protected abstract void InternalWrite(TextWriter writer,TDocument document, string filename);
    }
}
