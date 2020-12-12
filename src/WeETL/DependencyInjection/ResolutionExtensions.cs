using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WeETL.DependencyInjection
{
    public static class ResolutionExtensions
    {
        
        public static TService ResolveKeyed<TService>(this IServiceProvider serviceProvider, string key)
            where TService : notnull, INamed
            //>serviceProvider.GetServices<TService>().Where(s => s.Name == key).FirstOrDefault();
        {
            var services = serviceProvider.GetServices<TService>();
            var v=services.Where(s => s.Name == key).FirstOrDefault();
            return v;
        }
        
        public static TService ResolveKeyed<TService,TAttribute>(this IServiceProvider serviceProvider,string name)
            where TService : notnull
            where TAttribute:Attribute,INamed
            // =>serviceProvider.GetServices<TService>().Where(s => s.GetType().GetCustomAttributes(typeof(TAttribute), true).Where(t => (t as TAttribute).Name == name).Any()).FirstOrDefault();
        {
            var services = serviceProvider.GetServices<TService>();
            var v = services.Where(s => s.GetType().GetCustomAttributes(typeof(TAttribute), true).Where(t => (t as TAttribute).Name == name).Any());
            return v.FirstOrDefault();
        }
    }
}
