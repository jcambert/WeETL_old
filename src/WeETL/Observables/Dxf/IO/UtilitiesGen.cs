



using System;
using System.Text;
using WeETL.Observables.Dxf.Units;
using WeETL.Observables.Dxf.Entities;
using System.Globalization;
namespace WeETL.Observables.Dxf.IO
{
    internal static class Utilities
    {
     internal static bool ReadString(string value, Action<string> onset)
        {
            if (value != null)
            {
                onset?.Invoke(value);
                return true;
            }
            return false;
        }

        internal static bool ReadDouble(string value, Action<double> onset)
        {
            if (double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                onset?.Invoke(result);
                return true;
            }
            return false;
        }
        internal static bool ReadInt(string value, Action<int> onset)
        {
            if (int.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                onset?.Invoke(result);
                return true;
            }
            return false;
        }
        internal static bool ReadOnOff(string value, Action<OnOff> onset)
        {
            if (int.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                onset?.Invoke(result > 1 ? OnOff.ON : OnOff.OFF);
                return true;
            }
            return false;
        }
        internal static bool ReadShort(string value, Action<short> onset)
        {
            if (short.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                onset?.Invoke(result);
                return true;
            }
            return false;
        }
        internal static bool ReadByte(string value, Action<byte> onset)
        {
            if (byte.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                onset?.Invoke(result);
                return true;
            }
            return false;
        }
        internal static bool ReadTimeSpan(string value, Action<TimeSpan> onset)
        {
            if (double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                onset?.Invoke(TimeSpan.FromHours(result));
                return true;
            }
            return false;
        }

        internal static  bool ReadDateTime(string value, Action<DateTime> onset)
        {
            double res = 0.0;
            if (!ReadDouble(value, result => res = result))
            {
                return false;
            }
            try
            {
                onset?.Invoke(JulianToDateTime(res));
                return true;
            }
            catch
            {

                return false;
            }
        }
        static DateTime JulianToDateTime(double julianDate)
        {
            double unixTime = (julianDate - 2440587.5) * 86400;

            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTime).ToLocalTime();

            return dtDateTime;
        }
    internal static Action<EntityObject,string> EntityObjectReadHandle=(o,value)=>{
        ReadString(value, value => o.Handle = value );
    };

    internal static Action<EntityObject,string> EntityObjectReadLayerName=(o,value)=>{
        ReadString(value, value => o.LayerName = value );
    };

    internal static Action<EntityObject,string> EntityObjectReadColor=(o,value)=>{
        ReadInt(value, value => o.Color =new AciColor((byte)value) );
    };

    internal static Action<EntityObject,string> EntityObjectReadModelSpace=(o,value)=>{
        ReadInt(value, value => o.ModelSpace =(ModelSpace) value );
    };

    internal static Action<Line,string> LineReadStartX=(o,value)=>{
        var start = o.Start;
ReadDouble(value, value => start.X =value);
o.Start = start;

    };

    internal static Action<Line,string> LineReadStartY=(o,value)=>{
        var start = o.Start;
ReadDouble(value, value => start.Y =value);
o.Start = start;

    };

    internal static Action<Line,string> LineReadStartZ=(o,value)=>{
        var start = o.Start;
ReadDouble(value, value => start.Z =value);
o.Start = start;

    };

    internal static Action<Line,string> LineReadEndX=(o,value)=>{
        var end = o.End;
ReadDouble(value, value => end.X =value);
o.End = end;

    };

    internal static Action<Line,string> LineReadEndY=(o,value)=>{
        var end = o.End;
ReadDouble(value, value => end.Y =value);
o.End = end;

    };

    internal static Action<Line,string> LineReadEndZ=(o,value)=>{
        var end = o.End;
ReadDouble(value, value => end.Z =value);
o.End = end;

    };

    internal static Action<Line,string> LineReadThickness=(o,value)=>{
        ReadDouble(value, value => o.Thickness = value );
    };

    internal static Action<Line,string> LineReadSubclassMarker=(o,value)=>{
        ReadString(value, value => o.SubclassMarker = value );
    };

