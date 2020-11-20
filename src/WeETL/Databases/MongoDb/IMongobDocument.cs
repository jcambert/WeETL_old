using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WeETL.Databases.MongoDb
{
    public interface IMongobDocument:IDocument<ObjectId>
    {
        

    }
}