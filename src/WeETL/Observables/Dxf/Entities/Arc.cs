using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.Numerics;

namespace WeETL.Observables.Dxf.Entities
{
    public partial class Arc
    {
        #region constructors

        /// <summary>
        /// Initializes a new instance of the <c>Arc</c> class.
        /// </summary>
        public Arc()
            : this(Vector3.Zero, 1.0, 0.0, 180.0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <c>Arc</c> class.
        /// </summary>
        /// <param name="center">Arc <see cref="Vector2">center</see> in world coordinates.</param>
        /// <param name="radius">Arc radius.</param>
        /// <param name="startAngle">Arc start angle in degrees.</param>
        /// <param name="endAngle">Arc end angle in degrees.</param>
        public Arc(Vector2 center, double radius, double startAngle, double endAngle)
            : this(new Vector3(center.X, center.Y, 0.0), radius, startAngle, endAngle)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <c>Arc</c> class.
        /// </summary>
        /// <param name="center">Arc <see cref="Vector3">center</see> in world coordinates.</param>
        /// <param name="radius">Arc radius.</param>
        /// <param name="startAngle">Arc start angle in degrees.</param>
        /// <param name="endAngle">Arc end angle in degrees.</param>
        public Arc(Vector3 center, double radius, double startAngle, double endAngle)
            : base()
        {
            this.Center = center;
            if (radius <= 0)
                throw new ArgumentOutOfRangeException(nameof(radius), radius, "The circle radius must be greater than zero.");
            this.Radius = radius;
            this.StartAngle = MathHelper.NormalizeAngle(startAngle);
            this.EndAngle = MathHelper.NormalizeAngle(endAngle);
            this.Thickness = 0.0;
        }

        #endregion

    }
}
