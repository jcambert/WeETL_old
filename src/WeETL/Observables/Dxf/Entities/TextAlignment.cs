namespace WeETL.Observables.Dxf.Entities
{
    /// <summary>
    /// Defines the text alignment.
    /// </summary>
    public enum TextAlignment
    {
        /// <summary>
        /// Top left.
        /// </summary>
        TopLeft,

        /// <summary>
        /// Top center.
        /// </summary>
        TopCenter,

        /// <summary>
        /// Top right.
        /// </summary>
        TopRight,

        /// <summary>
        /// Middle left.
        /// </summary>
        MiddleLeft,

        /// <summary>
        /// Middle center (uses the center of the text as uppercase characters).
        /// </summary>
        MiddleCenter,

        /// <summary>
        /// Middle right.
        /// </summary>
        MiddleRight,

        /// <summary>
        /// Bottom left.
        /// </summary>
        BottomLeft,

        /// <summary>
        /// Bottom center.
        /// </summary>
        BottomCenter,

        /// <summary>
        /// Bottom right.
        /// </summary>
        BottomRight,

        /// <summary>
        /// Baseline left.
        /// </summary>
        BaselineLeft,

        /// <summary>
        /// Baseline center.
        /// </summary>
        BaselineCenter,

        /// <summary>
        /// Baseline right.
        /// </summary>
        BaselineRight,

        /// <summary>
        /// Aligned.
        /// </summary>
        /// <remarks>The text width factor will be automatically adjusted so the text will fit in the specified width.</remarks>
        Aligned,

        /// <summary>
        /// Middle (uses the center of the text including descenders).
        /// </summary>
        Middle,

        /// <summary>
        /// Fit.
        /// </summary>
        /// <remarks>The text height will be automatically adjusted so the text will fit in the specified width.</remarks>
        Fit
    }
}
