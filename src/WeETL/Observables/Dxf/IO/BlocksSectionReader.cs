using Microsoft.Extensions.Logging;
using System;

namespace WeETL.Observables.Dxf.IO
{
    [DxfSection(DxfBlockCode.BlockSection)]
    public class BlocksSectionReader : SectionReader
    {
        public BlocksSectionReader(IServiceProvider serviceProvider, ILogger<BlocksSectionReader> logger) : base(serviceProvider,logger)
        {
        }
    }

    [DxfEntityType(DxfBlockCode.Block)]
    internal class BlocksSectionBlock : AbstractReader
    {
        public BlocksSectionBlock(IServiceProvider serviceProvider, ILogger<BlocksSectionBlock> logger) : base(serviceProvider,logger)
        {
        }
    }
    [DxfEntityType(DxfBlockCode.EndBlock)]
    internal class BlocksSectionEndBlock : AbstractReader
    {
        public BlocksSectionEndBlock(IServiceProvider serviceProvider, ILogger<BlocksSectionEndBlock> logger) : base(serviceProvider,logger)
        {
        }
    }
}
