using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace WeETL.Observables.Dxf.IO
{
    public interface IDxfWriter
    {
        void Write(IDxfDocument document, string filename, DxfSection DxfSection = DxfSection.All);


        IObservable<IDxfDocument> OnWrited { get; }
    }

    public class DxfWriter : IDxfWriter
    {
        private ISubject<IDxfDocument> _onWrited = new Subject<IDxfDocument>();
        public IObservable<IDxfDocument> OnWrited => _onWrited.AsObservable();

        public IWriterFactory WriterFactory { get; }

        private DxfSection SectionsToWrite = DxfSection.All;

        public DxfWriter(IWriterFactory writerFactory)
        {
            WriterFactory = writerFactory;
        }
        public void Write(IDxfDocument document, string filename, DxfSection section = DxfSection.All)
        {
            
            SectionsToWrite = section;
            using (TextWriter tw = new StreamWriter(filename, false))
            {
                var writer = WriterFactory.CreateWriterSection("ENTITIES", document,tw);
                writer.Write();

               /* tw.WriteStartSection();
                tw.WriteSection("ENTITIES");
                foreach (var entity in document.Entities)
                {
                    entity.WriteTo(tw);
                }
                tw.WriteEndSection();*/
                tw.WriteEOF();

            }
            
            _onWrited.OnNext(document);
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
