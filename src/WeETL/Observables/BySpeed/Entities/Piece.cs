using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.Numerics;
using WeETL.Observables.Dxf.Entities;
using WeETL.Observables.Dxf.Units;

namespace WeETL.Observables.BySpeed.Entities
{
    public class Piece
    {

        public List<Line> Lines { get; } = new List<Line>();
        public List<Arc> Arcs { get; } = new List<Arc>();
        public Piece(double width, double height, string name)
        {
            Width = width;
            Height = height;
            Name = name;
        }

        public double Width { get; }
        public double Height { get; }
        public string Name { get; }

        public Vector2 AddLine(Vector2 start, Vector2 to)
        {
            Lines.Add(new Line(start, to));
            return to;
        }
       
        public Vector2 AddArc(string name, Vector2 start, Vector2 end, Vector2 center, AngleDirection orientation = AngleDirection.CCW)
        {
            var radius = Math.Min(Math.Abs((start - center).Modulus()), Math.Abs((end - center).Modulus()));
            
            double startAngle = orientation==AngleDirection.CCW? Vector2.Angle(start-center).ToDegree() : Vector2.Angle(end-center).ToDegree();
            double endAngle = orientation == AngleDirection.CCW ? Vector2.Angle(end-center).ToDegree() : Vector2.Angle(start-center).ToDegree();

            var arc = new Arc(center, radius, startAngle, endAngle) { Comment = name };
            Arcs.Add(arc);
            return end;
        }

       

    }
}
