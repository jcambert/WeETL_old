using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.Contracts;

namespace WeETL
{
    public class ETLContext
    {
        private readonly IServiceCollection _serviceCollection = new ServiceCollection();
        private ServiceProvider _serviceProvider;
        private bool _isConfigured = false;
        public ETLContext()
        {
        }
        public ETLContext ConfigureService(Action<IServiceCollection> cfg=null)
        {
            if (_isConfigured) return this;
            cfg?.Invoke(_serviceCollection);
            _serviceCollection
                .AddLogging(cfg => { cfg.AddConsole();cfg.AddDebug(); })
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information)
                .AddTransient<Job>()
                .AddTransient(typeof(TLogRow<>))
                .AddSingleton(this)
                ;
            _isConfigured = true;
            return this;
        }
        public void Build()
        {
            if (!_isConfigured) ConfigureService();
            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }
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
            return Provider.GetService<T>(); 
        }
        public Job CreateJob() => GetService<Job>();

    }
}
