using MongoDB.Bson;
using System;

namespace WeETL.Databases.MongoDb
{

    public abstract class MongoDbDocument : IMongobDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}
