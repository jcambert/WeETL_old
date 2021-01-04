
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.Observables.Dxf.Entities;

namespace WeETL.Observables.Dxf.IO
{
    public partial class EntitySectionWriter{
        protected override void InternalWrite()
        {
            foreach (EntityObject entity in Document.Entities)
            {
                if(entity is AcadProxyEntity){
                    AcadProxyEntity e=entity as AcadProxyEntity;
                    TextWriter.Write(Utilities.AcadProxyEntityToDxfFormat(e));
                    continue;
                }
                if(entity is Arc){
                    Arc e=entity as Arc;
                    TextWriter.Write(Utilities.ArcToDxfFormat(e));
                    continue;
                }
                if(entity is Attrib){
                    Attrib e=entity as Attrib;
                    TextWriter.Write(Utilities.AttribToDxfFormat(e));
                    continue;
                }
                if(entity is AttributeDefinition){
                    AttributeDefinition e=entity as AttributeDefinition;
                    TextWriter.Write(Utilities.AttributeDefinitionToDxfFormat(e));
                    continue;
                }
                if(entity is Body){
                    Body e=entity as Body;
                    TextWriter.Write(Utilities.BodyToDxfFormat(e));
                    continue;
                }
                if(entity is Circle){
                    Circle e=entity as Circle;
                    TextWriter.Write(Utilities.CircleToDxfFormat(e));
                    continue;
                }
                if(entity is Dimension){
                    Dimension e=entity as Dimension;
                    TextWriter.Write(Utilities.DimensionToDxfFormat(e));
                    continue;
                }
                if(entity is Ellipse){
                    Ellipse e=entity as Ellipse;
                    TextWriter.Write(Utilities.EllipseToDxfFormat(e));
                    continue;
                }
                if(entity is EndSection){
                    EndSection e=entity as EndSection;
                    TextWriter.Write(Utilities.EndSectionToDxfFormat(e));
                    continue;
                }
                if(entity is Face3d){
                    Face3d e=entity as Face3d;
                    TextWriter.Write(Utilities.Face3dToDxfFormat(e));
                    continue;
                }
                if(entity is Hatch){
                    Hatch e=entity as Hatch;
                    TextWriter.Write(Utilities.HatchToDxfFormat(e));
                    continue;
                }
                if(entity is Helix){
                    Helix e=entity as Helix;
                    TextWriter.Write(Utilities.HelixToDxfFormat(e));
                    continue;
                }
                if(entity is Image){
                    Image e=entity as Image;
                    TextWriter.Write(Utilities.ImageToDxfFormat(e));
                    continue;
                }
                if(entity is Insert){
                    Insert e=entity as Insert;
                    TextWriter.Write(Utilities.InsertToDxfFormat(e));
                    continue;
                }
                if(entity is Leader){
                    Leader e=entity as Leader;
                    TextWriter.Write(Utilities.LeaderToDxfFormat(e));
                    continue;
                }
                if(entity is Light){
                    Light e=entity as Light;
                    TextWriter.Write(Utilities.LightToDxfFormat(e));
                    continue;
                }
                if(entity is Line){
                    Line e=entity as Line;
                    TextWriter.Write(Utilities.LineToDxfFormat(e));
                    continue;
                }
                if(entity is LwPolyline){
                    LwPolyline e=entity as LwPolyline;
                    TextWriter.Write(Utilities.LwPolylineToDxfFormat(e));
                    continue;
                }
                if(entity is Mesh){
                    Mesh e=entity as Mesh;
                    TextWriter.Write(Utilities.MeshToDxfFormat(e));
                    continue;
                }
                if(entity is MultiLeader){
                    MultiLeader e=entity as MultiLeader;
                    TextWriter.Write(Utilities.MultiLeaderToDxfFormat(e));
                    continue;
                }
                if(entity is MultiLeaderStyle){
                    MultiLeaderStyle e=entity as MultiLeaderStyle;
                    TextWriter.Write(Utilities.MultiLeaderStyleToDxfFormat(e));
                    continue;
                }
                if(entity is MultiLine){
                    MultiLine e=entity as MultiLine;
                    TextWriter.Write(Utilities.MultiLineToDxfFormat(e));
                    continue;
                }
                if(entity is MultiText){
                    MultiText e=entity as MultiText;
                    TextWriter.Write(Utilities.MultiTextToDxfFormat(e));
                    continue;
                }
                if(entity is Ole2Frame){
                    Ole2Frame e=entity as Ole2Frame;
                    TextWriter.Write(Utilities.Ole2FrameToDxfFormat(e));
                    continue;
                }
                if(entity is OleFrame){
                    OleFrame e=entity as OleFrame;
                    TextWriter.Write(Utilities.OleFrameToDxfFormat(e));
                    continue;
                }
                if(entity is Point){
                    Point e=entity as Point;
                    TextWriter.Write(Utilities.PointToDxfFormat(e));
                    continue;
                }
                if(entity is PolyLine){
                    PolyLine e=entity as PolyLine;
                    TextWriter.Write(Utilities.PolyLineToDxfFormat(e));
                    continue;
                }
                if(entity is Ray){
                    Ray e=entity as Ray;
                    TextWriter.Write(Utilities.RayToDxfFormat(e));
                    continue;
                }
                if(entity is Region){
                    Region e=entity as Region;
                    TextWriter.Write(Utilities.RegionToDxfFormat(e));
                    continue;
                }
                if(entity is Section){
                    Section e=entity as Section;
                    TextWriter.Write(Utilities.SectionToDxfFormat(e));
                    continue;
                }
                if(entity is Shape){
                    Shape e=entity as Shape;
                    TextWriter.Write(Utilities.ShapeToDxfFormat(e));
                    continue;
                }
                if(entity is Solid){
                    Solid e=entity as Solid;
                    TextWriter.Write(Utilities.SolidToDxfFormat(e));
                    continue;
                }
                if(entity is Solid3d){
                    Solid3d e=entity as Solid3d;
                    TextWriter.Write(Utilities.Solid3dToDxfFormat(e));
                    continue;
                }
                if(entity is Spline){
                    Spline e=entity as Spline;
                    TextWriter.Write(Utilities.SplineToDxfFormat(e));
                    continue;
                }
                if(entity is Sun){
                    Sun e=entity as Sun;
                    TextWriter.Write(Utilities.SunToDxfFormat(e));
                    continue;
                }
                if(entity is Surface){
                    Surface e=entity as Surface;
                    TextWriter.Write(Utilities.SurfaceToDxfFormat(e));
                    continue;
                }
                if(entity is Table){
                    Table e=entity as Table;
                    TextWriter.Write(Utilities.TableToDxfFormat(e));
                    continue;
                }
                if(entity is Text){
                    Text e=entity as Text;
                    TextWriter.Write(Utilities.TextToDxfFormat(e));
                    continue;
                }
                if(entity is Tolerance){
                    Tolerance e=entity as Tolerance;
                    TextWriter.Write(Utilities.ToleranceToDxfFormat(e));
                    continue;
                }
                if(entity is Trace){
                    Trace e=entity as Trace;
                    TextWriter.Write(Utilities.TraceToDxfFormat(e));
                    continue;
                }
                if(entity is Underlay){
                    Underlay e=entity as Underlay;
                    TextWriter.Write(Utilities.UnderlayToDxfFormat(e));
                    continue;
                }
                if(entity is Vertex){
                    Vertex e=entity as Vertex;
                    TextWriter.Write(Utilities.VertexToDxfFormat(e));
                    continue;
                }
                if(entity is ViewPort){
                    ViewPort e=entity as ViewPort;
                    TextWriter.Write(Utilities.ViewPortToDxfFormat(e));
                    continue;
                }
                if(entity is WipeOut){
                    WipeOut e=entity as WipeOut;
                    TextWriter.Write(Utilities.WipeOutToDxfFormat(e));
                    continue;
                }
                if(entity is XLine){
                    XLine e=entity as XLine;
                    TextWriter.Write(Utilities.XLineToDxfFormat(e));
                    continue;
                }
            }
        }
    }
}