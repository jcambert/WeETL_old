using System;

namespace WeETL.Numerics
{
    public static class Extensions
    {
        public static double ToDegree(this double v) => v * 180 / Math.PI;
    }
}
