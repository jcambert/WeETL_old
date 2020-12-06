
using System;
using WeETL.Numerics;
using System.Text;
using WeETL.Observables.Dxf.Header;
using WeETL.Observables.Dxf.Units;
namespace WeETL.Observables.Dxf
{
	public static class DxfHeaderCode{
		public const string AcadVer="$ACADVER";
		public const string HandleSeed="$HANDSEED";
		public const string Angbase="$ANGBASE";
		public const string Angdir="$ANGDIR";
		public const string AttMode="$ATTMODE";
		public const string AUnits="$AUNITS";
		public const string AUprec="$AUPREC";
		public const string CeColor="$CECOLOR";
		public const string CeLtScale="$CELTSCALE";
		public const string CeLtype="$CELTYPE";
		public const string CeLweight="$CELWEIGHT";
		public const string CLayer="$CLAYER";
		public const string CMLJust="$CMLJUST";
		public const string CMLScale="$CMLSCALE";
		public const string CMLStyle="$CMLSTYLE";
		public const string DimStyle="$DIMSTYLE";
		public const string TextSize="$TEXTSIZE";
		public const string LUnits="$LUNITS";
		public const string LUprec="$LUPREC";
		public const string DwgCodePage="$DWGCODEPAGE";
		public const string Extnames="$EXTNAMES";
		public const string InsBase="$INSBASE";
		public const string InsUnits="$INSUNITS";
		public const string LwDisplay="$LWDISPLAY";
		public const string LtScale="$LTSCALE";
		public const string MirrText="$MIRRTEXT";
		public const string PdMode="$PDMODE";
		public const string PdSize="$PDSIZE";
		public const string PLineGen="$PLINEGEN";
		public const string PsLtScale="$PSLTSCALE";
		public const string TdCreate="$TDCREATE";
		public const string TduCreate="$TDUCREATE";
		public const string TdUpdate="$TDUPDATE";
		public const string TduUpdate="$TDUUPDATE";
		public const string TdinDwg="$TDINDWG";
		public const string UcsOrg="$UCSORG";
		public const string UcsXDir="$UCSXDIR";
		public const string UcsYDir="$UCSYDIR";
	}
	public interface IDxfHeader{
		void SetValue(string key, DxfHeaderValue value);
		IDxfVersion AcadVer{get;set;}
		string HandleSeed{get;set;}
		double Angbase{get;set;}
		AngleDirection Angdir{get;set;}
		AttributeVisibility AttMode{get;set;}
		int AUnits{get;set;}
		short AUprec{get;set;}
		int CeColor{get;set;}
		double CeLtScale{get;set;}
		string CeLtype{get;set;}
		int CeLweight{get;set;}
		string CLayer{get;set;}
		int CMLJust{get;set;}
		double CMLScale{get;set;}
		string CMLStyle{get;set;}
		string DimStyle{get;set;}
		int TextSize{get;set;}
		int LUnits{get;set;}
		int LUprec{get;set;}
		string DwgCodePage{get;set;}
		int Extnames{get;set;}
		Vector3 InsBase{get;set;}
		int InsUnits{get;set;}
		int LwDisplay{get;set;}
		double LtScale{get;set;}
		int MirrText{get;set;}
		int PdMode{get;set;}
		double PdSize{get;set;}
		short PLineGen{get;set;}
		short PsLtScale{get;set;}
		DateTime TdCreate{get;set;}
		DateTime TduCreate{get;set;}
		DateTime TdUpdate{get;set;}
		DateTime TduUpdate{get;set;}
		TimeSpan TdinDwg{get;set;}
		Vector3 UcsOrg{get;set;}
		Vector3 UcsXDir{get;set;}
		Vector3 UcsYDir{get;set;}
	
	}
	public partial class DxfHeader:IDxfHeader
    {
		private void Initialize(){
			Codes[DxfHeaderCode.AcadVer]=new DxfHeaderValue(DxfHeaderCode.AcadVer,1,DxfVersion.AutoCad2018);
			Codes[DxfHeaderCode.HandleSeed]=new DxfHeaderValue(DxfHeaderCode.HandleSeed,5,"1");
			Codes[DxfHeaderCode.Angbase]=new DxfHeaderValue(DxfHeaderCode.Angbase,50,0.0);
			Codes[DxfHeaderCode.Angdir]=new DxfHeaderValue(DxfHeaderCode.Angdir,70,AngleDirection.CCW);
			Codes[DxfHeaderCode.AttMode]=new DxfHeaderValue(DxfHeaderCode.AttMode,70,AttributeVisibility.Normal);
			Codes[DxfHeaderCode.AUnits]=new DxfHeaderValue(DxfHeaderCode.AUnits,70,0);
			Codes[DxfHeaderCode.AUprec]=new DxfHeaderValue(DxfHeaderCode.AUprec,70,0);
			Codes[DxfHeaderCode.CeColor]=new DxfHeaderValue(DxfHeaderCode.CeColor,62,256);
			Codes[DxfHeaderCode.CeLtScale]=new DxfHeaderValue(DxfHeaderCode.CeLtScale,40,1.0);
			Codes[DxfHeaderCode.CeLtype]=new DxfHeaderValue(DxfHeaderCode.CeLtype,6,"ByLayer");
			Codes[DxfHeaderCode.CeLweight]=new DxfHeaderValue(DxfHeaderCode.CeLweight,370,-1);
			Codes[DxfHeaderCode.CLayer]=new DxfHeaderValue(DxfHeaderCode.CLayer,8,"DIM");
			Codes[DxfHeaderCode.CMLJust]=new DxfHeaderValue(DxfHeaderCode.CMLJust,70,0);
			Codes[DxfHeaderCode.CMLScale]=new DxfHeaderValue(DxfHeaderCode.CMLScale,40,1.0);
			Codes[DxfHeaderCode.CMLStyle]=new DxfHeaderValue(DxfHeaderCode.CMLStyle,2,"Standard");
			Codes[DxfHeaderCode.DimStyle]=new DxfHeaderValue(DxfHeaderCode.DimStyle,2,"STANDARD");
			Codes[DxfHeaderCode.TextSize]=new DxfHeaderValue(DxfHeaderCode.TextSize,40,3);
			Codes[DxfHeaderCode.LUnits]=new DxfHeaderValue(DxfHeaderCode.LUnits,70,2);
			Codes[DxfHeaderCode.LUprec]=new DxfHeaderValue(DxfHeaderCode.LUprec,70,2);
			Codes[DxfHeaderCode.DwgCodePage]=new DxfHeaderValue(DxfHeaderCode.DwgCodePage,3,"ANSI_" + Encoding.ASCII.WindowsCodePage);
			Codes[DxfHeaderCode.Extnames]=new DxfHeaderValue(DxfHeaderCode.Extnames,290,1);
			Codes[DxfHeaderCode.InsBase]=new DxfHeaderValue(DxfHeaderCode.InsBase,-1,Vector3.Zero);
			Codes[DxfHeaderCode.InsUnits]=new DxfHeaderValue(DxfHeaderCode.InsUnits,70,4);
			Codes[DxfHeaderCode.LwDisplay]=new DxfHeaderValue(DxfHeaderCode.LwDisplay,290,0);
			Codes[DxfHeaderCode.LtScale]=new DxfHeaderValue(DxfHeaderCode.LtScale,40,1.0);
			Codes[DxfHeaderCode.MirrText]=new DxfHeaderValue(DxfHeaderCode.MirrText,70,0);
			Codes[DxfHeaderCode.PdMode]=new DxfHeaderValue(DxfHeaderCode.PdMode,70,0);
			Codes[DxfHeaderCode.PdSize]=new DxfHeaderValue(DxfHeaderCode.PdSize,40,0.0);
			Codes[DxfHeaderCode.PLineGen]=new DxfHeaderValue(DxfHeaderCode.PLineGen,70,0);
			Codes[DxfHeaderCode.PsLtScale]=new DxfHeaderValue(DxfHeaderCode.PsLtScale,70,1);
			Codes[DxfHeaderCode.TdCreate]=new DxfHeaderValue(DxfHeaderCode.TdCreate,40,DateTime.Now);
			Codes[DxfHeaderCode.TduCreate]=new DxfHeaderValue(DxfHeaderCode.TduCreate,40,DateTime.UtcNow);
			Codes[DxfHeaderCode.TdUpdate]=new DxfHeaderValue(DxfHeaderCode.TdUpdate,40,DateTime.Now);
			Codes[DxfHeaderCode.TduUpdate]=new DxfHeaderValue(DxfHeaderCode.TduUpdate,40,DateTime.UtcNow);
			Codes[DxfHeaderCode.TdinDwg]=new DxfHeaderValue(DxfHeaderCode.TdinDwg,40,new TimeSpan());
			Codes[DxfHeaderCode.UcsOrg]=new DxfHeaderValue(DxfHeaderCode.UcsOrg,-1,Vector3.Zero);
			Codes[DxfHeaderCode.UcsXDir]=new DxfHeaderValue(DxfHeaderCode.UcsXDir,-1,Vector3.UnitX);
			Codes[DxfHeaderCode.UcsYDir]=new DxfHeaderValue(DxfHeaderCode.UcsYDir,-1,Vector3.UnitY);
		}

