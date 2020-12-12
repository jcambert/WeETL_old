using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.Numerics;
using WeETL.Observables.Dxf.Tables;

namespace WeETL.Observables.Dxf.Entities
{
    public class Text : EntityObject
    {
        #region private fields

        private TextAlignment alignment;
        private Vector3 position;
        private double obliqueAngle;
        private TextStyle style;
        private string text;
        private double height;
        private double widthFactor;
        private double width;
        private double rotation;
        private bool isBackward;
        private bool isUpsideDown;

        #endregion

        #region ctor
        /// <summary>
        /// Initializes a new instance of the <c>Text</c> class.
        /// </summary>
        public Text()
            : this(string.Empty, Vector3.Zero, 1.0, TextStyle.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <c>Text</c> class.
        /// </summary>
        /// <param name="text">Text string.</param>
        public Text(string text)
            : this(text, Vector2.Zero, 1.0, TextStyle.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <c>Text</c> class.
        /// </summary>
        /// <param name="text">Text string.</param>
        /// <param name="position">Text <see cref="Vector2">position</see> in world coordinates.</param>
        /// <param name="height">Text height.</param>
        public Text(string text, Vector2 position, double height)
            : this(text, new Vector3(position.X, position.Y, 0.0), height, TextStyle.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <c>Text</c> class.
        /// </summary>
        /// <param name="text">Text string.</param>
        /// <param name="position">Text <see cref="Vector3">position</see> in world coordinates.</param>
        /// <param name="height">Text height.</param>
        public Text(string text, Vector3 position, double height)
            : this(text, position, height, TextStyle.Default)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <c>Text</c> class.
        /// </summary>
        /// <param name="text">Text string.</param>
        /// <param name="position">Text <see cref="Vector2">position</see> in world coordinates.</param>
        /// <param name="height">Text height.</param>
        /// <param name="style">Text <see cref="TextStyle">style</see>.</param>
        public Text(string text, Vector2 position, double height, TextStyle style)
            : this(text, new Vector3(position.X, position.Y, 0.0), height, style)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <c>Text</c> class.
        /// </summary>
        /// <param name="text">Text string.</param>
        /// <param name="position">Text <see cref="Vector3">position</see> in world coordinates.</param>
        /// <param name="height">Text height.</param>
        /// <param name="style">Text <see cref="TextStyle">style</see>.</param>
        public Text(string text, Vector3 position, double height, TextStyle style)
            : base(/*EntityType.Text,*/ DxfObjectCode.Text)
        {
            this.text = text;
            this.position = position;
            this.alignment = TextAlignment.BaselineLeft;
            this.Normal = Vector3.UnitZ;
            if (style == null)
            {
                throw new ArgumentNullException(nameof(style));
            }
            this.style = style;
            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height), this.text, "The Text height must be greater than zero.");
            }
            this.height = height;
            this.width = 1.0;
            this.widthFactor = style.WidthFactor;
            this.obliqueAngle = style.ObliqueAngle;
            this.rotation = 0.0;
            this.isBackward = false;
            this.isUpsideDown = false;
        }
        #endregion

        #region overrides
        public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
