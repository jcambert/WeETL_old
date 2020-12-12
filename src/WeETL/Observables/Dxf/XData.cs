using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.Observables.Dxf.Tables;
using WeETL.Utilities;

namespace WeETL.Observables.Dxf
{
    public sealed class XData : ICloneable
    {
        #region private variables
        private readonly ApplicationRegistry _appReg;
        private readonly List<XDataRecord> _data;
        #endregion
        #region ctor
        public XData(ApplicationRegistry appReg)
        {
            Check.NotNull(appReg, nameof(appReg));
            this._appReg = appReg;
            this._data = new List<XDataRecord>();
        }
        #endregion
        #region public properties
        public ApplicationRegistry ApplicationRegistry => _appReg;
        public List<XDataRecord> Datas { get; set; }
        #endregion

        #region overrides
        public override string ToString() => _appReg.Name;
        #endregion

        #region ICloneable
        public object Clone()
        {
            XData xdata = new XData((ApplicationRegistry)_appReg.Clone());
            foreach (XDataRecord record in Datas)
            {
                xdata.Datas.Add(new XDataRecord(record.Code, record.Value));
            }

            return xdata;
        }
        #endregion
    }
}
