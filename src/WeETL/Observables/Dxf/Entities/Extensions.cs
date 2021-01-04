using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.Numerics;

namespace WeETL.Observables.Dxf.Entities
{
    public static class Extensions
    {
        public static Arc CloneMe(this Arc arc)
        {
            Arc entity = new Arc
            {
                
                //Arc properties
                Normal=arc.Normal,
                Center = arc.Center,
                Radius = arc.Radius,
                StartAngle = arc.StartAngle,
                EndAngle = arc.EndAngle,
                Thickness = arc.Thickness
            };

           

            return entity;
        }
        /// <summary>
        /// Moves, scales, and/or rotates the current entity given a 3x3 transformation matrix and a translation vector.
        /// </summary>
        /// <param name="transformation">Transformation matrix.</param>
        /// <param name="translation">Translation vector.</param>
        /// <remarks>
        /// Non-uniform scaling is not supported, create an ellipse arc from the arc data and transform that instead.<br />
        /// Matrix3 adopts the convention of using column vectors to represent a transformation matrix.
        /// </remarks>
        public static Arc TransformMe(this Arc arc, Matrix3 transformation, Vector3 translation)
        {
            Arc clone = arc.CloneMe();
            Vector3 newCenter = transformation * clone.Center + translation;
            Vector3 newNormal = transformation * clone.Normal;
            if (Vector3.Equals(Vector3.Zero, newNormal)) newNormal = clone.Normal;

            Matrix3 transOW = MathHelper.ArbitraryAxis(clone.Normal);
            Matrix3 transWO = MathHelper.ArbitraryAxis(newNormal).Transpose();

            Vector3 axis = transOW * new Vector3(clone.Radius, 0.0, 0.0);
            axis = transformation * axis;
            axis = transWO * axis;
            Vector2 axisPoint = new Vector2(axis.X, axis.Y);
            double newRadius = axisPoint.Modulus();
            if (MathHelper.IsZero(newRadius)) newRadius = MathHelper.Epsilon;

            Vector2 start = Vector2.Rotate(new Vector2(clone.Radius, 0.0), clone.StartAngle * MathHelper.DegToRad);
            Vector2 end = Vector2.Rotate(new Vector2(clone.Radius, 0.0), clone.EndAngle * MathHelper.DegToRad);

            Vector3 vStart = transOW * new Vector3(start.X, start.Y, 0.0);
            vStart = transformation * vStart;
            vStart = transWO * vStart;

            Vector3 vEnd = transOW * new Vector3(end.X, end.Y, 0.0);
            vEnd = transformation * vEnd;
            vEnd = transWO * vEnd;

            Vector2 startPoint = new Vector2(vStart.X, vStart.Y);
            Vector2 endPoint = new Vector2(vEnd.X, vEnd.Y);

            clone.Normal = newNormal;
            clone.Center = newCenter;
            clone.Radius = newRadius;

            if (Math.Sign(transformation.M11 * transformation.M22 * transformation.M33) < 0)
            {
                clone.EndAngle = Vector2.Angle(startPoint) * MathHelper.RadToDeg;
                clone.StartAngle = Vector2.Angle(endPoint) * MathHelper.RadToDeg;
            }
            else
            {
                clone.StartAngle = Vector2.Angle(startPoint) * MathHelper.RadToDeg;
                clone.EndAngle = Vector2.Angle(endPoint) * MathHelper.RadToDeg;
            }
            return clone;
        }


        /// <summary>
        /// Moves, scales, and/or rotates the current entity given a 3x3 transformation matrix and a translation vector.
        /// </summary>
        /// <param name="transformation">Transformation matrix.</param>
        /// <param name="translation">Translation vector.</param>
        /// <remarks>Matrix3 adopts the convention of using column vectors to represent a transformation matrix.</remarks>
        public static Line TransformMe(this Line line, Matrix3 transformation, Vector3 translation)
        {
            Line clone = line.CloneMe();
            Vector3 newNormal = transformation * clone.Normal;
            if (Vector3.Equals(Vector3.Zero, newNormal)) newNormal = clone.Normal;

            clone.Start = transformation * clone.Start+ translation;
            clone.End= transformation * clone.End+ translation;
            clone.Normal = newNormal;
            return clone;
        }

        /// <summary>
        /// Creates a new Line that is a copy of the current instance.
        /// </summary>
        /// <returns>A new Line that is a copy of this instance.</returns>
        public static Line CloneMe(this Line line)
        {
            Line entity = new Line
            {
                //EntityObject properties
               
                Normal = line.Normal,
                Start= line.Start,
                End= line.End,
                Thickness = line.Thickness
            };

            foreach (XData data in line.XData.Values)
                entity.XData.Add((XData)data.Clone());

            return entity;
        }
    }
}
