using Microsoft.Extensions.Logging;
using System;

namespace WeETL.Observables.Dxf.IO
{
    [DxfSection(DxfObjectCode.TablesSection)]
    public class TablesSectionReader : SectionReader
    {
        public TablesSectionReader(IServiceProvider serviceProvider,ILogger<TablesSectionReader> logger) : base(serviceProvider,logger)
        {
        }
    }

    [DxfEntityType(DxfObjectCode.AppId)]
    internal class TableSectionAppId : AbstractReader
    {
        public TableSectionAppId(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.BlockRecord)]
    internal class TableSectionBLockRecord : AbstractReader
    {
        public TableSectionBLockRecord(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.DimStyle)]
    internal class TableSectionDimStyle : AbstractReader
    {
        public TableSectionDimStyle(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Layer)]
    internal class TableSectionLayer : AbstractReader
    {
        public TableSectionLayer(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.LType)]
    internal class TableSectionLType : AbstractReader
    {
        public TableSectionLType(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Style)]
    internal class TableSectionStyle : AbstractReader
    {
        public TableSectionStyle(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Ucs)]
    internal class TableSectionUcs : AbstractReader
    {
        public TableSectionUcs(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.View)]
    internal class TableSectionView : AbstractReader
    {
        public TableSectionView(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.VPort)]
    internal class TableSectionVPort : AbstractReader
    {
        public TableSectionVPort(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
