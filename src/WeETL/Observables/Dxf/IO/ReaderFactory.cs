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
        void Read<TType>((int, string) code) where TType:DxfBaseTypeAttribute;
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
    internal  class BaseReader
    {
        internal bool ReadString(string value, Action<string> onset) => Utilities.ReadString(value, onset);
        internal bool ReadDouble(string value, Action<double> onset) => Utilities.ReadDouble(value, onset);
        internal bool ReadInt(string value, Action<int> onset) => Utilities.ReadInt(value, onset);
        internal bool ReadOnOff(string value, Action<OnOff> onset) => Utilities.ReadOnOff(value, onset);
        internal bool ReadShort(string value, Action<short> onset) => Utilities.ReadShort(value, onset);
        internal bool ReadByte(string value, Action<byte> onset) => Utilities.ReadByte(value, onset);
        internal bool ReadTimeSpan(string value, Action<TimeSpan> onset) => Utilities.ReadTimeSpan(value, onset);
        internal bool ReadDateTime(string value, Action<DateTime> onset) => Utilities.ReadDateTime(value, onset);
        
    }
    internal abstract class AbstractReader :BaseReader, IReader
    {
        //TODO Making abstract after all Entities are implemented
        protected virtual DxfObject DxfObject { get; set; }
        public IDxfDocument Document { get; set; }
        protected IServiceProvider ServiceProvider { get; }
        protected ILogger<AbstractReader> Logger { get; }

        static Action<DxfObject, string> ReadHandle = (line, value) =>
        {
            var v = value.HexStringToInt();//Test if Hex String
            line.Handle = value;
        };
        static Action<DxfObject, string> ReadApplicationDefined = (line, value) => { line.Owner = value; };//TODO
        static Action<DxfObject, string> ReadOwner = (line, value) => { line.Owner = value; };
        Action<DxfObject, string> fn;
        int _currentCode = -1;
        public const string NOT_HANDLED = "NotHandled";
        #region ctor
        public AbstractReader(IServiceProvider serviceProvider, ILogger<AbstractReader> logger)
        {
            ServiceProvider = serviceProvider;
            Logger = logger;
        }

        #endregion


        public virtual void Read<TType>((int, string) code)
            where TType:DxfBaseTypeAttribute
        {
            //Thorw exception if null when all Entities are implemented
            if (DxfObject == null) return;

            fn = code.Item1 switch
            {
                EntityGroupCode.Handle => ReadHandle,
                EntityGroupCode.ApplicationDefined => ReadApplicationDefined,
                EntityGroupCode.Owner => ReadOwner,
                _ => (line, value) => { }// Nothing TO DO
            };
            
            fn(DxfObject, code.Item2);
            _currentCode = code.Item1;
        }
        
    }

    internal abstract class AbstractVector2Reader : AbstractReader
    {
        double x, y;
        protected AbstractVector2Reader(IServiceProvider serviceProvider, ILogger<AbstractVector2Reader> logger) : base(serviceProvider,logger)
        {
        }
        public override void Read<TTYpe>((int, string) code)
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
        readonly Vector3Reader v3Reader = new Vector3Reader();
        double x, y, z;
        protected AbstractVector3Reader(IServiceProvider serviceProvider, ILogger<AbstractVector3Reader> logger) : base(serviceProvider,logger)
        {
        }
        public override void Read<TType>((int, string) code)
        {
            v3Reader.Read(code, (range,v) => SetValue(v));
            
        }
        protected abstract void SetValue(Vector3 value);
    }

    internal class Vector3Reader :BaseReader
    {
        double x, y, z;
        int range;
        public Vector3Reader() 
        {
        }

        public void Read((int,string) code,Action<int,Vector3> setValue)
        {
            if (code.Item1 < 10 || code.Item1 > 39) return;
            range = (int)(((code.Item1 / 10.0) % 1) * 10);
            if (code.Item1 >= 10 && code.Item1 < 20) ReadDouble(code.Item2, result => x = result);
            if (code.Item1 >= 20 && code.Item1 < 30) ReadDouble(code.Item2, result => y = result);
            if (code.Item1 >= 30 && code.Item1 < 40)
            {
                ReadDouble(code.Item2, result => z = result);
                setValue(range,new Vector3(x, y, z));
            }
        }
    }

    internal abstract class AbstractAciColorReader : AbstractReader
    {
        protected AbstractAciColorReader(IServiceProvider serviceProvider, ILogger<AbstractAciColorReader> logger) : base(serviceProvider,logger)
        {
        }
        public override void Read<TType>((int, string) code)
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
    public class DxfSectionAttribute : System.Attribute, INamed
    {
        public DxfSectionAttribute(string name)
        {
            Check.NotEmpty(name, nameof(name));
            this.Name = name;
        }
        public string Name { get; }
    }

    
    public class DxfBaseTypeAttribute : System.Attribute, INamed
    {
        public DxfBaseTypeAttribute(string name)
        {
            Check.NotEmpty(name, nameof(name));
            this.Name = name;
        }

        public string Name { get; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class DxfHeaderTypeAttribute : DxfBaseTypeAttribute
    {
        public DxfHeaderTypeAttribute(string name) : base(name) { }
        
    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class DxfClassTypeAttribute : DxfBaseTypeAttribute
    {
        public DxfClassTypeAttribute(string name) : base(name) { }

    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class DxfTableTypeAttribute : DxfBaseTypeAttribute
    {
        public DxfTableTypeAttribute(string name) : base(name) { }

    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class DxfBlockTypeAttribute : DxfBaseTypeAttribute
    {
        public DxfBlockTypeAttribute(string name) : base(name) { }

    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class DxfObjectTypeAttribute : DxfBaseTypeAttribute
    {
        public DxfObjectTypeAttribute(string name) : base(name) { }

    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class DxfEntityTypeAttribute : DxfBaseTypeAttribute
    {
        public DxfEntityTypeAttribute(string name) : base(name) { }
    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class DxObjectTypeAttribute : DxfBaseTypeAttribute
    {
        public DxObjectTypeAttribute(string name) : base(name) { }

    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class DxfThumbnailTypeAttribute : DxfBaseTypeAttribute
    {
        public DxfThumbnailTypeAttribute(string name) : base(name) { }

    }

    public abstract class SectionReader : IReader
    {
        IReader currentReader;
        public SectionReader(IServiceProvider serviceProvider, ILogger<SectionReader> logger)
        {
            ServiceProvider = serviceProvider;
            Logger = logger;
        }
        public ILogger<SectionReader> Logger { get; }
        public IServiceProvider ServiceProvider { get; }
        public IDxfDocument Document { get; set; }
        protected virtual int GroupCode => EntityGroupCode.EntityType;
        protected virtual bool CanIgnore => true;
        public virtual void Read<TType>((int, string) code)
            where TType : DxfBaseTypeAttribute
        {
            if (code.Item1 == GroupCode)
            {
                currentReader = ServiceProvider.ResolveKeyed<IReader, TType>(code.Item2);

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
                    currentReader.Read<TType>(code);

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
