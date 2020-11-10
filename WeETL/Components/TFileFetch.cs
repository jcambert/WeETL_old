using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using WeETL.Core;

namespace WeETL.Components
{
    public class TFileFetch<TSchema> : ETLStartableComponent<TSchema, TSchema>
        where TSchema : class, new()
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public string RequestUri { get; set; }
        public HttpClient Requester => _httpClient;
        public HttpRequestHeaders Headers => _httpClient.DefaultRequestHeaders;
        protected override async Task InternalStart()
        {
            Contract.Requires(RequestUri != null, "The request uri cannot be null");
            var response = await _httpClient.GetAsync(RequestUri);
            if (response.IsSuccessStatusCode)
            {
                var stringresult = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<TSchema>(stringresult);
                OutputHandler.OnNext(result);


            }
            OutputHandler.OnCompleted();

        }
    }
}
