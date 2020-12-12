using System;

namespace WeETL.Observables.Dxf.Tables
{
    /// <summary>
    /// Event data for changes or substitutions of table objects in entities or other tables.
    /// </summary>
    /// <typeparam name="T">A table object</typeparam>
    public sealed class TableObjectChangedEventArgs<TSender,T> :
        EventArgs
    {
 

        #region constructor

        /// <summary>
        /// Initializes a new instance of <c>TableObjectModifiedEventArgs</c>.
        /// </summary>
        /// <param name="oldTable">The previous table object.</param>
        /// <param name="newTable">The new table object.</param>
        public TableObjectChangedEventArgs(TSender sender, T oldTable, T newTable)
        {
            Sender = sender;
            OldValue = oldTable;
            NewValue = newTable;
        }


        #endregion

        #region public properties
        /// <summary>
        /// Get the sender of event
        /// </summary>
        public TSender Sender { get; }

        /// <summary>
        /// Gets the previous property value.
        /// </summary>
        public T OldValue
        {
            get;

        }

        /// <summary>
        /// Gets or sets the new property value.
        /// </summary>
        public T NewValue
        {
            get;
        }

        #endregion
    }
}
