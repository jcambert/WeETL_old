using Microsoft.Extensions.Logging;
using System;

namespace WeETL.Observables.Dxf.IO
{

    [DxfSection(DxfClasseCode.ClasseSection)]
    public class ClassesSectionReader : SectionReader
    {
        public ClassesSectionReader(IServiceProvider serviceProvider,ILogger<ClassesSectionReader> logger) : base(serviceProvider,logger)
        {
        }
    }

   
}
