using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WeETL.Numerics;
using WeETL.Observables.Dxf.Header;

namespace WeETL.Observables.Dxf
{
    public class DxfHeaderValue
    {
        readonly List<int> _groupCodes = new List<int>();
        readonly List<object> _values = new List<object>();
        internal DxfHeaderValue()
        {

        }

        public DxfHeaderValue(string name, int groupCode, object value)
        {
            this.Name = name;
            Add(groupCode, value);

        }
        internal void Add(int groupCode, object value)
        {
            if (value is Vector3)
            {
                var v = (Vector3)value;
                for (int i = 0; i < 3; i++)
                {
                    _groupCodes.Add(groupCode + (i * 10));

                }
                _values.Add(v.X); _values.Add(v.Y); _values.Add(v.Z);
            }
            else if (value.GetType().IsEnum)
            {
                _groupCodes.Add(groupCode);
                _values.Add((int)value);
            }

            else
            {
                _groupCodes.Add(groupCode);
                _values.Add(value);
            }
        }
        public string Name { get; internal set; }

        internal List<int> GroupCodes => _groupCodes;
        internal List<object> Values => _values;
        public object Value
        {
            get
            {
                if (this.IsVector3()) return new Vector3((double)_values[0], (double)_values[1], (double)_values[2]);
                return _values.First();
            }
            set
            {
                _values.Clear();
                if (value is Vector3)
                {
                    var v = (Vector3)value;
                    _values.Add(v.X);
                    _values.Add(v.Y);
                    _values.Add(v.Z);
                }
                else
                {
                    _values.Add(value);
                }

            }
        }
        public override string ToString()
        {

            var res = $"{Utilities.GroupCodeVariableName}\n{Name}\n";
            for (int i = 0; i < _values.Count; i++)
            {
                res += $"{_groupCodes[i]}\n{_values[i].ToString()}\n";

            }


            return res;
        }
    }

    public partial class DxfHeader
    {
        internal readonly Dictionary<string, DxfHeaderValue> Codes = new Dictionary<string, DxfHeaderValue>(StringComparer.OrdinalIgnoreCase);
        public DxfHeader(IDxfVersion version)
        {
            //@see DxfHeaderCode.tt
            Initialize();
            AcadVer = version;
        }
        public void SetValue(string key, DxfHeaderValue value) => Codes[key] = value;
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Utilities.WriteStartSection);
            sb.Append(Utilities.WriteStartHeader);
            foreach (var key in Codes.Keys.OrderBy(k=>k))
            {
                sb.Append(Codes[key].ToString());
            }
            sb.Append(Utilities.WriteEndSection);
            return sb.ToString();
        }
    }
}
