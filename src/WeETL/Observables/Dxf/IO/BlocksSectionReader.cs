using Microsoft.Extensions.Logging;
using System;

namespace WeETL.Observables.Dxf.IO
{
    [DxfSection(DxfObjectCode.BlocksSection)]
    public class BlocksSectionReader : SectionReader
    {
        public BlocksSectionReader(IServiceProvider serviceProvider, ILogger<BlocksSectionReader> logger) : base(serviceProvider,logger)
        {
        }
    }

    [DxfEntityType(DxfObjectCode.Block)]
    internal class BlocksSectionBlock : AbstractReader
    {
        public BlocksSectionBlock(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.EndBlock)]
    internal class BlocksSectionEndBlock : AbstractReader
    {
        public BlocksSectionEndBlock(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
