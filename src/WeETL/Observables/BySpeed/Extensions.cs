using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using WeETL.DependencyInjection;
using WeETL.Observables.BySpeed.IO;
using WeETL.Observables.Dxf;

namespace WeETL.Observables.BySpeed
{
    public class MapToDxfOption
    {
        public bool DrawFormat { get; set; }
    }
    public static class Extensions
    {

        public static IServiceCollection UseGCommands(this IServiceCollection sc)
        {
            GCode.Extensions.UseGCommands(sc);
            
            sc.AddTransient<ILaserCutBySpeedReader, LaserCutBySpeedReader>();
            sc.AddTransient<ILaserDocument, LaserDocument>();
            return sc;
        }
        public static void MapToDxf(this ILaserDocument document, IDxfDocument dxf,MapToDxfOption options=null)
        {
            options = options ?? new MapToDxfOption();

        }
    }
}
