using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeETL.Observables.Dxf
{
    public interface IDxfDocument
    {
        IDxfHeader Header { get; }
    }
    public class DxfDocument:IDxfDocument
    {
        public DxfDocument(IDxfHeader header)
        {
            this.Header = header;
        }

        public IDxfHeader Header { get; }

        public override string ToString()
        {
            return Header.ToString();
        }
    }
}