    internal static Action<Line,string> LineReadNormalX=(o,value)=>{
        var normal = o.Normal;
ReadDouble(value, value => normal.X =value);
o.Normal = normal;

    };

    internal static Action<Line,string> LineReadNormalY=(o,value)=>{
        var normal = o.Normal;
ReadDouble(value, value => normal.Y =value);
o.Normal = normal;

    };

    internal static Action<Line,string> LineReadNormalZ=(o,value)=>{
        var normal = o.Normal;
ReadDouble(value, value => normal.Z =value);
o.Normal = normal;

    };

    internal static Action<Circle,string> CircleReadCenterX=(o,value)=>{
        var center = o.Center;
ReadDouble(value, value => center.X =value);
o.Center = center;

    };

    internal static Action<Circle,string> CircleReadCenterY=(o,value)=>{
        var center = o.Center;
ReadDouble(value, value => center.Y =value);
o.Center = center;

    };

    internal static Action<Circle,string> CircleReadCenterZ=(o,value)=>{
        var center = o.Center;
ReadDouble(value, value => center.Z =value);
o.Center = center;

    };

    internal static Action<Circle,string> CircleReadThickness=(o,value)=>{
        ReadDouble(value, value => o.Thickness = value );
    };

    internal static Action<Circle,string> CircleReadRadius=(o,value)=>{
        ReadDouble(value, value => o.Radius = value );
    };

    internal static Action<Circle,string> CircleReadSubclassMarker=(o,value)=>{
        ReadString(value, value => o.SubclassMarker = value );
    };

    internal static Action<Circle,string> CircleReadNormalX=(o,value)=>{
        var normal = o.Normal;
ReadDouble(value, value => normal.X =value);
o.Normal = normal;

    };

    internal static Action<Circle,string> CircleReadNormalY=(o,value)=>{
        var normal = o.Normal;
ReadDouble(value, value => normal.Y =value);
o.Normal = normal;

    };

    internal static Action<Circle,string> CircleReadNormalZ=(o,value)=>{
        var normal = o.Normal;
ReadDouble(value, value => normal.Z =value);
o.Normal = normal;

    };

    internal static Action<Arc,string> ArcReadCenterX=(o,value)=>{
        var center = o.Center;
ReadDouble(value, value => center.X =value);
o.Center = center;

    };

    internal static Action<Arc,string> ArcReadCenterY=(o,value)=>{
        var center = o.Center;
ReadDouble(value, value => center.Y =value);
o.Center = center;

    };

    internal static Action<Arc,string> ArcReadCenterZ=(o,value)=>{
        var center = o.Center;
ReadDouble(value, value => center.Z =value);
o.Center = center;

    };

    internal static Action<Arc,string> ArcReadThickness=(o,value)=>{
        ReadDouble(value, value => o.Thickness = value );
    };

    internal static Action<Arc,string> ArcReadRadius=(o,value)=>{
        ReadDouble(value, value => o.Radius = value );
    };

    internal static Action<Arc,string> ArcReadStartAngle=(o,value)=>{
        ReadDouble(value, value => o.StartAngle = value );
    };

    internal static Action<Arc,string> ArcReadEndAngle=(o,value)=>{
        ReadDouble(value, value => o.EndAngle = value );
    };

    internal static Action<Arc,string> ArcReadSubclassMarker=(o,value)=>{
        ReadString(value, value => o.SubclassMarker = value );
    };

    internal static Action<Arc,string> ArcReadNormalX=(o,value)=>{
        var normal = o.Normal;
ReadDouble(value, value => normal.X =value);
o.Normal = normal;

    };

    internal static Action<Arc,string> ArcReadNormalY=(o,value)=>{
        var normal = o.Normal;
ReadDouble(value, value => normal.Y =value);
o.Normal = normal;

    };

    internal static Action<Arc,string> ArcReadNormalZ=(o,value)=>{
        var normal = o.Normal;
ReadDouble(value, value => normal.Z =value);
o.Normal = normal;

    };

    internal static Action<MultiLine,string> MultiLineReadStyleName=(o,value)=>{
        ReadString(value, value => o.StyleName = value );
    };

