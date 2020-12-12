using System;
using WeETL.Numerics;

namespace WeETL.Observables.Dxf.Entities
{
    public class Line : EntityObject
    {
        public Line(): this(Vector3.Zero, Vector3.Zero){}
        public Line(Vector2 start,Vector2 end): this(new Vector3(start.X, start.Y, 0.0), new Vector3(end.X, end.Y, 0.0)){}
        public Line(Vector3 start, Vector3 end) : this(start, end, 0.0) { }
        public Line(Vector3 start, Vector3 end,double thickness):base(DxfObjectCode.Line)
            => (Start, End, Thickness) = (start, end, thickness);

        public Vector3 Start{ get; set; }
        public Vector3 End { get; set; }
        public double Thickness { get; set; }
        public Vector3 Direction => End - Start;

        

        public void Reverse()
        {
            Vector3 tmp =Start;
            Start = End;
            End = tmp;
        }

        #region overrides
        public override void TransformBy(Matrix3 transformation, Vector3 translation)
        {
            Vector3 newNormal = transformation * this.Normal;
            if (Vector3.Equals(Vector3.Zero, newNormal)) newNormal = this.Normal;

            Start= transformation * Start+ translation;
            End= transformation * End + translation;
            this.Normal = newNormal;
        }
        public override object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
