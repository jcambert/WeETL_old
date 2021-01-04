using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeETL.Observables.Dxf.Entities
{
    public partial class MultiLine
    {
        public List<MultiLineVertex> Vertices { get; } = new List<MultiLineVertex>();

        public double Elevation { get; set; }
    }
}