    internal static Action<MultiLine,string> MultiLineReadCenterX=(o,value)=>{
        var center = o.Center;
ReadDouble(value, value => center.X =value);
o.Center = center;

    };

    internal static Action<MultiLine,string> MultiLineReadCenterY=(o,value)=>{
        var center = o.Center;
ReadDouble(value, value => center.Y =value);
o.Center = center;

    };

    internal static Action<MultiLine,string> MultiLineReadCenterZ=(o,value)=>{
        var center = o.Center;
ReadDouble(value, value => center.Z =value);
o.Center = center;

    };

    internal static Action<MultiLine,string> MultiLineReadScale=(o,value)=>{
        ReadDouble(value, value => o.Scale = value );
    };

    internal static Action<MultiLine,string> MultiLineReadJustification=(o,value)=>{
        ReadInt(value, value => o.Justification =(MLineJustification) value );
    };

    internal static Action<MultiLine,string> MultiLineReadFlags=(o,value)=>{
        ReadInt(value, value => o.Flags =(MLineFlags) value );
    };

    internal static Action<MultiLine,string> MultiLineReadNumberofVertices=(o,value)=>{
        ReadShort(value, value => o.NumberofVertices =(short) value );
    };

    internal static Action<MultiLine,string> MultiLineReadNumberofStyleElements=(o,value)=>{
        ReadShort(value, value => o.NumberofStyleElements =(short) value );
    };

    internal static Action<MultiLine,string> MultiLineReadSubclassMarker=(o,value)=>{
        ReadString(value, value => o.SubclassMarker = value );
    };

    internal static Action<MultiLine,string> MultiLineReadNormalX=(o,value)=>{
        var normal = o.Normal;
ReadDouble(value, value => normal.X =value);
o.Normal = normal;

    };

    internal static Action<MultiLine,string> MultiLineReadNormalY=(o,value)=>{
        var normal = o.Normal;
ReadDouble(value, value => normal.Y =value);
o.Normal = normal;

    };

    internal static Action<MultiLine,string> MultiLineReadNormalZ=(o,value)=>{
        var normal = o.Normal;
ReadDouble(value, value => normal.Z =value);
o.Normal = normal;

    };

    internal static Action<Text,string> TextReadValue=(o,value)=>{
        ReadString(value, value => o.Value = value );
    };

    internal static Action<Text,string> TextReadStyle=(o,value)=>{
        ReadString(value, value => o.Style = value );
    };

    internal static Action<Text,string> TextReadFirstAlignmentPointX=(o,value)=>{
        var firstalignmentpoint = o.FirstAlignmentPoint;
ReadDouble(value, value => firstalignmentpoint.X =value);
o.FirstAlignmentPoint = firstalignmentpoint;

    };

    internal static Action<Text,string> TextReadSecondAlignmentPointX=(o,value)=>{
        var secondalignmentpoint = o.SecondAlignmentPoint;
ReadDouble(value, value => secondalignmentpoint.X =value);
o.SecondAlignmentPoint = secondalignmentpoint;

    };

    internal static Action<Text,string> TextReadFirstAlignmentPointY=(o,value)=>{
        var firstalignmentpoint = o.FirstAlignmentPoint;
ReadDouble(value, value => firstalignmentpoint.Y =value);
o.FirstAlignmentPoint = firstalignmentpoint;

    };

    internal static Action<Text,string> TextReadSecondAlignmentPointY=(o,value)=>{
        var secondalignmentpoint = o.SecondAlignmentPoint;
ReadDouble(value, value => secondalignmentpoint.Y =value);
o.SecondAlignmentPoint = secondalignmentpoint;

    };

    internal static Action<Text,string> TextReadFirstAlignmentPointZ=(o,value)=>{
        var firstalignmentpoint = o.FirstAlignmentPoint;
ReadDouble(value, value => firstalignmentpoint.Z =value);
o.FirstAlignmentPoint = firstalignmentpoint;

    };

    internal static Action<Text,string> TextReadSecondAlignmentPointZ=(o,value)=>{
        var secondalignmentpoint = o.SecondAlignmentPoint;
ReadDouble(value, value => secondalignmentpoint.Z =value);
o.SecondAlignmentPoint = secondalignmentpoint;

    };

