using Microsoft.Extensions.Logging;
using System;

namespace WeETL.Observables.Dxf.IO
{

    [DxfSection(DxfThumbnailCode.ThumbnailImageSection)]
    public class ThumbnailImageSectionReader : SectionReader
    {
        public ThumbnailImageSectionReader(IServiceProvider serviceProvider,ILogger<ThumbnailImageSectionReader> logger) : base(serviceProvider,logger)
        {
        }
    }
}
