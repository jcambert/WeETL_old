using System;
using System.Globalization;

namespace WeETL.Utilities
{
    internal static class Extensions
    {
        internal static double? TrySetDoubleValue(this string from) => double.TryParse(from, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double res) ? res : null;

        internal static int? TrySetIntValue(this string from) => Int32.TryParse(from, out int res) ? res : null;
    }
}
