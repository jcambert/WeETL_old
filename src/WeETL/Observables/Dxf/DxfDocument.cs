using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.Observables.Dxf.Collections;

namespace WeETL.Observables.Dxf
{
    public interface IDxfDocument
    {
        IDxfHeader Header { get; }
    }
    public class DxfDocument:DxfObject,IDxfDocument
    {
        #region private variables
        private long _numHandles;
        internal readonly ObservableDictionary<string, DxfObject> AddedObjects=new ObservableDictionary<string, DxfObject>();
        #endregion
        #region ctor
        public DxfDocument(IDxfHeader header):base(DxfObjectCode.Document)
        {
            this.Header = header;
        }
        #endregion
        #region public propreties
        public IDxfHeader Header { get; }
        #endregion
        #region internal properties
        #region internal properties

        /// <summary>
        /// Gets or sets the number of handles generated, this value is saved as an hexadecimal in the drawing variables HandleSeed property.
        /// </summary>
        internal long NumHandles
        {
            get { return _numHandles; }
            set
            {
                Header.HandleSeed = value.ToString("X");
                _numHandles = value;
            }
        }

        #endregion
        #endregion
        #region overrides
        public override string ToString()
        {
            return Header.ToString();
        }
        #endregion
    }
}
