using Microsoft.Extensions.Logging;
using System;

namespace WeETL.Observables.Dxf.IO
{
    [DxfEntityType(DxfObjectCode.Unknown)]
    internal partial class ObjectSectionUnknownReader: AbstractReader
    {
        public ObjectSectionUnknownReader(IServiceProvider sp, ILogger<ObjectSectionUnknownReader> logger) : base(sp,logger){}
    }
}