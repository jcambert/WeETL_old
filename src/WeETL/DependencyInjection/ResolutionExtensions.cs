using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace WeETL.DependencyInjection
{
    public static class ResolutionExtensions
    {
        public static TService ResolveKeyed<TService>(this ServiceProvider serviceProvider, string key)
            where TService : notnull, INamed
        =>serviceProvider.GetServices<TService>().Where(s => s.Name == key).FirstOrDefault();
        

    }
}
