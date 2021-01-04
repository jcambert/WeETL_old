using System;

namespace WeETL.IO
{
    public interface IFileReader<TDocument>
        where TDocument:IDocument
    {
        void Load(string filename);
        TDocument Document { get; }

        IObservable<TDocument> OnLoaded { get; }
    }
}
