using System;
using WeETL.Observables.Dxf.Entities;

namespace WeETL.Observables.Dxf.IO
{
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
        };
    }*/
}
