using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using WeETL.Utilities;

namespace WeETL.Observables.Dxf.Tables
{
    public abstract class TableObject : DxfObject,
        ICloneable,
        IComparable,
        IComparable<TableObject>,
        IEquatable<TableObject>
    {

        #region private fields

        private static readonly char[] _invalidCharacters = { '\\', '/', ':', '*', '?', '"', '<', '>', '|', ';', ',', '=', '`' };
        private bool _reserved;
        private string _name;
        private ISubject<TableObjectChangedEventArgs<TableObject, string>> _onNameChanged = new Subject<TableObjectChangedEventArgs<TableObject, string>>();
        #endregion

        #region ctor
        public TableObject(string name,/* string codeName, */bool checkName):base(/*codeName*/)
        {
            if (checkName && !IsValidName(name))
                throw new ArgumentException($"The name should be at least one character long and the following characters {string.Join("",_invalidCharacters)} are not supported.");
            Name = name;
        }
        #endregion

        #region public methods

        /// <summary>
        /// Checks if a string is valid as a table object name.
        /// </summary>
        /// <param name="name">String to check.</param>
        /// <returns>True if the string is valid as a table object name, or false otherwise.</returns>
        public static bool IsValidName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            return name.IndexOfAny(_invalidCharacters) == -1;
        }

        #endregion
        #region internal methods

        /// <summary>
        /// Hack to change the table name without having to check its name. Some invalid characters are used for internal purposes only.
        /// </summary>
        /// <param name="newName">Table object new name.</param>
        internal void SetName(string newName, bool checkName)
        {
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentNullException(nameof(newName));
            


            if (this.IsReserved)
            throw new ArgumentException("Reserved table objects cannot be renamed.", nameof(newName));
            

            if (string.Equals(this._name, newName, StringComparison.OrdinalIgnoreCase))
            return;
            

            if (checkName && !IsValidName(newName))
                    throw new ArgumentException("The following characters \\<>/?\":;*|,=` are not supported for table object names.", nameof(newName));
            _onNameChanged.OnNext(new TableObjectChangedEventArgs<TableObject,string>(this,_name, newName));
            this._name = newName;
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets the name of the table object.
        /// </summary>
        /// <remarks>Table object names are case insensitive.</remarks>
        public string Name
        {
            get => _name;
            set { this.SetName(value, true); }
        }

        /// <summary>
        /// Gets if the table object is reserved and cannot be deleted.
        /// </summary>
        public bool IsReserved
        {
            get => _reserved;
            internal set { this._reserved = value; }
        }

        /// <summary>
        /// Gets the array of characters not supported as table object names.
        /// </summary>
        public static char[] InvalidCharacters=>_invalidCharacters.ToArray();

        public IObservable<TableObjectChangedEventArgs<TableObject, string>> OnNamedChanged => _onNameChanged.AsObservable();

        #endregion
        #region ICloneable
        /// <summary>
        /// Creates a new table object that is a copy of the current instance.
        /// </summary>
        /// <param name="newName">TableObject name of the copy.</param>
        /// <returns>A new table object that is a copy of this instance.</returns>
        public abstract TableObject Clone(string newName);

        /// <summary>
        /// Creates a new table object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new table object that is a copy of this instance.</returns>
        public abstract object Clone();

        #endregion
        #region IComparable
        public int CompareTo(object obj)
       => CompareTo((TableObject)obj);

        public int CompareTo(TableObject other)
        {
            Check.NotNull(other, nameof(other));
            return this.GetType() == other.GetType() ? string.Compare(this.Name, other.Name, StringComparison.OrdinalIgnoreCase) : 0;
        }

        #endregion
        #region IEquatable
        public override bool Equals(object other)
        {
            if (other == null || GetType() != other.GetType()) return false;
            return Equals((TableObject)other);
            
        }
        public bool Equals(TableObject other)
        {
            if (other == null) return false;
            return string.Equals(this.Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }
        #endregion

    }
}
