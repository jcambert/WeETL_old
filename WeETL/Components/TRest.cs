using System;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Core;
namespace WeETL.Components
{
    public enum TRestMode
    {
        Get,
        Put,
        Post,
        Delete,
        Patch
    }

    public class TRest<TSchema> : ETLStartableComponent<TSchema, TSchema>
        where TSchema : class, new()
    {
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private static Func<HttpClient, string,HttpContent,CancellationToken, Task<HttpResponseMessage>> Get = (client, requestUri,content,token) => client.GetAsync(requestUri,token);
        private static Func<HttpClient, string, HttpContent, CancellationToken, Task<HttpResponseMessage>> Post = (client, requestUri,content, token) => client.PostAsync(requestUri,content, token);
        private static Func<HttpClient, string, HttpContent, CancellationToken, Task<HttpResponseMessage>> Put = (client, requestUri,content, token) => client.PutAsync(requestUri,content, token);
        private static Func<HttpClient, string, HttpContent, CancellationToken, Task<HttpResponseMessage>> Delete = (client, requestUri,content, token) => client.DeleteAsync(requestUri, token);
        private static Func<HttpClient, string, HttpContent, CancellationToken, Task<HttpResponseMessage>> Patch = (client, requestUri,content, token) => client.PatchAsync(requestUri,content, token);


        private readonly IDisposable _modeObserver;
        private TRestMode _mode = TRestMode.Get;
        private Func<HttpClient, string, HttpContent, CancellationToken, Task<HttpResponseMessage>> _requestMethod=Get;
        public TRest() : base()
        {
            this._modeObserver= this.OnPropertyChanged.Where(p=>p=="Mode").Subscribe(e => {
                _requestMethod = Mode switch
                {
                    TRestMode.Get=>Get,
                    TRestMode.Delete=>Delete,
                    TRestMode.Patch=>Patch,
                    TRestMode.Post=>Post,
                    TRestMode.Put=>Put,
                    _=>throw new ArgumentException("NOT MANAGED")
                };
            });
        }

        public TRestMode Mode {
            get => _mode;
            set
            {
                this.SetPropertyValueAndNotify(e => e.Mode, ref _mode, value, PropertyChangedHandler);
            }
        }

        public string RequestUri { get; set; }
        public HttpContent Content { get; set; }
        public HttpClient Requester { get; } = new HttpClient();
        public HttpRequestHeaders Headers => Requester.DefaultRequestHeaders;

        public void Cancel()
        {
            _tokenSource.Cancel();
        }
        protected override async Task InternalStart()
        {
            Contract.Requires(RequestUri != null, "The request uri cannot be null");
            var response = await _requestMethod(Requester,RequestUri,Content,_tokenSource.Token);

            if (response.IsSuccessStatusCode)
            {
                var stringresult = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<TSchema>(stringresult);
                OutputHandler.OnNext(result);

                

            }
            OutputHandler.OnCompleted();

        }
        protected override void InternalDispose()
        {
            base.InternalDispose();
            _modeObserver?.Dispose();
        }
    }
}
