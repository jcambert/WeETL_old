using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.Numerics;
using WeETL.Observables.Dxf.Entities;

namespace WeETL.Observables.Dxf.IO
{
    internal partial class EntitySectionMultiLineReader
    {
        private static Func<Vector3, string,Vector3> ReadVector3X = (o, value) =>
        {
            double v = 0.0;
            Utilities.ReadDouble(value, value =>v = value);
            return new Vector3(v, o.Y, o.Z);
        };
        private static Func<Vector3, string,Vector3> ReadVector3Y = (o, value) =>
        {
            double v = 0.0;
            Utilities.ReadDouble(value, value => v = value);
            return new Vector3(o.X,  v,o.Z);
        };
        private static Func<Vector3, string,Vector3> ReadVector3Z = (o, value) =>
        {
            double v = 0.0;
            Utilities.ReadDouble(value, value => v = value);
            return new Vector3(o.X, o.Y, v);
        };
        private MultiLine MultiLine => DxfObject as MultiLine;

        Vector3 vertex = Vector3.NaN, direction = Vector3.NaN, miter = Vector3.NaN;
        List<double>[] Distances = null;
        int _currentStyleCount = -1;
        short _distance=0,_currentDistance=-1;
        List<int> _internalCodes = new List<int>() { 11, 12, 13,21,22,23,31,32,33, 74, 41 };
        protected override void InternalRead<TType>((int, string) code)
        {
            if (DxfObject == null || !_internalCodes.Contains( code.Item1)) return;
           
            base.InternalRead<TType>(code);
            if (code.Item1 == 11)
            {

                vertex = new Vector3();
                direction = new Vector3();
                miter = new Vector3();
                Distances = new List<double>[MultiLine.NumberofStyleElements];
                _currentStyleCount = -1;
                _currentDistance = -1;

            }

            Vector3 v = code.Item1 switch
            {
                int x when x == 11 || x == 21 || x == 31 => vertex,
                int x when x == 12 || x == 22 || x == 32 => direction,
                int x when x == 13 || x == 23 || x == 33 => miter,
                _ => Vector3.NaN
            };
            Func<Vector3, string,Vector3> fn1 = code.Item1 switch
            {
                int x when x >= 11 && x <= 13 => ReadVector3X,
                int x when x >= 21 && x <= 23 => ReadVector3Y,
                int x when x >= 31 && x <= 33 => ReadVector3Z,

                _ => null
            };
            if (fn1 != null && v != Vector3.NaN)
            {
                var result =fn1(v, code.Item2);
                if (code.Item1 == 11 || code.Item1 == 21 || code.Item1 == 31) vertex = result;
                if (code.Item1 == 12 || code.Item1 == 22 || code.Item1 == 32) direction= result;
                if (code.Item1 == 13 || code.Item1 == 23 || code.Item1 == 33) miter = result;
                return;
            }
            if (code.Item1 == 74)
            {
                Distances[++_currentStyleCount] = new List<double>();
                
                ReadShort(code.Item2, v => _distance = v);
                _currentDistance = -1;
                return;
            }
            if (code.Item1 == 41)
            {
                double value = 0.0;
                ReadDouble(code.Item2, v => value = v);
                Distances[_currentStyleCount].Add(value);
                _currentDistance++;
            }

            if (_currentDistance>-1 && (_currentDistance+1)==_distance &&   _currentStyleCount > -1 && (_currentStyleCount+1) == MultiLine.NumberofStyleElements)
            {
                // we need to convert WCS coordinates to OCS coordinates
                Matrix3 trans = MathHelper.ArbitraryAxis(MultiLine.Normal).Transpose();
                vertex = trans * vertex;
                direction = trans * direction;
                miter = trans * miter;

                MultiLineVertex segment = new MultiLineVertex(
                    new Vector2(vertex.X, vertex.Y),
                    new Vector2(direction.X, direction.Y),
                    new Vector2(miter.X, miter.Y),
                    Distances);
                MultiLine.Vertices.Add(segment);
                MultiLine.Elevation = vertex.Z;
            }
        }
    }


}
