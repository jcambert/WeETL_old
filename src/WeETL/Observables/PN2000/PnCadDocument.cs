using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.IO;

namespace WeETL.Observables.PN2000
{
    public interface IPnCadDocument : IDocument
    {
        List<PnCadSchema> Schema { get; set; }
    }
    public class PnCadDocument:IPnCadDocument
    {
        public List<PnCadSchema> Schema { get; set; } = new List<PnCadSchema>();
    }
}
