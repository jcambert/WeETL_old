using System;
using System.IO;
using System.Text;
using WeETL.Observables.Dxf.Entities;

namespace WeETL.Observables.Dxf.IO
{
    internal static class OtherUtilities
    {
       /* internal static Func<Circle,string> ToDxfFormat=>
        
             circle =>
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("0"); sb.AppendLine("CIRCLE");
                sb.AppendLine("10"); sb.AppendLine(circle.Center.X.ToString());
                sb.AppendLine("20"); sb.AppendLine(circle.Center.Y.ToString());
                sb.AppendLine("30"); sb.AppendLine(circle.Center.Z.ToString());
                sb.AppendLine("40"); sb.AppendLine(circle.Radius.ToString());
                return sb.ToString();
            }; 
        
    }*/
    /*internal static class Utilities
    {
        internal static Action<AbstractReader, Line, string> ReadThickness = (reader, line, value) => reader.ReadDouble(value, value => line.Thickness = value);
        internal static Action<AbstractReader, Line, string> ReadNormalX = (reader, line, value) =>
        {
            var normal = line.Normal;
            reader.ReadDouble(value, value => normal.X = value);
            line.Normal = normal;
        };
        internal static Action<AbstractReader, Line, string> ReadNormalY = (reader, line, value) =>
        {
            var normal = line.Normal;
            reader.ReadDouble(value, value => normal.Y = value);
            line.Normal = normal;
        };

        internal static Action<AbstractReader, Line, string> ReadNormalZ = (reader, line, value) =>
        {
            var normal = line.Normal;
            reader.ReadDouble(value, value => normal.Z = value);
            line.Normal = normal;
        };*/
    }
}
