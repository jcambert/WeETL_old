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

    [DxfEntityType(DxfObjectCode.AcadProxyObject)]
    internal class ObjectsSectionBlock : AbstractReader
    {
        public ObjectsSectionBlock(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.AcDbDictionary)]
    internal class ObjectsSectionAcDbDictionary : AbstractReader
    {
        public ObjectsSectionAcDbDictionary(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.AcDbPlaceholder)]
    internal class ObjectsSectionAcDbPlaceHolder : AbstractReader
    {
        public ObjectsSectionAcDbPlaceHolder(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Datatable)]
    internal class ObjectsSectionDatatable : AbstractReader
    {
        public ObjectsSectionDatatable(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Dictionary)]
    internal class ObjectsSectionDictionary : AbstractReader
    {
        public ObjectsSectionDictionary(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.DictionaryVar)]
    internal class ObjectsSectionDictionaryVar : AbstractReader
    {
        public ObjectsSectionDictionaryVar(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.DimensionAssoc)]
    internal class ObjectsSectionDimensionAssoc : AbstractReader
    {
        public ObjectsSectionDimensionAssoc(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Field)]
    internal class ObjectsSectionField : AbstractReader
    {
        public ObjectsSectionField(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.GeoData)]
    internal class ObjectsSectionGeoData : AbstractReader
    {
        public ObjectsSectionGeoData(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Group)]
    internal class ObjectsSectionGroup : AbstractReader
    {
        public ObjectsSectionGroup(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.IdBuffer)]
    internal class ObjectsSectionIdBuffer : AbstractReader
    {
        public ObjectsSectionIdBuffer(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.ImageDef)]
    internal class ObjectsSectionImageDef : AbstractReader
    {
        public ObjectsSectionImageDef(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.ImageDefReactor)]
    internal class ObjectsSectionImageDefReactor : AbstractReader
    {
        public ObjectsSectionImageDefReactor(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.LayerFilter)]
    internal class ObjectsSectionLayerFilter : AbstractReader
    {
        public ObjectsSectionLayerFilter(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.LayerIndex)]
    internal class ObjectsSectionLayerIndex : AbstractReader
    {
        public ObjectsSectionLayerIndex(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Layout)]
    internal class ObjectsSectionLayout : AbstractReader
    {
        public ObjectsSectionLayout(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.LightList)]
    internal class ObjectsSectionLightList : AbstractReader
    {
        public ObjectsSectionLightList(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Material)]
    internal class ObjectsSectionMaterial : AbstractReader
    {
        public ObjectsSectionMaterial(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.MLineStyle)]
    internal class ObjectsSectionMLineStyle : AbstractReader
    {
        public ObjectsSectionMLineStyle(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.ObjectPointer)]
    internal class ObjectsSectionObjectPointer : AbstractReader
    {
        public ObjectsSectionObjectPointer(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.PlotSettings)]
    internal class ObjectsSectionPlotSettings : AbstractReader
    {
        public ObjectsSectionPlotSettings(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.RasterVariables)]
    internal class ObjectsSectionRasterVariables : AbstractReader
    {
        public ObjectsSectionRasterVariables(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.SortEntsTable)]
    internal class ObjectsSectionSortEntsTable : AbstractReader
    {
        public ObjectsSectionSortEntsTable(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.SpacialFilter)]
    internal class ObjectsSectionSpacialFilter : AbstractReader
    {
        public ObjectsSectionSpacialFilter(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.SpacialIndex)]
    internal class ObjectsSectionSpacialIndex : AbstractReader
    {
        public ObjectsSectionSpacialIndex(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.SunStudy)]
    internal class ObjectsSectionSunStudy : AbstractReader
    {
        public ObjectsSectionSunStudy(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.TableStyle)]
    internal class ObjectsSectionTableStyle : AbstractReader
    {
        public ObjectsSectionTableStyle(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.UnderlayDefinition)]
    internal class ObjectsSectionUnderlayDefinition : AbstractReader
    {
        public ObjectsSectionUnderlayDefinition(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.VbaProject)]
    internal class ObjectsSectionVbaProject : AbstractReader
    {
        public ObjectsSectionVbaProject(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.VisualStyle)]
    internal class ObjectsSectionVisualStyle : AbstractReader
    {
        public ObjectsSectionVisualStyle(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.WipeoutVariables)]
    internal class ObjectsSectionWipeoutVariables : AbstractReader
    {
        public ObjectsSectionWipeoutVariables(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.XRecord)]
    internal class ObjectsSectionXRecord : AbstractReader
    {
        public ObjectsSectionXRecord(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
