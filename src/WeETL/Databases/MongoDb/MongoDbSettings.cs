using MongoDB.Driver;

namespace WeETL.Databases.MongoDb
{
    public class MongoDbSettings : AbstractDatabaseSettings<MongoClientSettings>
    {
        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 27017;

        protected override MongoClientSettings InternalCreateSettings()
        {
             MongoClientSettings settings=  new MongoClientSettings();
            settings.Server = new MongoServerAddress(Host, Port);
            
            return settings;
        }
    }
}
