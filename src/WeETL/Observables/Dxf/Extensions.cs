using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeETL.Numerics;
using WeETL.Observables.Dxf.Header;
using WeETL.Observables.Dxf.IO;

namespace WeETL.Observables.Dxf
{
    public static class Extensions
    {
        public static IServiceCollection UseDxf(this IServiceCollection sc)
        {

            sc.AddTransient<IDxfDocument, DxfDocument>();
            sc.AddTransient<IDxfHeader, DxfHeader>();
            sc.AddTransient<IDxfReader, DxfReader>();

            sc.RegisterReaders();

            sc.RegisterSupportedVersions();
            return sc;
        }

        private static void RegisterReaders(this IServiceCollection sc)
        {
            sc.AddTransient<IReaderFactory, ReaderFactory>();
            var readers = typeof(IReaderFactory).Assembly.GetTypes().Where(t => !t.IsAbstract && t.IsClass && typeof(IReader).IsAssignableFrom(t)).ToList();
            readers.ForEach(reader =>
            {
                sc.AddTransient(typeof(IReader), reader);
                Console.WriteLine($"Register reader {reader.Name}");
            });

        }

        private static void RegisterSupportedVersions(this IServiceCollection sc)
        {
            /*var versions = typeof(IDxfVersion).Assembly.GetTypes().Where(t => !t.IsAbstract && t.IsClass && typeof(IDxfVersion).IsAssignableFrom(t)).ToList();
            versions.ForEach(reader =>
            {
                sc.AddTransient(typeof(IDxfVersion), reader);
                Console.WriteLine($"Register Version {reader.Name}");
            });
            sc.AddSingleton(typeof(IDxfVersion), sp => sp.GetServices<IDxfVersion>().OrderBy(s=>s.Order).LastOrDefault());*/
            sc.AddTransient(typeof(IDxfVersion), sp => DxfVersion.R10);
            sc.AddTransient(typeof(IDxfVersion), sp => DxfVersion.R11);
            sc.AddTransient(typeof(IDxfVersion), sp => DxfVersion.AutoCad2000);


        }

        internal static bool IsVector3(this DxfHeaderValue header) =>
           header.GroupCodes.Count == 3 && header.GroupCodes.TrueForAll(c => c >= 10 && c <= 37);

        internal static T GetOwner<T>(this DxfObject o)
        {
            return default(T);
        }

    }

    internal static class Utilities
    {
        internal static string GroupCodeEntity => "0";
        internal static string GroupCodeName => "2";
        internal static string GroupCodeVariableName => "9";
        internal static string GroupCodeVectorX => "10";
        internal static string GroupCodeVectorY => "20";
        internal static string GroupCodeVectorZ => "30";

        internal static string WriteStartSection => $"{GroupCodeEntity}\nSECTION\n";
        internal static string WriteEndSection => $"{GroupCodeEntity}\nENDSEC\n";
        internal static string WriteStartHeader => $"{GroupCodeName}\nHEADER\n";

        internal static string WriteVector3(this Vector3 v, List<int> groupCodes)
        {
            if (groupCodes.Count != 3) throw new ArgumentException($"GroupCodes must have 3 values in {nameof(WriteVector3)}");
            var sb = new StringBuilder();
            sb.AppendLine($"{groupCodes[0]}\n{v.X}");
            sb.AppendLine($"{groupCodes[1]}\n{v.Y}");
            sb.AppendLine($"{groupCodes[2]}\n{v.Z}");
            return sb.ToString();
        }
    }
}
