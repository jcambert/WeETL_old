


using System;
using WeETL.Numerics;

namespace WeETL.Observables.Dxf.Entities
{
	public abstract partial class EntityObject{
        #region properties
        public string LayerName{get;set;}
        public AciColor Color{get;set;}
        #endregion
	}
	public  partial class Face3d:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Face3d ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Solid3d:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Solid3d ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class AcadProxyEntity:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.AcadProxyEntity ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Arc:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Arc ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class AttributeDefinition:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.AttributeDefinition ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Attrib:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Attrib ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Body:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Body ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Circle:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Circle ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Dimension:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Dimension ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Ellipse:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Ellipse ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Hatch:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Hatch ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Helix:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Helix ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Image:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Image ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Insert:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Insert ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Leader:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Leader ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Light:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Light ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Line:EntityObject{
        #region properties
        public Vector3 Start{get;set;}
        public Vector3 End{get;set;}
        public double Thickness{get;set;}
        public string SubclassMarker{get;set;}
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Line ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class LwPolyline:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.LwPolyline ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Mesh:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Mesh ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class MultiLine:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.MultiLine ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class MultiLeader:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.MultiLeader ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class MultiLeaderStyle:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.MultiLeaderStyle ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class MultiText:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.MultiText ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class OleFrame:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.OleFrame ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Ole2Frame:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Ole2Frame ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Point:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Point ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class PolyLine:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.PolyLine ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Ray:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Ray ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Region:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Region ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Section:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Section ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class EndSection:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.EndSection ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Shape:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Shape ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Solid:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Solid ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Spline:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Spline ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Sun:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Sun ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Surface:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Surface ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Table:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Table ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Text:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Text ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Tolerance:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Tolerance ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Trace:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Trace ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Underlay:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Underlay ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class Vertex:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.Vertex ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class ViewPort:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.ViewPort ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class WipeOut:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.WipeOut ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
	public  partial class XLine:EntityObject{
        #region properties
        #endregion
        #region overrides
        public override string CodeName { get; }= DxfEntityCode.XLine ;
	    public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
}