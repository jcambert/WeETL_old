using System;
using System.Text;
using WeETL.Numerics;

namespace WeETL.Observables.Dxf.Entities
{
    public partial class Circle
    {
        #region constructors

        /// <summary>
        /// Initializes a new instance of the <c>Circle</c> class.
        /// </summary>
        public Circle()
            : this(Vector3.Zero, 1.0)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <c>Circle</c> class.
        /// </summary>
        /// <param name="centerX">Circle <see cref="Vector3">center</see> x in world coordinates.</param>
        /// <param name="centerY">Circle <see cref="Vector3">center</see> x in world coordinates.</param>
        /// <param name="radius">Circle radius.</param>
        public Circle(double centerX,double centerY,double radius):this(new Vector3(centerX,centerY,0),radius)
        {

        }
        /// <summary>
        /// Initializes a new instance of the <c>Circle</c> class.
        /// </summary>
        /// <param name="center">Circle <see cref="Vector3">center</see> in world coordinates.</param>
        /// <param name="radius">Circle radius.</param>
        public Circle(Vector3 center, double radius)
            : base()
        {
            this.Center = center;
            if (radius <= 0)
                throw new ArgumentOutOfRangeException(nameof(radius), radius, "The circle radius must be greater than zero.");
            this.Radius = radius;
            this.Thickness = 0.0;
        }

        /// <summary>
        /// Initializes a new instance of the <c>Circle</c> class.
        /// </summary>
        /// <param name="center">Circle <see cref="Vector2">center</see> in world coordinates.</param>
        /// <param name="radius">Circle radius.</param>
        public Circle(Vector2 center, double radius)
            : this(new Vector3(center.X, center.Y, 0.0), radius)
        {
        }

        #endregion

    }
}
