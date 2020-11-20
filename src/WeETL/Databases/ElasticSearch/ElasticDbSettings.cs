using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WeETL.Databases.ElasticSearch
{
    public class ElasticDbSettings : AbstractDatabaseSettings<ConnectionSettings>
    {
        private static Func<List<string>, List<Uri>> ToUri = src => src.Select(s => new Uri(s)).ToList();
        //public ConnectionSettings Settings { get ; set ; }
     
        public List<string> Nodes { get; set; }
        protected override ConnectionSettings InternalCreateSettings()
       => Nodes.Count switch
       {
           0 => throw new ArgumentNullException($"{nameof(Nodes)} cannot be null"),
           1 => new ConnectionSettings(new Uri(Nodes[0])),
           _ => new ConnectionSettings(new StaticConnectionPool(ToUri(Nodes)))


       };

  
    }
}
