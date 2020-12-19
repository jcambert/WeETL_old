using Microsoft.Extensions.Logging;
using System;

namespace WeETL.Observables.Dxf.IO
{

    [DxfSection(DxfObjectCode.ObjectsSection)]
    public class ObjectsSectionReader : SectionReader
    {
        public ObjectsSectionReader(IServiceProvider serviceProvider,ILogger<ObjectsSectionReader> logger) : base(serviceProvider,logger)
        {
        }
    }

}
