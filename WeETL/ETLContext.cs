using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text.RegularExpressions;
using WeETL.Core;

namespace WeETL
{
    public class ETLContext
    {
        private readonly IServiceCollection _serviceCollection = new ServiceCollection();
        private ServiceProvider _serviceProvider;
        private bool _isConfigured = false;
        private readonly bool _loadDefaultComponents;
        private readonly ISubject<ETLContext> _onLoaded = new Subject<ETLContext>();
        private readonly Dictionary<Type, int> _serviceCounter = new Dictionary<Type, int>();
        public ETLContext(bool loadDefaultComponents=true)
        {
            this._loadDefaultComponents = loadDefaultComponents;
        }
        public ETLContext ConfigureService(Action<IServiceCollection> cfg=null)
        {
            if (_isConfigured) return this;
            cfg?.Invoke(_serviceCollection);
            _serviceCollection
                .AddLogging(cfg => { cfg.AddConsole();cfg.AddDebug(); })
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information)
                .AddTransient<Job>()
                //.AddTransient(typeof(TLogRow<>))
                .AddSingleton(this)
                ;
            if (_loadDefaultComponents)
                _serviceCollection.LoadDefaultComponents();
            _isConfigured = true;
            return this;
        }
        public void Build()
        {
            if (!_isConfigured) ConfigureService();
            _serviceProvider = _serviceCollection.BuildServiceProvider();
            _onLoaded.OnNext(this);
        }
        public IObservable<ETLContext> OnLoaded => _onLoaded.AsObservable();
        public dynamic Global { get; private set; } = ETLGlobal.Create();
        public ServiceProvider Provider
        {
            get
            {
                Contract.Ensures(Contract.ValueAtReturn(out _serviceProvider) != null, "An error append on Building DI.Check your code");
                if (_serviceProvider == null) Build();
                return _serviceProvider;
            }
        }
        public T GetService<T>() {
            Contract.Ensures(Contract.Result<T>() != null, $"The service {nameof(T)} has not been registered.Check your configuration");
           
            Contract.EndContractBlock();
            T service= Provider.GetService<T>();
            if (service.IsTypeof<ETLCoreComponent>())
            {
                ETLCoreComponent cmp = service as ETLCoreComponent;
                if (!_serviceCounter.ContainsKey(typeof(T)))
                    _serviceCounter[typeof(T)] = 1;
                else
                    _serviceCounter[typeof(T)]++;
                var name = Regex.Replace(typeof(T).Name, "`[0-9]*", "");
                cmp.Name = $"{name}-{ _serviceCounter[typeof(T)]}";

            }
            return service;
        }
        public Job CreateJob() => GetService<Job>();

    }
}
