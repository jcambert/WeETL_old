using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeETL.Numerics;
using WeETL.Observables.Dxf.Collections;
using WeETL.Observables.Dxf.Header;
using WeETL.Observables.Dxf.IO;

namespace WeETL.Observables.Dxf
{
    public static class Extensions
    {
        public static IServiceCollection UseDxf(this IServiceCollection sc)
        {
            sc.AddSingleton<IHandleRegistry, HandleRegistry>();
            sc.AddTransient<IDxfDocument, DxfDocument>();
            sc.AddTransient<IDxfHeader, DxfHeader>();

            sc
                .RegisterReaders()
                .RegisterWriters()
                .RegisterTablesComponents()
                .RegisterSupportedVersions();
            return sc;
        }

        private static IServiceCollection RegisterReaders(this IServiceCollection sc)
        {
#if DEBUG
            Console.Write(new string('#', 5));
            Console.Write("Register READERS ".PadBoth(20));
            Console.WriteLine(new string('#', 5));
#endif
            sc.AddTransient<IDxfReader, DxfReader>();
            sc.AddTransient<IReaderFactory, ReaderFactory>();
            var readers = typeof(IReaderFactory).Assembly.GetTypes().Where(t => !t.IsAbstract && t.IsClass && typeof(IReader).IsAssignableFrom(t)).ToList();
            readers.ForEach(reader =>
            {
                sc.AddTransient(typeof(IReader), reader);
#if DEBUG
                Console.WriteLine($"Register reader {reader.Name}");
#endif
            });
#if DEBUG
            Console.WriteLine(new string('*', 30));
#endif
            return sc;
        }
        private static IServiceCollection RegisterWriters(this IServiceCollection sc)
        {
#if DEBUG
            Console.Write(new string('#', 5));
            Console.Write("Register WRITERS ".PadBoth(20));
            Console.WriteLine(new string('#', 5));
#endif
            sc.AddTransient<IDxfWriter, DxfWriter>();
            sc.AddTransient<IWriterFactory, WriterFactory>();
            var writers = typeof(IWriterFactory).Assembly.GetTypes().Where(t => !t.IsAbstract && t.IsClass && typeof(IWriter).IsAssignableFrom(t)).ToList();
            writers.ForEach(writer =>
            {
                sc.AddTransient(typeof(IWriter), writer);
#if DEBUG
                Console.WriteLine($"Register Writer {writer.Name}");
#endif
            });
#if DEBUG
            Console.WriteLine(new string('*', 30));
#endif
            return sc;
        }


        private static IServiceCollection RegisterTablesComponents(this IServiceCollection sc)
        {
            sc.AddTransient<IDxfTables, DxfTables>();
            sc.AddTransient<ITextStyles, TextStyles>();
            return sc;
        }

        private static void RegisterSupportedVersions(this IServiceCollection sc)
        {
            /*var versions = typeof(IDxfVersion).Assembly.GetTypes().Where(t => !t.IsAbstract && t.IsClass && typeof(IDxfVersion).IsAssignableFrom(t)).ToList();
            versions.ForEach(reader =>
            {
                sc.AddTransient(typeof(IDxfVersion), reader);
                Console.WriteLine($"Register Version {reader.Name}");
            });
            sc.AddSingleton(typeof(IDxfVersion), sp => );*/
            sc.AddTransient(typeof(IDxfVersion), sp => DxfVersion.R10);
            sc.AddTransient(typeof(IDxfVersion), sp => DxfVersion.R11);
            sc.AddTransient(typeof(IDxfVersion), sp => DxfVersion.AutoCad2000);


        }
        internal static IDxfVersion GetLastSupported(this IServiceProvider sp) => sp.GetServices<IDxfVersion>().OrderBy(s => s.Order).LastOrDefault();

        internal static bool IsVector3(this DxfHeaderValue header) =>
           header.GroupCodes.Count == 3 && header.GroupCodes.TrueForAll(c => c >= 10 && c <= 37);

        internal static void SetOwner(this IDxfObject parent, IDxfObject child)
        {
            if (child is DxfObject)
                (child as DxfObject).Owner = parent.Handle;
        }
        internal static void SetOwner(this IDxfObject parent, IDxfObject child, long handle)
        {
            parent.SetOwner(child);
            if (child is DxfObject)
                (child as DxfObject).Handle = handle.ToString();
        }

        internal static T GetOwner<T>(this DxfObject o)
        {
            return default(T);
        }

        public static bool IsHexString(this string value)
        {
            try
            {
                var res = value.HexStringToInt();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public static int HexStringToInt(this string value)
        => value.StartsWith("0x") ? Convert.ToInt32(value, 16) : int.Parse(value, System.Globalization.NumberStyles.HexNumber);

        public static string IntToHexString(this int value) => $"{value:X}";
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
