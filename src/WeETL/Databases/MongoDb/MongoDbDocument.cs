using MongoDB.Bson;
using System;

namespace WeETL.Databases.MongoDb
{

    public abstract class MongoDbDocument : IDocument<ObjectId>
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
