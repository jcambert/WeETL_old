
using WeETL.Observables.Dxf.Entities;
using System;
using Microsoft.Extensions.Logging;

namespace WeETL.Observables.Dxf.IO
{
	internal partial class EntitySectionEntityObjectReader:AbstractReader{

		public EntitySectionEntityObjectReader(IServiceProvider sp,ILogger<EntitySectionEntityObjectReader> logger):base(sp,logger)
		{
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<EntityObject,string> fn = code.Item1 switch
            {
                8 => Utilities.ReadLayerName,
                62 => Utilities.ReadColor,
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as EntityObject, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Face3d),DxfBlockType(DxfEntityCode.Face3d)]
	internal partial class EntitySectionFace3dReader:EntitySectionEntityObjectReader{

		public EntitySectionFace3dReader(IServiceProvider sp,ILogger<EntitySectionFace3dReader> logger):base(sp,logger)
		{
 
			DxfObject= new Face3d() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Face3d,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Face3d, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Solid3d),DxfBlockType(DxfEntityCode.Solid3d)]
	internal partial class EntitySectionSolid3dReader:EntitySectionEntityObjectReader{

		public EntitySectionSolid3dReader(IServiceProvider sp,ILogger<EntitySectionSolid3dReader> logger):base(sp,logger)
		{
 
			DxfObject= new Solid3d() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Solid3d,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Solid3d, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.AcadProxyEntity),DxfBlockType(DxfEntityCode.AcadProxyEntity)]
	internal partial class EntitySectionAcadProxyEntityReader:EntitySectionEntityObjectReader{

		public EntitySectionAcadProxyEntityReader(IServiceProvider sp,ILogger<EntitySectionAcadProxyEntityReader> logger):base(sp,logger)
		{
 
			DxfObject= new AcadProxyEntity() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<AcadProxyEntity,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as AcadProxyEntity, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Arc),DxfBlockType(DxfEntityCode.Arc)]
	internal partial class EntitySectionArcReader:EntitySectionEntityObjectReader{

		public EntitySectionArcReader(IServiceProvider sp,ILogger<EntitySectionArcReader> logger):base(sp,logger)
		{
 
			DxfObject= new Arc() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Arc,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Arc, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.AttributeDefinition),DxfBlockType(DxfEntityCode.AttributeDefinition)]
	internal partial class EntitySectionAttributeDefinitionReader:EntitySectionEntityObjectReader{

		public EntitySectionAttributeDefinitionReader(IServiceProvider sp,ILogger<EntitySectionAttributeDefinitionReader> logger):base(sp,logger)
		{
 
			DxfObject= new AttributeDefinition() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<AttributeDefinition,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as AttributeDefinition, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Attrib),DxfBlockType(DxfEntityCode.Attrib)]
	internal partial class EntitySectionAttribReader:EntitySectionEntityObjectReader{

		public EntitySectionAttribReader(IServiceProvider sp,ILogger<EntitySectionAttribReader> logger):base(sp,logger)
		{
 
			DxfObject= new Attrib() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Attrib,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Attrib, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Body),DxfBlockType(DxfEntityCode.Body)]
	internal partial class EntitySectionBodyReader:EntitySectionEntityObjectReader{

		public EntitySectionBodyReader(IServiceProvider sp,ILogger<EntitySectionBodyReader> logger):base(sp,logger)
		{
 
			DxfObject= new Body() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Body,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Body, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Circle),DxfBlockType(DxfEntityCode.Circle)]
	internal partial class EntitySectionCircleReader:EntitySectionEntityObjectReader{

		public EntitySectionCircleReader(IServiceProvider sp,ILogger<EntitySectionCircleReader> logger):base(sp,logger)
		{
 
			DxfObject= new Circle() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Circle,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Circle, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Dimension),DxfBlockType(DxfEntityCode.Dimension)]
	internal partial class EntitySectionDimensionReader:EntitySectionEntityObjectReader{

		public EntitySectionDimensionReader(IServiceProvider sp,ILogger<EntitySectionDimensionReader> logger):base(sp,logger)
		{
 
			DxfObject= new Dimension() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Dimension,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Dimension, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Ellipse),DxfBlockType(DxfEntityCode.Ellipse)]
	internal partial class EntitySectionEllipseReader:EntitySectionEntityObjectReader{

		public EntitySectionEllipseReader(IServiceProvider sp,ILogger<EntitySectionEllipseReader> logger):base(sp,logger)
		{
 
			DxfObject= new Ellipse() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Ellipse,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Ellipse, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Hatch),DxfBlockType(DxfEntityCode.Hatch)]
	internal partial class EntitySectionHatchReader:EntitySectionEntityObjectReader{

