using WeETL.Observables.Dxf.Entities;
using System;
using Microsoft.Extensions.Logging;
#if DEBUG
using System.Diagnostics;
#endif

namespace WeETL.Observables.Dxf.IO
{
    [DxfSection(DxfObjectCode.EntitiesSection)]
    public class EntitySectionReader : SectionReader
    {
        public EntitySectionReader(IServiceProvider serviceProvider,ILogger<EntitySectionReader> logger) : base(serviceProvider,logger)
        {
        }
    }

    [DxfEntityType(DxfObjectCode.Face3d)]
    internal class EntitySection3DFaceReader : AbstractReader
    {
        public EntitySection3DFaceReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Solid3d)]
    internal class EntitySection3DSolidReader : AbstractReader
    {
        public EntitySection3DSolidReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.AcadProxyEntity)]
    internal class EntitySectionAcadProxyEntityReader : AbstractReader
    {
        public EntitySectionAcadProxyEntityReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Arc)]
    internal class EntitySectionArcReader : AbstractReader
    {
        public EntitySectionArcReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.AttributeDefinition)]
    internal class EntitySectionAttributeDefinitionReader : AbstractReader
    {
        public EntitySectionAttributeDefinitionReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Attribute)]
    internal class EntitySectionAttributeReader : AbstractReader
    {
        public EntitySectionAttributeReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Body)]
    internal class EntitySectionBodyReader : AbstractReader
    {
        public EntitySectionBodyReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Circle)]
    internal class EntitySectionCircleReader : AbstractReader
    {
        public EntitySectionCircleReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Dimension)]
    internal class EntitySectionDimensionReader : AbstractReader
    {
        public EntitySectionDimensionReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Ellipse)]
    internal class EntitySectionEllipseReader : AbstractReader
    {
        public EntitySectionEllipseReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Hatch)]
    internal class EntitySectionHatchReader : AbstractReader
    {
        public EntitySectionHatchReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Helix)]
    internal class EntitySectionHelixReader : AbstractReader
    {
        public EntitySectionHelixReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Image)]
    internal class EntitySectionImageReader : AbstractReader
    {
        public EntitySectionImageReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Insert)]
    internal class EntitySectionInsertReader : AbstractReader
    {
        public EntitySectionInsertReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Leader)]
    internal class EntitySectionLeaderReader : AbstractReader
    {
        public EntitySectionLeaderReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Light)]
    internal class EntitySectionLightReader : AbstractReader
    {
        public EntitySectionLightReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Line)]
    internal class EntitySectionLineReader : AbstractReader
    {
        Line line = new Line();

        public EntitySectionLineReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
#if DEBUG
            //if (Debugger.IsAttached) Debugger.Break();
#endif
            base.Read(code);
        }
        protected override DxfObject DxfObject => line;
    }
    [DxfEntityType(DxfObjectCode.LwPolyline)]
    internal class EntitySectionLWPolylineReader : AbstractReader
    {
        public EntitySectionLWPolylineReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Mesh)]
    internal class EntitySectionMeshReader : AbstractReader
    {
        public EntitySectionMeshReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.MLine)]
    internal class EntitySectionMLineReader : AbstractReader
    {
        public EntitySectionMLineReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.MLeader), DxfEntityType(DxfObjectCode.MultiLeader)]
    internal class EntitySectionMLeaderReader : AbstractReader
    {
        public EntitySectionMLeaderReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.MLeaderStyle)]
    internal class EntitySectionMLeaderStyleReader : AbstractReader
    {
        public EntitySectionMLeaderStyleReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.MText)]
    internal class EntitySectionMTextReader : AbstractReader
    {
        public EntitySectionMTextReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.OleFrame)]
    internal class EntitySectionOleFrameReader : AbstractReader
    {
        public EntitySectionOleFrameReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Ole2Frame)]
    internal class EntitySectionOle2FrameReader : AbstractReader
    {
        public EntitySectionOle2FrameReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Point)]
    internal class EntitySectionPointReader : AbstractReader
    {
        public EntitySectionPointReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Polyline)]
    internal class EntitySectionPolylineReader : AbstractReader
    {
        public EntitySectionPolylineReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Ray)]
    internal class EntitySectionRayReader : AbstractReader
    {
        public EntitySectionRayReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Region)]
    internal class EntitySectionRegionReader : AbstractReader
    {
        public EntitySectionRegionReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.BeginSection)]
    internal class EntitySectionSectionReader : AbstractReader
    {
        public EntitySectionSectionReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.EndSection)]
    internal class EntitySectionSeqEndReader : AbstractReader
    {
        public EntitySectionSeqEndReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Shape)]
    internal class EntitySectionShapeReader : AbstractReader
    {
        public EntitySectionShapeReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Solid)]
    internal class EntitySectionSolidReader : AbstractReader
    {
        public EntitySectionSolidReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Spline)]
    internal class EntitySectionSplineReader : AbstractReader
    {
        public EntitySectionSplineReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Sun)]
    internal class EntitySectionSunReader : AbstractReader
    {
        public EntitySectionSunReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Surface)]
    internal class EntitySectionSurfaceReader : AbstractReader
    {
        public EntitySectionSurfaceReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Table)]
    internal class EntitySectionTableReader : AbstractReader
    {
        public EntitySectionTableReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Text)]
    internal class EntitySectionTextReader : AbstractReader
    {
        public EntitySectionTextReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
#if DEBUG
            if (Debugger.IsAttached) Debugger.Break();
#endif
            base.Read(code);
        }
    }
    [DxfEntityType(DxfObjectCode.Tolerance)]
    internal class EntitySectoinToleranceReader : AbstractReader
    {
        public EntitySectoinToleranceReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Trace)]
    internal class EntitySectionTraceReader : AbstractReader
    {
        public EntitySectionTraceReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Underlay)]
    internal class EntitySectionUnderlayReader : AbstractReader
    {
        public EntitySectionUnderlayReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Vertex)]
    internal class EntitySectionVertexReader : AbstractReader
    {
        public EntitySectionVertexReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Viewport)]
    internal class EntitySectionViewportReader : AbstractReader
    {
        public EntitySectionViewportReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.Wipeout)]
    internal class EntitySectionWipeoutReader : AbstractReader
    {
        public EntitySectionWipeoutReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
    [DxfEntityType(DxfObjectCode.XLine)]
    internal class EntitySectionXLineReader : AbstractReader
    {
        public EntitySectionXLineReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
