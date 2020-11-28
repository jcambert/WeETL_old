using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Disposables;
using System.Net.Http;
using System.Threading;
using System.Net.Http.Headers;
using System.Reactive.Subjects;
using System.Text.Json;
#if DEBUG
using System.Diagnostics;
#endif
namespace WeETL.Observables
{
    public enum RestMode
    {
        Get,
        Put,
        Post,
        Delete,
        Patch
    }

    public class RestRequest<T> : AbstractObservable<T>, IDisposable
        where T : class, new()
    {
        private static Func<HttpClient, string, HttpContent, CancellationToken, Task<HttpResponseMessage>> Get = (client, requestUri, content, token) => client.GetAsync(requestUri, token);
        private static Func<HttpClient, string, HttpContent, CancellationToken, Task<HttpResponseMessage>> Post = (client, requestUri, content, token) => client.PostAsync(requestUri, content, token);
        private static Func<HttpClient, string, HttpContent, CancellationToken, Task<HttpResponseMessage>> Put = (client, requestUri, content, token) => client.PutAsync(requestUri, content, token);
        private static Func<HttpClient, string, HttpContent, CancellationToken, Task<HttpResponseMessage>> Delete = (client, requestUri, content, token) => client.DeleteAsync(requestUri, token);
        private static Func<HttpClient, string, HttpContent, CancellationToken, Task<HttpResponseMessage>> Patch = (client, requestUri, content, token) => client.PatchAsync(requestUri, content, token);

        private readonly IDisposable _modeObserver;
        private RestMode _mode = RestMode.Get;
        private Func<HttpClient, string, HttpContent, CancellationToken, Task<HttpResponseMessage>> _requestMethod = Get;
        private bool disposedValue;

        public RestRequest(CancellationTokenSource cts=null):base(cts)
        {
            this._modeObserver = this.OnPropertyChanged.Where(p => p == "Mode").Subscribe(e =>
            {
                _requestMethod = Mode switch
                {
                    RestMode.Get => Get,
                    RestMode.Delete => Delete,
                    RestMode.Patch => Patch,
                    RestMode.Post => Post,
                    RestMode.Put => Put,
                    _ => throw new ArgumentException("NOT MANAGED")
                };
            });

        }
        public RestRequestOptions<T> Options { get; set; } = new RestRequestOptions<T>();
        protected virtual string GetRequestUri() => RequestUri;
        public RestMode Mode
        {
            get => _mode;
            set
            {
                this.SetPropertyValueAndNotify(e => e.Mode, ref _mode, value, PropertyChangedHandler);
            }
        }
        public IObservable<string> OnPropertyChanged => PropertyChangedHandler.AsObservable();
        public ISubject<string> PropertyChangedHandler { get; } = new Subject<string>();
        public string RequestUri { get; set; }

        protected override IObservable<T> CreateOutputObservable()
        {

            return Observable.Defer(() =>
            {
#if DEBUG
                Debug.WriteLine($"Constructing {nameof(RestRequest<T>)} stream");
#endif
                return Observable.Create<T>( async o =>
              {

                  try
                  {
                      var uri = GetRequestUri();
                      var response = await _requestMethod(Options.Requester, uri, Options.Content, TokenSource.Token);

                      if (response.IsSuccessStatusCode)
                      {
                          var stringresult = await response.Content.ReadAsStringAsync();
                          var result = JsonSerializer.Deserialize<T>(stringresult);
                          o.OnNext(result);
                          //  o.OnCompleted();

                      }
                      else
                          o.OnError(new Exception(response.ReasonPhrase));
                  }
                  catch(Exception e)
                  {
                      o.OnError(e);
                  }
                  



                  // return Disposable.Empty;
              });
            });
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés)
                }

                // TODO: libérer les ressources non managées (objets non managés) et substituer le finaliseur
                // TODO: affecter aux grands champs une valeur null
                disposedValue = true;
            }
        }

        // // TODO: substituer le finaliseur uniquement si 'Dispose(bool disposing)' a du code pour libérer les ressources non managées
        // ~RestRequest()
        // {
        //     // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
