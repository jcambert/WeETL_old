using WeETL.Numerics;
using WeETL.Observables.Dxf.Tables;
using WeETL.Utilities;

namespace WeETL.Observables.Dxf.Entities
{
    public partial class Text 
    {


        #region ctor
        public Text(string text, Vector3 position) : this(text,position,1.0, TextStyle.DefaultName)
        {
            
        }
        /// <summary>
        /// Initializes a new instance of the <c>Text</c> class.
        /// </summary>
        public Text()
            : this(string.Empty, Vector3.Zero, 1.0, TextStyle.DefaultName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <c>Text</c> class.
        /// </summary>
        /// <param name="text">Text string.</param>
        public Text(string text)
            : this(text, Vector2.Zero, 1.0, TextStyle.DefaultName)
        {
        }

        

        /// <summary>
        /// Initializes a new instance of the <c>Text</c> class.
        /// </summary>
        /// <param name="text">Text string.</param>
        /// <param name="position">Text <see cref="Vector3">position</see> in world coordinates.</param>
        /// <param name="height">Text height.</param>
        public Text(string text, Vector3 position, double height)
            : this(text, position, height, TextStyle.DefaultName)
        {
        }


   

        /// <summary>
        /// Initializes a new instance of the <c>Text</c> class.
        /// </summary>
        /// <param name="text">Text string.</param>
        /// <param name="position">Text <see cref="Vector3">position</see> in world coordinates.</param>
        /// <param name="height">Text height.</param>
        /// <param name="style">Text <see cref="TextStyle">style</see>.</param>
        public Text(string text, Vector3 position, double height, string style)
            : base()
        {
            this.Value=Check.NotNull( text,nameof(text));
            this.FirstAlignmentPoint = position;
            this.VerticalJustification = VerticalTextJustification.BaseLine;
            this.HorizontalJustification = HorizontalTextJustification.Middle;
            this.Normal = Vector3.UnitZ;
            
           
            this.Style = style ??  TextStyle.DefaultName;
        
            this.Height =Check.NotNegative( height,nameof(height));
            this.ScaleX = 1.0;
            this.Oblique= 0.0;
            this.Rotation = 0.0;
            
        }
        #endregion

    }
}
