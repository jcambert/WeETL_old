using System;
using System.Globalization;
using WeETL.Utilities;

namespace WeETL.Observables.Dxf
{
    public sealed class XDataRecord
    {
        #region private variables
        private readonly XDataCode _code;
        private readonly object _value;
        #endregion
        #region constants
        private static Func<XDataCode, object, string> ControlAppReg = (code, value) => "An application registry cannot be an extended data record.";
        private static Func<XDataCode, object, string> ControlBinaryData = (code, value) =>
        {
            byte[] bytes = value as byte[];
            if (bytes.IsNullOrEmpty()) return "The value of a XDataCode.BinaryData must be a byte array.";
            if (bytes.Length > 127) return "The maximum length of a XDataCode.BinaryData is 127, if larger divide the data into multiple XDataCode.BinaryData records.";
            return string.Empty;
        };
        private static Func<XDataCode, object, string> ControlString = (code, value) =>
        {
            string v = value as string;

            if (v == null) return "The value of a XDataCode.ControlString must be a string.";
            if (!string.Equals(v, "{") && !string.Equals(v, "}")) return "The only valid values of a XDataCode.ControlString are { or }.";
            return string.Empty;
        };
        private static Func<XDataCode, object, string> ControlDatabaseHandle = (code, value) =>
        {
            string handle = value as string;

            if (handle == null) return "The value of a XDataCode.DatabaseHandle must be an hexadecimal number.";
            if (!long.TryParse(handle, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var test))
                return "The value of a XDataCode.DatabaseHandle must be an hexadecimal number.";

            return string.Empty;
        };
        private static Func<XDataCode, object, string> ControlTypeDouble = (code, value) =>
        {
            if (!(value is double)) return $"The value of a XDataCode.{code} must be a {typeof(double)}.";
            return string.Empty;
        };
        private static Func<XDataCode, object, string> ControlTypeShort = (code, value) =>
        {
            if (!(value is short)) return $"The value of a XDataCode.{code} must be a {typeof(short)}.";
            return string.Empty;
        };
        private static Func<XDataCode, object, string> ControlTypeInt = (code, value) =>
        {
            if (!(value is int)) return $"The value of a XDataCode.{code} must be a {typeof(int)}.";
            return string.Empty;
        };
        private static Func<XDataCode, object, string> ControlTypeString = (code, value) =>
        {
            if (!(value is string)) return $"The value of a XDataCode.{code} must be a {typeof(string)}.";
            return string.Empty;
        };
        #endregion
        #region ctor
        public XDataRecord(XDataCode code, object value)
        {
            Check.NotNull(value, nameof(value));
            Func<XDataCode, object, string> fn = code switch
            {
                XDataCode.AppReg => ControlAppReg,
                XDataCode.BinaryData => ControlBinaryData,
                XDataCode.ControlString => ControlString,
                XDataCode.DatabaseHandle => ControlDatabaseHandle,
                XDataCode.Distance | 
                XDataCode.Real |
                XDataCode.RealX |
                XDataCode.RealY  |
                XDataCode.RealZ |
                XDataCode.WorldSpacePositionX  |
                XDataCode.WorldSpacePositionY  |
                XDataCode.WorldSpacePositionZ  |
                XDataCode.ScaleFactor |
                XDataCode.WorldDirectionX  |
                XDataCode.WorldDirectionY |
                XDataCode.WorldDirectionZ  |
                XDataCode.WorldSpaceDisplacementX |
                XDataCode.WorldSpaceDisplacementY |
                XDataCode.WorldSpaceDisplacementZ => ControlTypeDouble,
                XDataCode.Int16=>ControlTypeShort,
                XDataCode.Int32=>ControlTypeInt,
                XDataCode.LayerName| XDataCode.String=>ControlTypeString,

               _ => (code, value) => string.Empty
            };
            var result = fn(code, value) ;
            if (!result.IsNullOrEmpty()) throw new ArgumentException(result);
            _code = code;
            _value = value;
        }
        #endregion
        #region public properties
        public XDataCode Code => _code;
        public object Value => _value;
        #endregion
    }
}
