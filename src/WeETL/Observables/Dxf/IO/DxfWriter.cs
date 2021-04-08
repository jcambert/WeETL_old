using System;
using System.IO;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using WeETL.IO;

namespace WeETL.Observables.Dxf.IO
{
    public interface IDxfWriter: IFileWriter<IDxfDocument>
    {
  
    }

    public class DxfWriter : FileWriter<IDxfDocument>, IDxfWriter
    {

        public IWriterFactory WriterFactory { get; }


        private DxfSection SectionsToWrite = DxfSection.All;

        public DxfWriter(IWriterFactory writerFactory)
        {
            WriterFactory = writerFactory;
        }


        protected override void InternalWrite(TextWriter writer, IDxfDocument document, string filename)
        {
           //TODO WRITE FOR SECTION, NOT ONLY ENTITIES
           
                var _writer = WriterFactory.CreateWriterSection("ENTITIES", document, writer);
                _writer.Write();

                writer.WriteEOF();

            
            
        }
    }

    public static class DxfWriterExtension
    {
        public static (int, string) START_SECTION = (0, "SECTION");
        public static (int, string) END_SECTION = (0, "ENDSEC");
        public static (int, string) END_OF_FILE = (0, "EOF");
        public static void WriteStartSection(this TextWriter tw) => tw.Writes(START_SECTION);
        public static void WriteEndSection(this TextWriter tw) => tw.Writes(END_SECTION);
        public static void WriteEOF(this TextWriter tw) => tw.Writes(END_OF_FILE);
        public static void WriteSection(this TextWriter tw, string name) => tw.Writes((2, name));
        public static void Writes(this TextWriter tw,(int,string) code)
        {
            tw.WriteLine($"{code.Item1}\n{code.Item2}");
        }
    }
}
