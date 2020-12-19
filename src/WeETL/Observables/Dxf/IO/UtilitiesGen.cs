

using System;
using WeETL.Observables.Dxf.Units;
using WeETL.Observables.Dxf.Entities;
using System.Globalization;
namespace WeETL.Observables.Dxf.IO
{
    internal static class Utilities
    {
        internal static bool ReadString(string value, Action<string> onset)
        {
            if (value != null)
            {
                onset?.Invoke(value);
                return true;
            }
            return false;
        }

        internal static bool ReadDouble(string value, Action<double> onset)
        {
            if (double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                onset?.Invoke(result);
                return true;
            }
            return false;
        }
        internal static bool ReadInt(string value, Action<int> onset)
        {
            if (int.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                onset?.Invoke(result);
                return true;
            }
            return false;
        }
        internal static bool ReadOnOff(string value, Action<OnOff> onset)
        {
            if (int.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                onset?.Invoke(result > 1 ? OnOff.ON : OnOff.OFF);
                return true;
            }
            return false;
        }
        internal static bool ReadShort(string value, Action<short> onset)
        {
            if (short.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                onset?.Invoke(result);
                return true;
            }
            return false;
        }
        internal static bool ReadByte(string value, Action<byte> onset)
        {
            if (byte.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                onset?.Invoke(result);
                return true;
            }
            return false;
        }
        internal static bool ReadTimeSpan(string value, Action<TimeSpan> onset)
        {
            if (double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                onset?.Invoke(TimeSpan.FromHours(result));
                return true;
            }
            return false;
        }

        internal static bool ReadDateTime(string value, Action<DateTime> onset)
        {
            double res = 0.0;
            if (!ReadDouble(value, result => res = result))
            {
                return false;
            }
            try
            {
                onset?.Invoke(JulianToDateTime(res));
                return true;
            }
            catch
            {

                return false;
            }
        }
        static DateTime JulianToDateTime(double julianDate)
        {
            double unixTime = (julianDate - 2440587.5) * 86400;

            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTime).ToLocalTime();

            return dtDateTime;
        }
        internal static Action<EntityObject, string> ReadLayerName = (o, value) =>
        {
            ReadString(value, value => o.LayerName = value);
        };
        internal static Action<EntityObject, string> ReadColor = (o, value) =>
        {
            ReadInt(value, value => o.Color = value);
        };
        internal static Action<Line, string> ReadStartX = (o, value) =>
        {
            var start = o.Start;
            ReadDouble(value, value => start.X = value);
            o.Start = start;
        };
        internal static Action<Line, string> ReadStartY = (o, value) =>
        {
            var start = o.Start;
            ReadDouble(value, value => start.Y = value);
            o.Start = start;
        };
        internal static Action<Line, string> ReadStartZ = (o, value) =>
        {
            var start = o.Start;
            ReadDouble(value, value => start.Z = value);
            o.Start = start;
        };
        internal static Action<Line, string> ReadEndX = (o, value) =>
        {
            var end = o.End;
            ReadDouble(value, value => end.X = value);
            o.End = end;
        };
        internal static Action<Line, string> ReadEndY = (o, value) =>
        {
            var end = o.End;
            ReadDouble(value, value => end.Y = value);
            o.End = end;
        };
        internal static Action<Line, string> ReadEndZ = (o, value) =>
        {
            var end = o.End;
            ReadDouble(value, value => end.Z = value);
            o.End = end;
        };
        internal static Action<Line, string> ReadThickness = (o, value) =>
        {
            ReadDouble(value, value => o.Thickness = value);
        };
        internal static Action<Line, string> ReadSubclassMarker = (o, value) =>
        {
            ReadString(value, value => o.SubclassMarker = value);
        };
        internal static Action<Line, string> ReadNormalX = (o, value) =>
        {
            var normal = o.Normal;
            ReadDouble(value, value => normal.X = value);
            o.Normal = normal;
        };
        internal static Action<Line, string> ReadNormalY = (o, value) =>
        {
            var normal = o.Normal;
            ReadDouble(value, value => normal.Y = value);
            o.Normal = normal;
        };
        internal static Action<Line, string> ReadNormalZ = (o, value) =>
        {
            var normal = o.Normal;
            ReadDouble(value, value => normal.Z = value);
            o.Normal = normal;
        };
    }
}