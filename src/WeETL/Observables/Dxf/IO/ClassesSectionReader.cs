using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeETL.Observables.Dxf.IO
{

    [DxfSection(DxfObjectCode.ClassesSection)]
    public class ClassesSectionReader : SectionReader
    {
        public ClassesSectionReader(IServiceProvider serviceProvider,ILogger<ClassesSectionReader> logger) : base(serviceProvider,logger)
        {
        }
    }

   
}