		public IDxfVersion AcadVer{
			get=>(IDxfVersion) Codes[DxfHeaderCode.AcadVer].Value;
			set{Codes[DxfHeaderCode.AcadVer].Value=value;}
		}
		public string HandleSeed{
			get=>Codes[DxfHeaderCode.HandleSeed].Value.ToString();
			set{Codes[DxfHeaderCode.HandleSeed].Value=value;}
		}
		public double Angbase{
			get=>(double) Codes[DxfHeaderCode.Angbase].Value;
			set{Codes[DxfHeaderCode.Angbase].Value=value;}
		}
		public AngleDirection Angdir{
			get=>(AngleDirection) Codes[DxfHeaderCode.Angdir].Value;
			set{Codes[DxfHeaderCode.Angdir].Value=value;}
		}
		public AttributeVisibility AttMode{
			get=>(AttributeVisibility) Codes[DxfHeaderCode.AttMode].Value;
			set{Codes[DxfHeaderCode.AttMode].Value=value;}
		}
		public int AUnits{
			get=>(int) Codes[DxfHeaderCode.AUnits].Value;
			set{Codes[DxfHeaderCode.AUnits].Value=value;}
		}
		public short AUprec{
			get=>(short) Codes[DxfHeaderCode.AUprec].Value;
			set{Codes[DxfHeaderCode.AUprec].Value=value;}
		}
		public int CeColor{
			get=>(int) Codes[DxfHeaderCode.CeColor].Value;
			set{Codes[DxfHeaderCode.CeColor].Value=value;}
		}
		public double CeLtScale{
			get=>(double) Codes[DxfHeaderCode.CeLtScale].Value;
			set{Codes[DxfHeaderCode.CeLtScale].Value=value;}
		}
		public string CeLtype{
			get=>Codes[DxfHeaderCode.CeLtype].Value.ToString();
			set{Codes[DxfHeaderCode.CeLtype].Value=value;}
		}
		public int CeLweight{
			get=>(int) Codes[DxfHeaderCode.CeLweight].Value;
			set{Codes[DxfHeaderCode.CeLweight].Value=value;}
		}
		public string CLayer{
			get=>Codes[DxfHeaderCode.CLayer].Value.ToString();
			set{Codes[DxfHeaderCode.CLayer].Value=value;}
		}
		public int CMLJust{
			get=>(int) Codes[DxfHeaderCode.CMLJust].Value;
			set{Codes[DxfHeaderCode.CMLJust].Value=value;}
		}
		public double CMLScale{
			get=>(double) Codes[DxfHeaderCode.CMLScale].Value;
			set{Codes[DxfHeaderCode.CMLScale].Value=value;}
		}
		public string CMLStyle{
			get=>Codes[DxfHeaderCode.CMLStyle].Value.ToString();
			set{Codes[DxfHeaderCode.CMLStyle].Value=value;}
		}
		public string DimStyle{
			get=>Codes[DxfHeaderCode.DimStyle].Value.ToString();
			set{Codes[DxfHeaderCode.DimStyle].Value=value;}
		}
		public int TextSize{
			get=>(int) Codes[DxfHeaderCode.TextSize].Value;
			set{Codes[DxfHeaderCode.TextSize].Value=value;}
		}
		public int LUnits{
			get=>(int) Codes[DxfHeaderCode.LUnits].Value;
			set{Codes[DxfHeaderCode.LUnits].Value=value;}
		}
		public int LUprec{
			get=>(int) Codes[DxfHeaderCode.LUprec].Value;
			set{Codes[DxfHeaderCode.LUprec].Value=value;}
		}
		public string DwgCodePage{
			get=>Codes[DxfHeaderCode.DwgCodePage].Value.ToString();
			set{Codes[DxfHeaderCode.DwgCodePage].Value=value;}
		}
		public int Extnames{
			get=>(int) Codes[DxfHeaderCode.Extnames].Value;
			set{Codes[DxfHeaderCode.Extnames].Value=value;}
		}
		public Vector3 InsBase{
			get=>(Vector3) Codes[DxfHeaderCode.InsBase].Value;
			set{Codes[DxfHeaderCode.InsBase].Value=value;}
		}
		public int InsUnits{
			get=>(int) Codes[DxfHeaderCode.InsUnits].Value;
			set{Codes[DxfHeaderCode.InsUnits].Value=value;}
		}
		public int LwDisplay{
			get=>(int) Codes[DxfHeaderCode.LwDisplay].Value;
			set{Codes[DxfHeaderCode.LwDisplay].Value=value;}
		}
		public double LtScale{
			get=>(double) Codes[DxfHeaderCode.LtScale].Value;
			set{Codes[DxfHeaderCode.LtScale].Value=value;}
		}
		public int MirrText{
			get=>(int) Codes[DxfHeaderCode.MirrText].Value;
			set{Codes[DxfHeaderCode.MirrText].Value=value;}
		}
		public int PdMode{
			get=>(int) Codes[DxfHeaderCode.PdMode].Value;
			set{Codes[DxfHeaderCode.PdMode].Value=value;}
		}
		public double PdSize{
			get=>(double) Codes[DxfHeaderCode.PdSize].Value;
			set{Codes[DxfHeaderCode.PdSize].Value=value;}
		}
		public short PLineGen{
			get=>(short) Codes[DxfHeaderCode.PLineGen].Value;
			set{Codes[DxfHeaderCode.PLineGen].Value=value;}
		}
		public short PsLtScale{
			get=>(short) Codes[DxfHeaderCode.PsLtScale].Value;
			set{Codes[DxfHeaderCode.PsLtScale].Value=value;}
		}
		public DateTime TdCreate{
			get=>(DateTime) Codes[DxfHeaderCode.TdCreate].Value;
			set{Codes[DxfHeaderCode.TdCreate].Value=value;}
		}
		public DateTime TduCreate{
			get=>(DateTime) Codes[DxfHeaderCode.TduCreate].Value;
			set{Codes[DxfHeaderCode.TduCreate].Value=value;}
		}
		public DateTime TdUpdate{
			get=>(DateTime) Codes[DxfHeaderCode.TdUpdate].Value;
			set{Codes[DxfHeaderCode.TdUpdate].Value=value;}
		}
		public DateTime TduUpdate{
			get=>(DateTime) Codes[DxfHeaderCode.TduUpdate].Value;
			set{Codes[DxfHeaderCode.TduUpdate].Value=value;}
		}
		public TimeSpan TdinDwg{
			get=>(TimeSpan) Codes[DxfHeaderCode.TdinDwg].Value;
			set{Codes[DxfHeaderCode.TdinDwg].Value=value;}
		}
		public Vector3 UcsOrg{
			get=>(Vector3) Codes[DxfHeaderCode.UcsOrg].Value;
			set{Codes[DxfHeaderCode.UcsOrg].Value=value;}
		}
		public Vector3 UcsXDir{
			get=>(Vector3) Codes[DxfHeaderCode.UcsXDir].Value;
			set{Codes[DxfHeaderCode.UcsXDir].Value=value;}
		}
		public Vector3 UcsYDir{
			get=>(Vector3) Codes[DxfHeaderCode.UcsYDir].Value;
			set{Codes[DxfHeaderCode.UcsYDir].Value=value;}
		}
	}
}


