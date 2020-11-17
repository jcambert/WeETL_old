using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using WeEFLastic.Infrastructure.Internal;
using WeEFLastic.Storage.Internal;

namespace WeEFLastic.Extensions.DependencyInjection
{
    public static class ElasticServiceCollectionExtensions
    {
        
        public static IServiceCollection AddEntityFrameworkElastic(this IServiceCollection serviceCollection)
        {
            var builder = new EntityFrameworkServicesBuilder(serviceCollection)
                .TryAdd<IDatabaseProvider, DatabaseProvider<ElasticOptionsExtension>> ()
                .TryAdd<ITypeMappingSource, ElasticTypeMappingSource>()
                // NO NEED JUST IN RELATIONAL DB .TryAdd<ISqlGenerationHelper, SqliteSqlGenerationHelper>()

                ;
            builder.TryAddCoreServices();
            return serviceCollection;
        }
    }
}
