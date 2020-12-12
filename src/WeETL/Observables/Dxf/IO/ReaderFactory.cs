using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.DependencyInjection;
using WeETL.Observables.Dxf.Entities;
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
