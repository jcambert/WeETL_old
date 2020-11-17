using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeETL.Databases.MongoDb
{
    public class MongoDbSettings : IDatabaseSettings<MongoClientSettings>
    {
        public virtual MongoClientSettings Settings { get; set; }

        public virtual string DatabaseName { get; set; }
    }
}
