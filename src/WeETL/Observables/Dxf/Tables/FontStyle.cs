using System;

namespace WeETL.Observables.Dxf.Tables
{ /// <summary>
  /// Represent the font character formatting, such as italic, bold, or regular.
  /// </summary>
    [Flags]
    public enum FontStyle
    {
        /// <summary>
        /// Regular.
        /// </summary>
        Regular = 0,

        /// <summary>
        /// Italic or oblique.
        /// </summary>
        Italic = 1,

        /// <summary>
        /// Bold.
        /// </summary>
        Bold = 2
    }
}
