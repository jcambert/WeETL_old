using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeETL.Observables.PN2000
{
    public class PnCadSchema
    {
        public int Color { get; set; } // FROM 1 TO 63
        public int Prop2 { get; set; }
        public int Prop3 { get; set; }
        public int Prop4 { get; set; }
        public int Prop5 { get; set; }
        public int Prop6 { get; set; }
        public int Type { get; set; }//1=Line 2=Circle
        public int Direction { get; set; }//0=CW 1==CCW
        public double Point1X{ get; set; }
        public double  Point1Y { get; set; }
        public double Point2X { get; set; }
        public double Point2Y { get; set; }

        public double  Angle { get; set; }

        public override string ToString()
        {
            var ptx1 = Point1X.ToString("0.00000").PadLeft(14);
            var pty1 = Point1Y.ToString("0.00000").PadLeft(14);
            var ptx2 = Point2X.ToString("0.00000").PadLeft(14);
            var pty2 = Point2Y.ToString("0.00000").PadLeft(14);
            var angle = Angle.ToString("0.00000").PadLeft(14);
            return $"{Color.ToString().PadLeft(6)}{Prop2.ToString().PadLeft(6)}{Prop3.ToString().PadLeft(6)}{Prop4.ToString().PadLeft(6)}{Prop5.ToString().PadLeft(6)}{Prop6.ToString().PadLeft(6)}{Type.ToString().PadLeft(6)}{Direction.ToString().PadLeft(6)}{ptx1}{pty1}{ptx2}{pty2}{angle}";
        }
    }
}
