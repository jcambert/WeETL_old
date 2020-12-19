﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.Observables.Dxf.Tables;

namespace WeETL.Observables.Dxf.Collections
{
    class TableObjects
    {
    }/// <summary>
     /// Represents a list of table objects
     /// </summary>
     /// <typeparam name="T"><see cref="TableObject">TableObject</see>.</typeparam>
    public abstract class TableObjects<T> :
        DxfObject,
        IEnumerable<T> where T : TableObject
    {
        #region private fields

        protected readonly Dictionary<string, T> list;
        protected Dictionary<string, List<DxfObject>> references;

        #endregion

        #region constructor

        protected TableObjects(DxfDocument document, /*string codeName, */string handle)
            : base(/*codeName*/)
        {
            
            this.list = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);
            this.references = new Dictionary<string, List<DxfObject>>(StringComparer.OrdinalIgnoreCase);
            this.Owner = document.Handle;

            if (string.IsNullOrEmpty(handle))
            {
                this.GetOwner<DxfDocument>().NumHandles = base.AssignHandle(this.GetOwner<DxfDocument>().NumHandles);
            }
            else
            {
                this.Handle = handle;
            }

            this.GetOwner<DxfDocument>().AddedObjects.Add(this.Handle, this);
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets a table object from the list by name.
        /// </summary>
        /// <param name="name">Table object name.</param>
        /// <returns>The table object with the specified name.</returns>
        /// <remarks>Table object names are case insensitive.</remarks>
        public T this[string name]=> this.list.TryGetValue(name, out T item) ? item : null;


        /// <summary>
        /// Gets the table object list.
        /// </summary>
        public ICollection<T> Items=> this.list.Values; 
        

        /// <summary>
        /// Gets the ObjectTable names.
        /// </summary>
        public ICollection<string> Names=>  this.list.Keys; 
        

        /// <summary>
        /// Gets the number of table objects.
        /// </summary>
        public int Count=> this.list.Count; 


        #endregion

        #region internal properties

        /// <summary>
        /// Gets the <see cref="DxfObject">dxf objects</see> referenced by a T.
        /// </summary>
        internal Dictionary<string, List<DxfObject>> References=> this.references; 
        

        #endregion

        #region public methods

        /// <summary>
        /// Gets the <see cref="DxfObject">dxf objects</see> referenced by a T.
        /// </summary>
        /// <param name="name">Table object name.</param>
        /// <returns>The list of DxfObjects that reference the specified table object.</returns>
        /// <remarks>
        /// If there is no table object with the specified name in the list the method an empty list.<br />
        /// The Groups collection method GetReferences will always return an empty list since there are no DxfObjects that references them.
        /// </remarks>
        public List<DxfObject> GetReferences(string name)
        {
            if (!this.Contains(name))
            {
                return new List<DxfObject>();
            }

            return new List<DxfObject>(this.references[name]);
        }

        /// <summary>
        /// Gets the <see cref="DxfObject">dxf objects</see> referenced by a T.
        /// </summary>
        /// <param name="item">Table object.</param>
        /// <returns>The list of DxfObjects that reference the specified table object.</returns>
        /// <remarks>
        /// If there is no table object with the specified name in the list the method an empty list.<br />
        /// The Groups collection method GetReferences will always return an empty list since there are no DxfObjects that references them.
        /// </remarks>
        public List<DxfObject> GetReferences(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return this.GetReferences(item.Name);
        }

        /// <summary>
        /// Checks if a table object already exists in the list. 
        /// </summary>
        /// <param name="name">Table object name.</param>
        /// <returns>True is a table object exists with the specified name, false otherwise.</returns>
        public bool Contains(string name)=> this.list.ContainsKey(name);
        

        /// <summary>
        /// Checks if a table object already exists in the list. 
        /// </summary>
        /// <param name="item">Table object.</param>
        /// <returns>True is a table object exists, false otherwise.</returns>
        public bool Contains(T item)=>this.list.ContainsValue(item);
        

        /// <summary>
        /// Gets the table object associated with the specified name.
        /// </summary>
        /// <param name="name"> The name of the table object to get.</param>
        /// <param name="item">When this method returns, contains the table object associated with the specified name, if the key is found;
        /// otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
        /// <returns>True if the table contains an element with the specified name; otherwise, false.</returns>
        public bool TryGetValue(string name, out T item)
        {
            return this.list.TryGetValue(name, out item);
        }

        /// <summary>
        /// Adds a table object to the list.
        /// </summary>
        /// <param name="item"><see cref="TableObject">Table object</see> to add to the list.</param>
        /// <returns>
        /// If a table object already exists with the same name as the instance that is being added the method returns the existing table object,
        /// if not it will return the new table object.
        /// </returns>
        public T Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return this.Add(item, true);
        }

        internal abstract T Add(T item, bool assignHandle);

        /// <summary>
        /// Removes a table object.
        /// </summary>
        /// <param name="name">Table object name to remove from the document.</param>
        /// <returns>True is the table object has been successfully removed, or false otherwise.</returns>
        /// <remarks>Reserved table objects or any other referenced by objects cannot be removed.</remarks>
        public abstract bool Remove(string name);

        /// <summary>
        /// Removes a table object.
        /// </summary>
        /// <param name="item"><see cref="TableObject">Table object</see> to remove from the document.</param>
        /// <returns>True is the table object has been successfully removed, or false otherwise.</returns>
        /// <remarks>Reserved table objects or any other referenced by objects cannot be removed.</remarks>
        public abstract bool Remove(T item);

        /// <summary>
        /// Removes all table objects that are not reserved and have no references.
        /// </summary>
        public void Clear()
        {
            string[] names = new string[this.list.Count];
            this.list.Keys.CopyTo(names, 0);
            foreach (string o in names)
            {
                this.Remove(o);
            }
        }

        #endregion

        #region implements IEnumerator<T>

        /// <summary>
        /// Returns an enumerator that iterates through the table object collection.
        /// </summary>
        /// <returns>An enumerator for the table object collection.</returns>
        public IEnumerator<T> GetEnumerator()=> this.list.Values.GetEnumerator();
        

        IEnumerator IEnumerable.GetEnumerator()=> GetEnumerator();



        #endregion
    }
}
