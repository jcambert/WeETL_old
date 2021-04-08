using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.IO;

namespace WeETL.Observables.PN2000.IO
{
    public interface IPnCadWriter:IFileWriter<IPnCadDocument>
    {
 
    }
    public class PnCadWriter : FileWriter<IPnCadDocument>
    {
        protected override void InternalWrite(TextWriter writer, IPnCadDocument document, string filename)
        {
            var piece=Path.GetFileName(filename);
            writer.WriteLine(" V  3.0");
            for (int i = 0; i < 39; i++)
            {
                writer.WriteLine("");
            }
            writer.WriteLine("    .00");
            writer.WriteLine("");
            writer.WriteLine(piece ?? "");
            document.Schema.ForEach(line =>
            {
                writer.WriteLine(line.ToString());
            });
            writer.WriteLine("");
        }
    }
}
