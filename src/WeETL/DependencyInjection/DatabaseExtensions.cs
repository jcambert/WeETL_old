
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.Databases;
using WeETL.Databases.ElasticSearch;
using WeETL.Databases.MongoDb;

namespace WeETL.DependencyInjection
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection UseMongoDb<TSettings>(this IServiceCollection sc,IConfiguration cfg,Action<TSettings> initSettings,params Type[] typeToRepositories)
            where TSettings: MongoDbSettings
        {
            sc.Configure<TSettings>(cfg.GetSection(typeof(TSettings).Name));
            sc.AddSingleton<IDatabaseSettings<MongoClientSettings>>(sp => { 
                var result =sp.GetRequiredService<IOptions<TSettings>>().Value;
                return result;
            });

            typeToRepositories.ToList().ForEach(t => sc.RegisterRepository(t, typeof(MongoDbRepository<,>), typeof(IDatabaseSettings<MongoClientSettings>)));
             return sc;
        }
        public static IServiceCollection UseElastic<TSettings>(this IServiceCollection sc, IConfiguration cfg,Action<TSettings> initSettings, params Type[] typeToRepositories)
            where TSettings : ElasticDbSettings
        {
            sc.Configure<TSettings>(cfg.GetSection(typeof(TSettings).Name));
            sc.AddSingleton<IDatabaseSettings<ConnectionSettings>>(sp => { 
                var result =sp.GetRequiredService<IOptions<TSettings>>().Value;
                initSettings?.Invoke(result);
                return result;
            });

            typeToRepositories.ToList().ForEach(t => sc.RegisterRepository(t, typeof(ElasticDbRepository<,>), typeof(IDatabaseSettings<ConnectionSettings>)));
            return sc;
        }
        
        private static void RegisterRepository(this IServiceCollection sc,Type repoType,Type repoImpl,Type dbSettings)
        {
            Type idType = repoType.GetProperty("Id").PropertyType;
            var t1 = typeof(IRepository<,>).MakeGenericType(repoType, idType);
            var ctor = repoImpl.MakeGenericType(repoType, idType).GetConstructor(new Type[] { dbSettings });
            sc.AddSingleton(t1, sp => { return ctor.Invoke(new object[] { sp.GetRequiredService(dbSettings) }); });


        }
    }
}
