using System.Reactive.Subjects;
using WeETL.Observables.Dxf.Collections;
using WeETL.Utilities;

namespace WeETL.Observables.Dxf
{
    public abstract class DxfObject
    {
        

        #region ctor
        public DxfObject(/*string codeName*/)
        {
           // Check.NotNull(codeName, nameof(codeName));
            //CodeName = codeName;
            XData = new XDataDictionary();
        }
        #endregion
        #region public properties
        public string Handle { get; internal set; }
        public string Owner { get; internal set; }

        
        public abstract string CodeName { get; /*protected set; */}
        public XDataDictionary XData { get; }
        #endregion

        #region internal methods
        /// <summary>
        /// Assigns a handle to the object based in a integer counter.
        /// </summary>
        /// <param name="entityNumber">Number to assign to the actual object.</param>
        /// <returns>Next available entity number.</returns>
        /// <remarks>
        /// Some objects might consume more than one, is, for example, the case of polylines that will assign
        /// automatically a handle to its vertexes. The entity number will be converted to an hexadecimal number.
        /// </remarks>
        internal virtual long AssignHandle(long entityNumber)
        {
            this.Handle = entityNumber.ToString("X");
            return entityNumber + 1;
        }
        #endregion
    }
}
