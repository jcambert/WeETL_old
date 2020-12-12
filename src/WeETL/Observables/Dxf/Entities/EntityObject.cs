using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.Numerics;

namespace WeETL.Observables.Dxf.Entities
{
    public abstract class EntityObject:DxfObject, ICloneable
    {
        #region private Variables
        private Vector3 _normal;
        #endregion

        #region ctor
        protected EntityObject(string dxfCode):base(dxfCode)
        {
            Normal = Vector3.UnitZ;
        }
        #endregion

        #region public properties
        /// <summary>
        /// Gets or sets the entity <see cref="Vector3">normal</see>.
        /// </summary>
        public Vector3 Normal
        {
            get { return _normal; }
            set
            {
                if (Vector3.Equals(Vector3.Zero, value))
                {
                    throw new ArgumentException("The normal can not be the zero vector.", nameof(value));
                }
                _normal = Vector3.Normalize(value);
            }
        }
        #endregion

        #region public methods

        /// <summary>
        /// Moves, scales, and/or rotates the current entity given a 3x3 transformation matrix and a translation vector.
        /// </summary>
        /// <param name="transformation">Transformation matrix.</param>
        /// <param name="translation">Translation vector.</param>
        /// <remarks>Matrix3 adopts the convention of using column vectors to represent a transformation matrix.</remarks>
        public abstract void TransformBy(Matrix3 transformation, Vector3 translation);

        /// <summary>
        /// Moves, scales, and/or rotates the current entity given a 4x4 transformation matrix.
        /// </summary>
        /// <param name="transformation">Transformation matrix.</param>
        /// <remarks>Matrix4 adopts the convention of using column vectors to represent a transformation matrix.</remarks>
        public void TransformBy(Matrix4 transformation)
        {
            Matrix3 m = new Matrix3(transformation.M11, transformation.M12, transformation.M13,
                                    transformation.M21, transformation.M22, transformation.M23,
                                    transformation.M31, transformation.M32, transformation.M33);
            Vector3 v = new Vector3(transformation.M14, transformation.M24, transformation.M34);

            this.TransformBy(m, v);
        }

        #endregion

        #region overrides

        /// <summary>
        /// Converts the value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            return string.Empty;
        }

        #endregion

        #region ICloneable

        /// <summary>
        /// Creates a new entity that is a copy of the current instance.
        /// </summary>
        /// <returns>A new entity that is a copy of this instance.</returns>
        public abstract object Clone();

        #endregion
    }
}