    internal static Action<Text,string> TextReadThickness=(o,value)=>{
        ReadDouble(value, value => o.Thickness = value );
    };

    internal static Action<Text,string> TextReadHeight=(o,value)=>{
        ReadDouble(value, value => o.Height = value );
    };

    internal static Action<Text,string> TextReadScaleX=(o,value)=>{
        ReadDouble(value, value => o.ScaleX = value );
    };

    internal static Action<Text,string> TextReadRotation=(o,value)=>{
        ReadDouble(value, value => o.Rotation = value );
    };

    internal static Action<Text,string> TextReadOblique=(o,value)=>{
        ReadDouble(value, value => o.Oblique = value );
    };

    internal static Action<Text,string> TextReadGenerationFlag=(o,value)=>{
        ReadInt(value, value => o.GenerationFlag =(TextGenerationFlag) value );
    };

    internal static Action<Text,string> TextReadHorizontalJustification=(o,value)=>{
        ReadInt(value, value => o.HorizontalJustification =(HorizontalTextJustification) value );
    };

    internal static Action<Text,string> TextReadVerticalJustification=(o,value)=>{
        ReadInt(value, value => o.VerticalJustification =(VerticalTextJustification) value );
    };

    internal static Action<Text,string> TextReadSubclassMarker=(o,value)=>{
        ReadString(value, value => o.SubclassMarker = value );
    };

    internal static Action<Text,string> TextReadNormalX=(o,value)=>{
        var normal = o.Normal;
ReadDouble(value, value => normal.X =value);
o.Normal = normal;

    };

    internal static Action<Text,string> TextReadNormalY=(o,value)=>{
        var normal = o.Normal;
ReadDouble(value, value => normal.Y =value);
o.Normal = normal;

    };

    internal static Action<Text,string> TextReadNormalZ=(o,value)=>{
        var normal = o.Normal;
ReadDouble(value, value => normal.Z =value);
o.Normal = normal;

    };

