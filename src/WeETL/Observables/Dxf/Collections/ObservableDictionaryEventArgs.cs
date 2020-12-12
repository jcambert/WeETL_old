using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeETL.Observables.Dxf.Collections
{
    /// <summary>
    /// Represents the arguments thrown by the <c>ObservableDictionaryEventArgs</c> events.
    /// </summary>
    /// <typeparam name="TKey">Type of items.</typeparam>
    /// <typeparam name="TValue">Type of items.</typeparam>
    public class ObservableDictionaryEventArgs<TSender, TKey, TValue> :
        EventArgs
    {

        #region constructor

        /// <summary>
        /// Initializes a new instance of <c>ObservableDictionaryEventArgs</c>.
        /// </summary>
        /// <param name="item">Item that is being added or removed from the dictionary.</param>
        public ObservableDictionaryEventArgs(TSender sender, KeyValuePair<TKey, TValue> item, bool cancel = false)
        {
            Sender = sender;
            this.Item = item;
            this.Cancel = cancel;
        }

        #endregion

        #region public properties
        /// <summary>
        /// The object that emit event
        /// </summary>
        public TSender Sender { get;}
        /// <summary>
        /// Get the item that is being added to or removed from the dictionary.
        /// </summary>
        public KeyValuePair<TKey, TValue> Item
        {
            get;
        }

        /// <summary>
        /// Gets or sets if the operation must be canceled.
        /// </summary>
        /// <remarks>This property is used by the OnBeforeAdd and OnBeforeRemove events to cancel the add or remove operations.</remarks>
        public bool Cancel { get; set; }

        #endregion
    }
}
