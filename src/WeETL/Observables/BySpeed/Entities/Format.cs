using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeETL.Observables.BySpeed.Entities
{
    [DebuggerDisplay("Format {Length} x {Width}")]
    public class Format
    {
        public double Length { get; set; }

        public double Width{ get; set; }
    }
}
