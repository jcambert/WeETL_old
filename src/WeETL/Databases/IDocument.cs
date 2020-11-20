using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WeETL.Databases
{
    public interface IDocument<T>
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        T Id { get; set; }

        DateTime CreatedAt { get; set; }
    }
}
