using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.DependencyInjection;
using WeETL.Utilities;

namespace WeETL.Observables.Dxf.IO
{

    public interface IWriter
    {
        void Write();
        IDxfDocument Document { get; set; }
        TextWriter TextWriter { get; set; }
        string SectionName { get; set; }
    }
    public interface IWriterFactory
    {
        IWriter CreateWriterSection(string sectionName, IDxfDocument document, TextWriter textWriter);
    }
    
    public abstract class SectionWriter : IWriter
    {
        public static (int, string) START_SECTION = (0, "SECTION");
        public static (int, string) END_SECTION = (0, "ENDSEC");
        public SectionWriter(IServiceProvider serviceProvider,ILogger<SectionWriter> logger)
        {
            ServiceProvider = serviceProvider;
            Logger = logger;
        }
        public ILogger<SectionWriter> Logger { get; }
        public IServiceProvider ServiceProvider { get; }
        public IDxfDocument Document { get; set; }
        public TextWriter TextWriter { get ; set ; }
        public string SectionName { get ; set ; }
        private void WriteStartSection() => Write(START_SECTION);
        private void WriteSectionName() => Write((2, SectionName));
        private void WriteEndSection() => Write(END_SECTION);

        public void Write((int, string) code)
        {
            TextWriter.WriteLine($"{code.Item1}\n{code.Item2}");
        }
        public  void Write()
        {
            WriteStartSection();
            WriteSectionName();
            InternalWrite();
            WriteEndSection();
        }

        protected abstract void InternalWrite();
    }

    public class WriterFactory : IWriterFactory
    {
        public WriterFactory(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        public IServiceProvider ServiceProvider { get; }
   
        public IWriter CreateWriterSection(string sectionName, IDxfDocument document, TextWriter textWriter)
        {
            Check.NotNull(sectionName, nameof(sectionName));
            Check.NotNull(document, nameof(document));
            Check.NotNull(textWriter, nameof(textWriter));
            var service = ServiceProvider.ResolveKeyed<IWriter, DxfSectionAttribute>(sectionName);
            service.SectionName = sectionName;
            service.Document = document;
            service.TextWriter = textWriter;
            return service;
        }

    }

}