		public EntitySectionHatchReader(IServiceProvider sp,ILogger<EntitySectionHatchReader> logger):base(sp,logger)
		{
 
			DxfObject= new Hatch() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Hatch,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Hatch, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Helix),DxfBlockType(DxfEntityCode.Helix)]
	internal partial class EntitySectionHelixReader:EntitySectionEntityObjectReader{

		public EntitySectionHelixReader(IServiceProvider sp,ILogger<EntitySectionHelixReader> logger):base(sp,logger)
		{
 
			DxfObject= new Helix() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Helix,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Helix, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Image),DxfBlockType(DxfEntityCode.Image)]
	internal partial class EntitySectionImageReader:EntitySectionEntityObjectReader{

		public EntitySectionImageReader(IServiceProvider sp,ILogger<EntitySectionImageReader> logger):base(sp,logger)
		{
 
			DxfObject= new Image() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Image,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Image, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Insert),DxfBlockType(DxfEntityCode.Insert)]
	internal partial class EntitySectionInsertReader:EntitySectionEntityObjectReader{

		public EntitySectionInsertReader(IServiceProvider sp,ILogger<EntitySectionInsertReader> logger):base(sp,logger)
		{
 
			DxfObject= new Insert() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Insert,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Insert, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Leader),DxfBlockType(DxfEntityCode.Leader)]
	internal partial class EntitySectionLeaderReader:EntitySectionEntityObjectReader{

		public EntitySectionLeaderReader(IServiceProvider sp,ILogger<EntitySectionLeaderReader> logger):base(sp,logger)
		{
 
			DxfObject= new Leader() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Leader,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Leader, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Light),DxfBlockType(DxfEntityCode.Light)]
	internal partial class EntitySectionLightReader:EntitySectionEntityObjectReader{

		public EntitySectionLightReader(IServiceProvider sp,ILogger<EntitySectionLightReader> logger):base(sp,logger)
		{
 
			DxfObject= new Light() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Light,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Light, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Line),DxfBlockType(DxfEntityCode.Line)]
	internal partial class EntitySectionLineReader:EntitySectionEntityObjectReader{

		public EntitySectionLineReader(IServiceProvider sp,ILogger<EntitySectionLineReader> logger):base(sp,logger)
		{
 
			DxfObject= new Line() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Line,string> fn = code.Item1 switch
            {
                10 => Utilities.ReadStartX,
                20 => Utilities.ReadStartY,
                30 => Utilities.ReadStartZ,
                11 => Utilities.ReadEndX,
                21 => Utilities.ReadEndY,
                31 => Utilities.ReadEndZ,
                39 => Utilities.ReadThickness,
                100 => Utilities.ReadSubclassMarker,
                210 => Utilities.ReadNormalX,
                220 => Utilities.ReadNormalY,
                230 => Utilities.ReadNormalZ,
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Line, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.LwPolyline),DxfBlockType(DxfEntityCode.LwPolyline)]
	internal partial class EntitySectionLwPolylineReader:EntitySectionEntityObjectReader{

		public EntitySectionLwPolylineReader(IServiceProvider sp,ILogger<EntitySectionLwPolylineReader> logger):base(sp,logger)
		{
 
			DxfObject= new LwPolyline() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<LwPolyline,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as LwPolyline, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Mesh),DxfBlockType(DxfEntityCode.Mesh)]
	internal partial class EntitySectionMeshReader:EntitySectionEntityObjectReader{

		public EntitySectionMeshReader(IServiceProvider sp,ILogger<EntitySectionMeshReader> logger):base(sp,logger)
		{
 
			DxfObject= new Mesh() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Mesh,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Mesh, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.MultiLine),DxfBlockType(DxfEntityCode.MultiLine)]
	internal partial class EntitySectionMultiLineReader:EntitySectionEntityObjectReader{

		public EntitySectionMultiLineReader(IServiceProvider sp,ILogger<EntitySectionMultiLineReader> logger):base(sp,logger)
		{
 
			DxfObject= new MultiLine() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<MultiLine,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as MultiLine, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.MultiLeader),DxfBlockType(DxfEntityCode.MultiLeader)]
	internal partial class EntitySectionMultiLeaderReader:EntitySectionEntityObjectReader{

		public EntitySectionMultiLeaderReader(IServiceProvider sp,ILogger<EntitySectionMultiLeaderReader> logger):base(sp,logger)
		{
 
			DxfObject= new MultiLeader() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<MultiLeader,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as MultiLeader, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.MultiLeaderStyle),DxfBlockType(DxfEntityCode.MultiLeaderStyle)]
	internal partial class EntitySectionMultiLeaderStyleReader:EntitySectionEntityObjectReader{

