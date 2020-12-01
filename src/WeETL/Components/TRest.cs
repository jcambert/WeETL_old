using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Core;
using WeETL.Observables;

namespace WeETL.Components
{
   
    
    public class TRest<TSchema> : ETLStartableComponent<string, TSchema>
        where TSchema : class, new()
    {
       
     
        

        public ILogger<TRest<TSchema>> Logger { get; }

        
       public TRest(ILogger<TRest<TSchema>> logger) : base()
        {
            
            this.Logger = logger;
        }
        public string RequestUri { get; set; }
        protected virtual string GetRequestUri() => RequestUri;

        public RestRequestOptions<TSchema> Options { get; set; } = new RestRequestOptions<TSchema>();
        protected override  Task InternalStart(CancellationToken token)
        {

            RestRequest<TSchema> rest = new RestRequest<TSchema>();
            rest.Options = Options;
            rest.RequestUri = GetRequestUri();
            rest.Subscribe(OutputHandler,token);
            return Task.CompletedTask;
        }
        
    }
}
