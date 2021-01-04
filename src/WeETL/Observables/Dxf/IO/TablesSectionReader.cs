using Microsoft.Extensions.Logging;
using System;

namespace WeETL.Observables.Dxf.IO
{
    [DxfSection(DxfTableCode.TableSection)]
    public class TablesSectionReader : SectionReader
    {
        public TablesSectionReader(IServiceProvider serviceProvider,ILogger<TablesSectionReader> logger) : base(serviceProvider,logger)
        {
        }
    }

}
