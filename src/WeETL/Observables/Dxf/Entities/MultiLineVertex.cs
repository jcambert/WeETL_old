using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.Numerics;

namespace WeETL.Observables.Dxf.Entities
{
    /// <summary>
    /// Represents a <see cref="MLine">multiline</see> vertex.
    /// </summary>
    public class MultiLineVertex
    {
        #region private fields

        private Vector2 position;
        private readonly Vector2 direction;
        private readonly Vector2 miter;
        private readonly List<double>[] distances;

        #endregion

        #region constructors

        internal MultiLineVertex(Vector2 location, Vector2 direction, Vector2 miter, List<double>[] distances)
        {
            this.position = location;
            this.direction = direction;
            this.miter = miter;
            this.distances = distances;
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets the MLine vertex position.
        /// </summary>
        /// <remarks>
        /// If this property is modified the function MLine.CalculateVertexesInfo() will need to be called manually to update the internal information.
        /// </remarks>
        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        /// <summary>
        /// Gets the MLine vertex direction.
        /// </summary>
        public Vector2 Direction
        {
            get { return this.direction; }
        }

        /// <summary>
        /// Gets the MLine vertex miter.
        /// </summary>
        public Vector2 Miter
        {
            get { return this.miter; }
        }

        /// <summary>
        /// Gets the <see cref="MLine">multiline</see> vertex distances lists.
        /// </summary>
        /// <remarks>
        /// <para>
        /// There is a list for every MLineStyle element, and every list contains an array of real values
        /// that parametrize the start and end point of every element of the style.
        /// </para>
        /// <para>
        /// The first value (index 0) represents the distance from the segment vertex along the miter vector to the
        /// point where the line element's path intersects the miter vector.<br />
        /// The second value (index 1) is the distance along the line element's direction from the point,
        /// defined by the first value, to the actual start of the line element.<br />
        /// The successive values list the start and stop points of the line element breaks or cuts in this segment of the multiline.
        /// </para>
        /// </remarks>
        public List<double>[] Distances
        {
            get { return this.distances; }
        }

        #endregion

        #region overrides

        /// <summary>
        /// Converts the value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            return string.Format("{0}: ({1})", "MLineVertex", this.position);
        }

        /// <summary>
        /// Creates a new MLineVertex that is a copy of the current instance.
        /// </summary>
        /// <returns>A new MLineVertex that is a copy of this instance.</returns>
        public object Clone()
        {
            List<double>[] copyDistances = new List<double>[this.distances.Length];
            for (int i = 0; i < this.distances.Length; i++)
            {
                copyDistances[i] = new List<double>();
                copyDistances[i].AddRange(this.distances[i]);
            }
            return new MultiLineVertex(this.position, this.direction, this.miter, copyDistances);
        }

        #endregion
    }
}
