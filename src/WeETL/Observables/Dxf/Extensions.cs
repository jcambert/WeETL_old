using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WeETL.Numerics;
using WeETL.Observables.Dxf.Header;

namespace WeETL.Observables.Dxf
{
    public static class Extensions
    {
        public static IServiceCollection  UseDxf(this IServiceCollection sc)
        {
            sc.AddTransient<IDxfDocument, DxfDocument>();
            sc.AddTransient(typeof(IDxfVersion), sp => DxfVersion.AutoCad2018);
            sc.AddTransient<IDxfHeader, DxfHeader>();
            sc.AddTransient<IDxfReader, DxfReader>();
            return sc;
        }
        
        internal static bool IsVector3(this DxfHeaderValue header) =>
           header.GroupCodes.Count==3 &&  header.GroupCodes.TrueForAll(c => c >= 10 && c <= 37);
        
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
