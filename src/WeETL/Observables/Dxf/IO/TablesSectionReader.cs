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
/*
    [DxfEntityType(DxfObjectCode.AppId)]
    internal class TableSectionAppId : AbstractReader
    {
        public TableSectionAppId(IServiceProvider serviceProvider, ILogger<BlocksSectionReader> logger) : base(serviceProvider,logger)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.BlockRecord)]
    internal class TableSectionBLockRecord : AbstractReader
    {
        public TableSectionBLockRecord(IServiceProvider serviceProvider, ILogger<BlocksSectionReader> logger) : base(serviceProvider,logger)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.DimStyle)]
    internal class TableSectionDimStyle : AbstractReader
    {
        public TableSectionDimStyle(IServiceProvider serviceProvider, ILogger<BlocksSectionReader> logger) : base(serviceProvider,logger)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Layer)]
    internal class TableSectionLayer : AbstractReader
    {
        public TableSectionLayer(IServiceProvider serviceProvider, ILogger<BlocksSectionReader> logger) : base(serviceProvider,logger)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.LType)]
    internal class TableSectionLType : AbstractReader
    {
        public TableSectionLType(IServiceProvider serviceProvider, ILogger<BlocksSectionReader> logger) : base(serviceProvider,logger)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Style)]
    internal class TableSectionStyle : AbstractReader
    {
        public TableSectionStyle(IServiceProvider serviceProvider, ILogger<BlocksSectionReader> logger) : base(serviceProvider,logger)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Ucs)]
    internal class TableSectionUcs : AbstractReader
    {
        public TableSectionUcs(IServiceProvider serviceProvider, ILogger<BlocksSectionReader> logger) : base(serviceProvider,logger)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.View)]
    internal class TableSectionView : AbstractReader
    {
        public TableSectionView(IServiceProvider serviceProvider, ILogger<BlocksSectionReader> logger) : base(serviceProvider,logger)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.VPort)]
    internal class TableSectionVPort : AbstractReader
    {
        public TableSectionVPort(IServiceProvider serviceProvider, ILogger<BlocksSectionReader> logger) : base(serviceProvider,logger)
        {
        }
    }*/
}
