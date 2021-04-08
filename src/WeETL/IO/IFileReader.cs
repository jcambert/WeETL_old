using System;
using System.IO;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using WeETL.Observables;
using WeETL.Utilities;

namespace WeETL.IO
{
    public interface IFileReader<TDocument>
        where TDocument : IDocument
    {
        void Load(string filename);
        TDocument Document { get; }

        IObservable<TDocument> OnLoaded { get; }

        IObservable<Exception> OnError { get; }
    }

    public abstract class FileReader<TDocument>
        where TDocument : IDocument
    {
        ISubject<TDocument> _onLoaded = new Subject<TDocument>();
        ISubject<Exception> _onError = new Subject<Exception>();
        public FileReader(TDocument document)
        {
            Check.NotNull(document, nameof(TDocument));
            this.Document = document;
        }

        public TDocument Document { get; }
        public IObservable<TDocument> OnLoaded => _onLoaded.AsObservable();
        public IObservable<Exception> OnError => _onError.AsObservable();

        protected virtual void SendLoaded(bool condition)
        {
            if (condition)
                _onLoaded.OnNext(Document);
        }
        protected void SendError(Exception e) => _onError.OnNext(e);
        public abstract void Load(string filename);
    }

    public abstract class FileLineReader<TDocument> : FileReader<TDocument>
        where TDocument : IDocument
    {
       protected ISubject<string> Lines = new ReplaySubject<string>();
        public FileLineReader(TDocument document, IFileReadLine lineReader) : base(document)
        {
            Check.NotNull(lineReader, nameof(IFileReadLine));
            this.LineReader = lineReader;
        }
        protected IFileReadLine LineReader { get; }
        public IObservable<string> OnLine => Lines.AsObservable();
        public override void Load(string filename)
        {
            try
            {

                LineReader.Filename = filename;
                LineReader.Output.Subscribe(line => Lines.OnNext(line), () => SendLoaded(true));
            }
            catch(FileNotFoundException e)
            {
                SendError(e);
            }
            catch (Exception e)
            {
                SendError(e);
            }
        }
    }
}
