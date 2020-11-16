using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using WeEFLastic.Infrastructure.Internal;

namespace WeEFLastic.Extensions.DependencyInjection
{
    public static class ElasticServiceCollectionExtensions
    {
        
        public static IServiceCollection AddEntityFrameworkElastic(this IServiceCollection serviceCollection)
        {
            var builder = new EntityFrameworkServicesBuilder(serviceCollection)
                .TryAdd<IDatabaseProvider, DatabaseProvider<ElasticOptionsExtension>> ();
            builder.TryAddCoreServices();
            return serviceCollection;
        }
    }
}
