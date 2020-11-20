using MongoDB.Bson;
using System;

namespace WeETL.Databases
{
    public abstract class DocumentBase<T> : IDocument<T>
    {
        public T Id { get ; set ; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
    public abstract class DocumentByGuid : DocumentBase<Guid> { }

    public abstract class DocumentByObjectId : DocumentBase<ObjectId> { }
}
