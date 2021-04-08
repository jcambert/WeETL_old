using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.Observables.PN2000.IO;

namespace WeETL.Observables.PN2000
{
    public static class Extensions
    {
        public static IServiceCollection UsePnCad(this IServiceCollection sc)
        {
            sc.AddTransient<IPnCadDocument, PnCadDocument>();
            sc.AddTransient<IPnCadReader, PnCadReader>();
            return sc;
        }
    }
}
