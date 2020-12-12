using System;
using WeETL.Observables.Dxf.Collections;
using WeETL.Utilities;

namespace WeETL.Observables.Dxf.Tables
{
    public sealed class ApplicationRegistry:TableObject
    {
        #region constants
        /// <summary>
        /// Default application registry name.
        /// </summary>
        public const string DefaultName = "ACAD";

        /// <summary>
        /// Gets the default application registry.
        /// </summary>
        public static ApplicationRegistry Default
        {
            get { return new ApplicationRegistry(DefaultName); }
        }
        #endregion

        #region ctors
        public ApplicationRegistry(string name):this(name,true){}
        internal ApplicationRegistry(string name, bool checkName) : base(name, DxfObjectCode.AppId, checkName)
        {
            Check.NotNull(name, nameof(name));
            this.IsReserved = name.Equals(DefaultName, StringComparison.OrdinalIgnoreCase);
        }
        #endregion

     
        #region overrides

        /// <summary>
        /// Creates a new ApplicationRegistry that is a copy of the current instance.
        /// </summary>
        /// <param name="newName">ApplicationRegistry name of the copy.</param>
        /// <returns>A new ApplicationRegistry that is a copy of this instance.</returns>
        public override TableObject Clone(string newName)
        {
            ApplicationRegistry copy = new ApplicationRegistry(newName);

            foreach (XData data in this.XData.Values)
                copy.XData.Add((XData)data.Clone());

            return copy;
        }

        /// <summary>
        /// Creates a new ApplicationRegistry that is a copy of the current instance.
        /// </summary>
        /// <returns>A new ApplicationRegistry that is a copy of this instance.</returns>
        public override object Clone()
        {
            return this.Clone(this.Name);
        }

        #endregion
    }
}
