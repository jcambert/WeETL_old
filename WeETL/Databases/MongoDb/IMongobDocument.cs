using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WeETL.Databases.MongoDb
{
    public interface IMongobDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }

        DateTime CreatedAt { get; }
    }
}