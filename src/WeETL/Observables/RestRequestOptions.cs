using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WeETL.Observables
{
    public class RestRequestOptions<T>
        where T : class, new()
    {
        public string RequestUri { get; set; }
        public HttpContent Content { get; set; }
        public HttpClient Requester { get; } = new HttpClient();
        public HttpRequestHeaders Headers => Requester.DefaultRequestHeaders;
    }
}