		public EntitySectionMultiLeaderStyleReader(IServiceProvider sp,ILogger<EntitySectionMultiLeaderStyleReader> logger):base(sp,logger)
		{
 
			DxfObject= new MultiLeaderStyle() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<MultiLeaderStyle,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as MultiLeaderStyle, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.MultiText),DxfBlockType(DxfEntityCode.MultiText)]
	internal partial class EntitySectionMultiTextReader:EntitySectionEntityObjectReader{

		public EntitySectionMultiTextReader(IServiceProvider sp,ILogger<EntitySectionMultiTextReader> logger):base(sp,logger)
		{
 
			DxfObject= new MultiText() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<MultiText,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as MultiText, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.OleFrame),DxfBlockType(DxfEntityCode.OleFrame)]
	internal partial class EntitySectionOleFrameReader:EntitySectionEntityObjectReader{

		public EntitySectionOleFrameReader(IServiceProvider sp,ILogger<EntitySectionOleFrameReader> logger):base(sp,logger)
		{
 
			DxfObject= new OleFrame() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<OleFrame,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as OleFrame, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Ole2Frame),DxfBlockType(DxfEntityCode.Ole2Frame)]
	internal partial class EntitySectionOle2FrameReader:EntitySectionEntityObjectReader{

		public EntitySectionOle2FrameReader(IServiceProvider sp,ILogger<EntitySectionOle2FrameReader> logger):base(sp,logger)
		{
 
			DxfObject= new Ole2Frame() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Ole2Frame,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Ole2Frame, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Point),DxfBlockType(DxfEntityCode.Point)]
	internal partial class EntitySectionPointReader:EntitySectionEntityObjectReader{

		public EntitySectionPointReader(IServiceProvider sp,ILogger<EntitySectionPointReader> logger):base(sp,logger)
		{
 
			DxfObject= new Point() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Point,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Point, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.PolyLine),DxfBlockType(DxfEntityCode.PolyLine)]
	internal partial class EntitySectionPolyLineReader:EntitySectionEntityObjectReader{

		public EntitySectionPolyLineReader(IServiceProvider sp,ILogger<EntitySectionPolyLineReader> logger):base(sp,logger)
		{
 
			DxfObject= new PolyLine() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<PolyLine,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as PolyLine, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Ray),DxfBlockType(DxfEntityCode.Ray)]
	internal partial class EntitySectionRayReader:EntitySectionEntityObjectReader{

		public EntitySectionRayReader(IServiceProvider sp,ILogger<EntitySectionRayReader> logger):base(sp,logger)
		{
 
			DxfObject= new Ray() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Ray,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Ray, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Region),DxfBlockType(DxfEntityCode.Region)]
	internal partial class EntitySectionRegionReader:EntitySectionEntityObjectReader{

		public EntitySectionRegionReader(IServiceProvider sp,ILogger<EntitySectionRegionReader> logger):base(sp,logger)
		{
 
			DxfObject= new Region() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Region,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Region, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Section),DxfBlockType(DxfEntityCode.Section)]
	internal partial class EntitySectionSectionReader:EntitySectionEntityObjectReader{

		public EntitySectionSectionReader(IServiceProvider sp,ILogger<EntitySectionSectionReader> logger):base(sp,logger)
		{
 
			DxfObject= new Section() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Section,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Section, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.EndSection),DxfBlockType(DxfEntityCode.EndSection)]
	internal partial class EntitySectionEndSectionReader:EntitySectionEntityObjectReader{

		public EntitySectionEndSectionReader(IServiceProvider sp,ILogger<EntitySectionEndSectionReader> logger):base(sp,logger)
		{
 
			DxfObject= new EndSection() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<EndSection,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as EndSection, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Shape),DxfBlockType(DxfEntityCode.Shape)]
	internal partial class EntitySectionShapeReader:EntitySectionEntityObjectReader{

		public EntitySectionShapeReader(IServiceProvider sp,ILogger<EntitySectionShapeReader> logger):base(sp,logger)
		{
 
			DxfObject= new Shape() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Shape,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Shape, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Solid),DxfBlockType(DxfEntityCode.Solid)]
	internal partial class EntitySectionSolidReader:EntitySectionEntityObjectReader{

		public EntitySectionSolidReader(IServiceProvider sp,ILogger<EntitySectionSolidReader> logger):base(sp,logger)
		{
 
			DxfObject= new Solid() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Solid,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Solid, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Spline),DxfBlockType(DxfEntityCode.Spline)]
	internal partial class EntitySectionSplineReader:EntitySectionEntityObjectReader{