    internal static void EntityObjectToDxfFormat(StringBuilder sb,EntityObject o)
    {
        if(o!=null && o.Handle!=null){
            sb.AppendLine("5");sb.AppendLine(o.Handle.ToString().ToString()); 
        }
        if(o!=null && o.LayerName!=null){
            sb.AppendLine("8");sb.AppendLine(o.LayerName.ToString().ToString()); 
        }
        if(o!=null && o.Color!=null){
            sb.AppendLine("62");sb.AppendLine(o.Color.ToString().ToString()); 
        }
        if(o!=null ){
            sb.AppendLine("67");sb.AppendLine(((int)o.ModelSpace).ToString().ToString()); 
        }
    }
    internal static Func<AcadProxyEntity,string> AcadProxyEntityToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("ACADPROXYENTITY");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Arc,string> ArcToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("ARC");
        EntityObjectToDxfFormat(sb, o);
        if(o!=null ){
            sb.AppendLine("10");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Center.X));
        }
        if(o!=null ){
            sb.AppendLine("20");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Center.Y));
        }
        if(o!=null ){
            sb.AppendLine("30");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Center.Z));
        }
        if(o!=null ){
            sb.AppendLine("39");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Thickness));
        }
        if(o!=null ){
            sb.AppendLine("40");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Radius));
        }
        if(o!=null ){
            sb.AppendLine("50");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.StartAngle));
        }
        if(o!=null ){
            sb.AppendLine("51");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.EndAngle));
        }
        if(o!=null && o.SubclassMarker!=null){
            sb.AppendLine("100");
            sb.AppendLine(o.SubclassMarker.ToString());
        }
        if(o!=null ){
            sb.AppendLine("210");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Normal.X));
        }
        if(o!=null ){
            sb.AppendLine("220");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Normal.Y));
        }
        if(o!=null ){
            sb.AppendLine("230");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Normal.Z));
        }
        return sb.ToString();
    };
    internal static Func<Attrib,string> AttribToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("ATTRIB");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<AttributeDefinition,string> AttributeDefinitionToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("ATTRIBUTEDEFINITION");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Body,string> BodyToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("BODY");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Circle,string> CircleToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("CIRCLE");
        EntityObjectToDxfFormat(sb, o);
        if(o!=null ){
            sb.AppendLine("10");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Center.X));
        }
        if(o!=null ){
            sb.AppendLine("20");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Center.Y));
        }
        if(o!=null ){
            sb.AppendLine("30");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Center.Z));
        }
        if(o!=null ){
            sb.AppendLine("39");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Thickness));
        }
        if(o!=null ){
            sb.AppendLine("40");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Radius));
        }
        if(o!=null && o.SubclassMarker!=null){
            sb.AppendLine("100");
            sb.AppendLine(o.SubclassMarker.ToString());
        }
        if(o!=null ){
            sb.AppendLine("210");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Normal.X));
        }
        if(o!=null ){
            sb.AppendLine("220");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Normal.Y));
        }
        if(o!=null ){
            sb.AppendLine("230");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Normal.Z));
        }
        return sb.ToString();
    };
    internal static Func<Dimension,string> DimensionToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("DIMENSION");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Ellipse,string> EllipseToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("ELLIPSE");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<EndSection,string> EndSectionToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("ENDSECTION");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Face3d,string> Face3dToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("FACE3D");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Hatch,string> HatchToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("HATCH");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Helix,string> HelixToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("HELIX");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Image,string> ImageToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("IMAGE");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Insert,string> InsertToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("INSERT");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Leader,string> LeaderToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("LEADER");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Light,string> LightToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("LIGHT");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Line,string> LineToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("LINE");
        EntityObjectToDxfFormat(sb, o);
        if(o!=null ){
            sb.AppendLine("10");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Start.X));
        }
        if(o!=null ){
            sb.AppendLine("20");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Start.Y));
        }
        if(o!=null ){
            sb.AppendLine("30");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Start.Z));
        }
        if(o!=null ){
            sb.AppendLine("11");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.End.X));
        }
        if(o!=null ){
            sb.AppendLine("21");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.End.Y));
        }
        if(o!=null ){
            sb.AppendLine("31");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.End.Z));
        }
        if(o!=null ){
            sb.AppendLine("39");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Thickness));
        }
        if(o!=null && o.SubclassMarker!=null){
            sb.AppendLine("100");
            sb.AppendLine(o.SubclassMarker.ToString());
        }
        if(o!=null ){
            sb.AppendLine("210");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Normal.X));
        }
        if(o!=null ){
            sb.AppendLine("220");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Normal.Y));
        }
        if(o!=null ){
            sb.AppendLine("230");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Normal.Z));
        }
        return sb.ToString();
    };
    internal static Func<LwPolyline,string> LwPolylineToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("LWPOLYLINE");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Mesh,string> MeshToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("MESH");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<MultiLeader,string> MultiLeaderToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("MULTILEADER");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<MultiLeaderStyle,string> MultiLeaderStyleToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("MULTILEADERSTYLE");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<MultiLine,string> MultiLineToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("MULTILINE");
        EntityObjectToDxfFormat(sb, o);
        if(o!=null && o.StyleName!=null){
            sb.AppendLine("2");
            sb.AppendLine(o.StyleName.ToString());
        }
        if(o!=null ){
            sb.AppendLine("10");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Center.X));
        }
        if(o!=null ){
            sb.AppendLine("20");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Center.Y));
        }
        if(o!=null ){
            sb.AppendLine("30");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Center.Z));
        }
        if(o!=null ){
            sb.AppendLine("40");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Scale));
        }
        if(o!=null ){
            sb.AppendLine("70");
            sb.AppendLine(((int)o.Justification).ToString());
        }
        if(o!=null ){
            sb.AppendLine("71");
            sb.AppendLine(((int)o.Flags).ToString());
        }
        if(o!=null ){
            sb.AppendLine("72");
            sb.AppendLine(o.NumberofVertices.ToString());
        }
        if(o!=null ){
            sb.AppendLine("73");
            sb.AppendLine(o.NumberofStyleElements.ToString());
        }
        if(o!=null && o.SubclassMarker!=null){
            sb.AppendLine("100");
            sb.AppendLine(o.SubclassMarker.ToString());
        }
        if(o!=null ){
            sb.AppendLine("210");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Normal.X));
        }
        if(o!=null ){
            sb.AppendLine("220");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Normal.Y));
        }
        if(o!=null ){
            sb.AppendLine("230");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Normal.Z));
        }
        return sb.ToString();
    };
    internal static Func<MultiText,string> MultiTextToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("MULTITEXT");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Ole2Frame,string> Ole2FrameToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("OLE2FRAME");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<OleFrame,string> OleFrameToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("OLEFRAME");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Point,string> PointToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("POINT");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<PolyLine,string> PolyLineToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("POLYLINE");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Ray,string> RayToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("RAY");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Region,string> RegionToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("REGION");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Section,string> SectionToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("SECTION");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Shape,string> ShapeToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("SHAPE");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Solid,string> SolidToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("SOLID");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Solid3d,string> Solid3dToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("SOLID3D");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Spline,string> SplineToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("SPLINE");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Sun,string> SunToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("SUN");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Surface,string> SurfaceToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("SURFACE");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Table,string> TableToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("TABLE");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Text,string> TextToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("TEXT");
        EntityObjectToDxfFormat(sb, o);
        if(o!=null && o.Value!=null){
            sb.AppendLine("1");
            sb.AppendLine(o.Value.ToString());
        }
        if(o!=null && o.Style!=null){
            sb.AppendLine("7");
            sb.AppendLine(o.Style.ToString());
        }
        if(o!=null ){
            sb.AppendLine("10");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.FirstAlignmentPoint.X));
        }
        if(o!=null ){
            sb.AppendLine("11");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.SecondAlignmentPoint.X));
        }
        if(o!=null ){
            sb.AppendLine("20");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.FirstAlignmentPoint.Y));
        }
        if(o!=null ){
            sb.AppendLine("21");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.SecondAlignmentPoint.Y));
        }
        if(o!=null ){
            sb.AppendLine("30");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.FirstAlignmentPoint.Z));
        }
        if(o!=null ){
            sb.AppendLine("31");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.SecondAlignmentPoint.Z));
        }
        if(o!=null ){
            sb.AppendLine("39");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Thickness));
        }
        if(o!=null ){
            sb.AppendLine("40");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Height));
        }
        if(o!=null ){
            sb.AppendLine("41");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.ScaleX));
        }
        if(o!=null ){
            sb.AppendLine("50");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Rotation));
        }
        if(o!=null ){
            sb.AppendLine("51");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Oblique));
        }
        if(o!=null ){
            sb.AppendLine("71");
            sb.AppendLine(((int)o.GenerationFlag).ToString());
        }
        if(o!=null ){
            sb.AppendLine("72");
            sb.AppendLine(((int)o.HorizontalJustification).ToString());
        }
        if(o!=null ){
            sb.AppendLine("73");
            sb.AppendLine(((int)o.VerticalJustification).ToString());
        }
        if(o!=null && o.SubclassMarker!=null){
            sb.AppendLine("100");
            sb.AppendLine(o.SubclassMarker.ToString());
        }
        if(o!=null ){
            sb.AppendLine("210");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Normal.X));
        }
        if(o!=null ){
            sb.AppendLine("220");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Normal.Y));
        }
        if(o!=null ){
            sb.AppendLine("230");
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0:0.0######}",o.Normal.Z));
        }
        return sb.ToString();
    };
    internal static Func<Tolerance,string> ToleranceToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("TOLERANCE");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Trace,string> TraceToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("TRACE");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Underlay,string> UnderlayToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("UNDERLAY");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<Vertex,string> VertexToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("VERTEX");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<ViewPort,string> ViewPortToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("VIEWPORT");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<WipeOut,string> WipeOutToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("WIPEOUT");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    internal static Func<XLine,string> XLineToDxfFormat=o=>{
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("0");sb.AppendLine("XLINE");
        EntityObjectToDxfFormat(sb, o);
        return sb.ToString();
    };
    }
}