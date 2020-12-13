using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.DependencyInjection;
using WeETL.Numerics;
using WeETL.Observables.Dxf.Entities;
using WeETL.Observables.Dxf.Units;
using WeETL.Utilities;

namespace WeETL.Observables.Dxf.IO
{


    public interface IReaderFactory
    {
        IReader CreateReaderSection(string sectionName, IDxfDocument document);
    }
    public interface IReader
    {
        void Read((int, string) code);
        IDxfDocument Document { get; set; }
    }

    internal static class EntityGroupCode
    {
        public const int EntityType = 0;
        public const int HeaderVar = 9;
        public const int Handle = 5;
        public const int ApplicationDefined = 102;
        public const int Owner = 330;
    }
    internal abstract class AbstractReader : IReader
    {
        //TODO Making abstract after all Entities are implemented
        protected virtual DxfObject DxfObject { get; set; }
        public IDxfDocument Document { get; set; }
        internal IServiceProvider ServiceProvider { get; }

        static Action<DxfObject, string> ReadHandle = (line, value) => { line.Handle = value; };
        static Action<DxfObject, string> ReadApplicationDefined = (line, value) => { line.Owner = value; };//TODO
        static Action<DxfObject, string> ReadOwner = (line, value) => { line.Owner = value; };
        Action<DxfObject, string> fn;
        int _currentCode = -1;

        #region ctor
        public AbstractReader(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        #endregion


        public virtual void Read((int, string) code)
        {
            //Thorw exception if null when all Entities are implemented
            if (DxfObject == null) return;

            fn = code.Item1 switch
            {
                EntityGroupCode.Handle => ReadHandle,
                EntityGroupCode.ApplicationDefined => ReadApplicationDefined,
                EntityGroupCode.Owner => ReadOwner,
                _ => (Line, value) => { }// Nothing TO DO
            };
            fn(DxfObject, code.Item2);
            _currentCode = code.Item1;
        }
        protected bool ReadString(string value, Action<string> onset)
        {
            if (value!=null)
            {
                onset?.Invoke(value);
                return true;
            }
            return false;
        }

        protected bool ReadDouble(string value, Action<double> onset)
        {
            if (double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                onset?.Invoke(result);
                return true;
            }
            return false;
        }
        protected bool ReadInt(string value, Action<int> onset)
        {
            if (int.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                onset?.Invoke(result);
                return true;
            }
            return false;
        }
        protected bool ReadOnOff(string value, Action<OnOff> onset)
        {
            if (int.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                onset?.Invoke(result>1?OnOff.ON:OnOff.OFF);
                return true;
            }
            return false;
        }
        protected bool ReadShort(string value, Action<short> onset)
        {
            if (short.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                onset?.Invoke(result);
                return true;
            }
            return false;
        }
        protected bool ReadByte(string value, Action<byte> onset)
        {
            if (byte.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                onset?.Invoke(result);
                return true;
            }
            return false;
        }
        protected bool ReadTimeSpan(string value, Action<TimeSpan> onset)
        {
            if (double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                onset?.Invoke(TimeSpan.FromHours( result));
                return true;
            }
            return false;
        }
        protected bool ReadHex(string value, Action<double> onset)
        {
            throw new NotImplementedException();
        }
        protected bool ReadDateTime(string value, Action<DateTime> onset)
        {
            double res=0.0;
            if (!ReadDouble(value, result => res=result))
            {
                return false;
            }
            try
            {
                onset?.Invoke(JulianToDateTime(res));
                return true;
            }
            catch
            {

            return false;
            }
        }
        DateTime JulianToDateTime(double julianDate)
        {
            double unixTime = (julianDate - 2440587.5) * 86400;

            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTime).ToLocalTime();

            return dtDateTime;
        }
    }

    internal abstract class AbstractVector2Reader : AbstractReader
    {
        double x, y;
        protected AbstractVector2Reader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        public override void Read((int, string) code)
        {
            if (code.Item1 >= 10 && code.Item1 < 20) ReadDouble(code.Item2, result => x = result);
            if (code.Item1 >= 20 && code.Item1 < 30) 
       
            {
                ReadDouble(code.Item2, result => y = result);
                SetValue(new Vector2(x, y));
            }
        }
        protected abstract void SetValue(Vector2 value);
    }
    internal abstract class AbstractVector3Reader : AbstractReader
    {
        double x, y, z;
        protected AbstractVector3Reader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        public override void Read((int, string) code)
        {
            if (code.Item1 >= 10 && code.Item1 < 20) ReadDouble(code.Item2, result => x = result);
            if (code.Item1 >= 20 && code.Item1 < 30) ReadDouble(code.Item2, result => y = result);
            if (code.Item1 >= 30 && code.Item1 < 40)
            {
                ReadDouble(code.Item2, result => z = result);
                SetValue( new Vector3(x, y, z));
            }
        }
        protected abstract void SetValue(Vector3 value);
    }

    internal abstract class AbstractAciColorReader : AbstractReader
    {
        protected AbstractAciColorReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        public override void Read((int, string) code)
        {
            byte index = 0;
            if (!ReadByte(code.Item2, result => index = result))
                SetValue(AciColor.FromIndex(0));
            else
                SetValue(AciColor.FromIndex(index));
        }
        protected abstract void SetValue(AciColor color);
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DxfSectionAttribute : Attribute, INamed
    {
        public DxfSectionAttribute(string name)
        {
            Check.NotEmpty(name, nameof(name));
            this.Name = name;
        }
        public string Name { get; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class DxfEntityTypeAttribute : Attribute, INamed
    {
        public DxfEntityTypeAttribute(string name)
        {
            Check.NotEmpty(name, nameof(name));
            this.Name = name;
        }

        public string Name { get; }
    }

    public abstract class SectionReader : IReader
    {
        IReader currentReader;
        public SectionReader(IServiceProvider serviceProvider,ILogger<SectionReader> logger)
        {
            ServiceProvider = serviceProvider;
            Logger = logger;
        }
        public ILogger<SectionReader> Logger { get; }
        public IServiceProvider ServiceProvider { get; }
        public IDxfDocument Document { get; set; }
        protected virtual int GroupCode => EntityGroupCode.EntityType;
        protected virtual bool CanIgnore => true;
        public virtual void Read((int, string) code)
        {
            if (code.Item1 == GroupCode)
            {
                currentReader = ServiceProvider.ResolveKeyed<IReader, DxfEntityTypeAttribute>(code.Item2);

                if (!CanIgnore)
                    Check.NotNull(currentReader, code.Item2);
                if (currentReader != null)
                    currentReader.Document = Document;
                else
                    Logger.LogWarning($"{this.GetType().Name} with Code {code} was not handled");
            }
            else
            {
                if (!CanIgnore)
                    _ = currentReader ?? throw new Exception("Malformed dxf");

                if (currentReader != null)
                    currentReader.Read(code);

            }
        }
    }





    public class ReaderFactory : IReaderFactory
    {
        public ReaderFactory(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; }

        public IReader CreateReaderSection(string sectionName, IDxfDocument document)
        {
            Check.NotNull(sectionName, nameof(sectionName));
            Check.NotNull(document, nameof(document));
            var service = ServiceProvider.ResolveKeyed<IReader, DxfSectionAttribute>(sectionName);
            service.Document = document;

            return service;
        }



    }
}