		public EntitySectionSplineReader(IServiceProvider sp,ILogger<EntitySectionSplineReader> logger):base(sp,logger)
		{
 
			DxfObject= new Spline() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Spline,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Spline, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Sun),DxfBlockType(DxfEntityCode.Sun)]
	internal partial class EntitySectionSunReader:EntitySectionEntityObjectReader{

		public EntitySectionSunReader(IServiceProvider sp,ILogger<EntitySectionSunReader> logger):base(sp,logger)
		{
 
			DxfObject= new Sun() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Sun,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Sun, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Surface),DxfBlockType(DxfEntityCode.Surface)]
	internal partial class EntitySectionSurfaceReader:EntitySectionEntityObjectReader{

		public EntitySectionSurfaceReader(IServiceProvider sp,ILogger<EntitySectionSurfaceReader> logger):base(sp,logger)
		{
 
			DxfObject= new Surface() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Surface,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Surface, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Table),DxfBlockType(DxfEntityCode.Table)]
	internal partial class EntitySectionTableReader:EntitySectionEntityObjectReader{

		public EntitySectionTableReader(IServiceProvider sp,ILogger<EntitySectionTableReader> logger):base(sp,logger)
		{
 
			DxfObject= new Table() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Table,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Table, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Text),DxfBlockType(DxfEntityCode.Text)]
	internal partial class EntitySectionTextReader:EntitySectionEntityObjectReader{

		public EntitySectionTextReader(IServiceProvider sp,ILogger<EntitySectionTextReader> logger):base(sp,logger)
		{
 
			DxfObject= new Text() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Text,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Text, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Tolerance),DxfBlockType(DxfEntityCode.Tolerance)]
	internal partial class EntitySectionToleranceReader:EntitySectionEntityObjectReader{

		public EntitySectionToleranceReader(IServiceProvider sp,ILogger<EntitySectionToleranceReader> logger):base(sp,logger)
		{
 
			DxfObject= new Tolerance() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Tolerance,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Tolerance, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Trace),DxfBlockType(DxfEntityCode.Trace)]
	internal partial class EntitySectionTraceReader:EntitySectionEntityObjectReader{

		public EntitySectionTraceReader(IServiceProvider sp,ILogger<EntitySectionTraceReader> logger):base(sp,logger)
		{
 
			DxfObject= new Trace() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Trace,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Trace, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Underlay),DxfBlockType(DxfEntityCode.Underlay)]
	internal partial class EntitySectionUnderlayReader:EntitySectionEntityObjectReader{

		public EntitySectionUnderlayReader(IServiceProvider sp,ILogger<EntitySectionUnderlayReader> logger):base(sp,logger)
		{
 
			DxfObject= new Underlay() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Underlay,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Underlay, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.Vertex),DxfBlockType(DxfEntityCode.Vertex)]
	internal partial class EntitySectionVertexReader:EntitySectionEntityObjectReader{

		public EntitySectionVertexReader(IServiceProvider sp,ILogger<EntitySectionVertexReader> logger):base(sp,logger)
		{
 
			DxfObject= new Vertex() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<Vertex,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as Vertex, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.ViewPort),DxfBlockType(DxfEntityCode.ViewPort)]
	internal partial class EntitySectionViewPortReader:EntitySectionEntityObjectReader{

		public EntitySectionViewPortReader(IServiceProvider sp,ILogger<EntitySectionViewPortReader> logger):base(sp,logger)
		{
 
			DxfObject= new ViewPort() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<ViewPort,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as ViewPort, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.WipeOut),DxfBlockType(DxfEntityCode.WipeOut)]
	internal partial class EntitySectionWipeOutReader:EntitySectionEntityObjectReader{

		public EntitySectionWipeOutReader(IServiceProvider sp,ILogger<EntitySectionWipeOutReader> logger):base(sp,logger)
		{
 
			DxfObject= new WipeOut() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<WipeOut,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as WipeOut, code.Item2);
		}
	}
	[DxfEntityType(DxfEntityCode.XLine),DxfBlockType(DxfEntityCode.XLine)]
	internal partial class EntitySectionXLineReader:EntitySectionEntityObjectReader{

		public EntitySectionXLineReader(IServiceProvider sp,ILogger<EntitySectionXLineReader> logger):base(sp,logger)
		{
 
			DxfObject= new XLine() ;
		}

		public override void Read<TType>((int, string) code)
        {
			base.Read<TType>(code);
			Action<XLine,string> fn = code.Item1 switch
            {
                _ => null
            };
			if(fn!=null && DxfObject!=null)
				fn( DxfObject as XLine, code.Item2);
		}
	}
}