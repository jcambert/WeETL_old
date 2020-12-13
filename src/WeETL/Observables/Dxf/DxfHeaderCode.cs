
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
		public const string DwgCodePage="$DWGCODEPAGE";
		public const string TextSize="$TEXTSIZE";
		public const string LUnits="$LUNITS";
		public const string LUprec="$LUPREC";
		public const string MaintenanceVersion="$ACADMAINTVER";
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
		public const string ExtMin="$EXTMIN";
		public const string ExtMax="$EXTMAX";
		public const string LimMin="$LIMMIN";
		public const string LimMax="$LIMMAX";
		public const string OrthogonalMode="$ORTHOMODE";
		public const string RegenMode="$REGENMODE";
		public const string FillMode="$FILLMODE";
		public const string QteExtMode="$QTEXTMODE";
		public const string TraceWidth="$TRACEWID";
		public const string TextStyle="$TEXTSTYLE";
		public const string DisplaySilhouette="$DISPSILH";
		public const string DimensioningScaleFactor="$DIMSCALE";
		public const string DimensioningArrowSize="$DIMASZ";
		public const string DimensionExtensionLineOffset="$DIMEXO";
		public const string DimensionLineIncrement="$DIMDLI";
		public const string DimensionRoudingValue="$DIMRND";
		public const string DimensionLineExtensions="$DIMDLE";
		public const string DimensionExtensionLineExtension="$DIMEXE";
		public const string TolerancePlus="$DIMTP";
		public const string ToleranceMinus="$DIMTM";
		public const string DimensionTextHeight="$DIMTXT";
		public const string SizeOfCenterMark="$DIMCEN";
		public const string DimensioningTickSize="$DIMTSZ";
		public const string DimensionTolerance="$DIMTOL";
		public const string DimensionLimit="$DIMLIM";
		public const string TextInsideHorizontal="$DIMTIH";
		public const string TextOutsideHorizontal="$DIMTOH";
		public const string DimensionFirstExtensionLineSuppressed="$DIMSE1";
		public const string DimensionSecondExtensionLineSuppressed="$DIMSE2";
		public const string TextAboveDimension="$DIMTAD";
		public const string ControlsSuppressionOfZerosForPrimaryUnit="$DIMZIN";
		public const string ArrowBlockName="$DIMBLK";
		public const string FirstArrowBlockName="$DIMBLK1";
		public const string SecondArrowBlockName="$DIMBLK2";
		public const string DimensioningAssociative="$DIMASO";
		public const string DimensioningRecomputeWhileDragging="$DIMSHO";
		public const string DimensioningGeneralSuffix="$DIMPOST";
		public const string DimensioningAlternateSuffix="$DIMAPOST";
		public const string DimensioningAlternateUnit="$DIMALT";
		public const string DimensioningAlternateUnitDecimalPlace="$DIMALTD";
		public const string DimensioningAlternateUnitScaleFactor="$DIMALTF";
		public const string DimensioningLinearScaleFactor="$DIMLFAC";
		public const string DimensioningForceLineExtensions="$DIMTOFL";
		public const string DimensionTextVerticalPosition="$DIMTVP";
		public const string DimensionForceTextInside="$DIMTIX";
		public const string DimensionSuppressOutsideExtension="$DIMSOXD";
		public const string DimensionUseSeparateArrow="$DIMSAH";
		public const string DimensionLineColor="$DIMCLRD";
		public const string DimensionExtensionLineColor="$DIMCLRE";
		public const string DimensionTextColor="$DIMCLRT";
		public const string DimensionTextScalFactor="$DIMTFAC";
		public const string DimensionLineGap="$DIMGAP";
		public const string DimensionHorizontalTextPosition="$DIMJUST";
		public const string DimensionSuppressFirstExtensionLine="$DIMSD1";
		public const string DimensionSuppressSecondExtensionLine="$DIMSD2";
		public const string DimensionVerticalTextPosition="$DIMTOLJ";
		public const string ControlsSuppressionOfZerosForToleranceValue="$DIMTZIN";
		public const string ControlsSuppressionOfZerosForAlternateUnit="$DIMALTZ";
		public const string ControlsSuppressionOfZerosForAlternateToleranceValue="$DIMALTTZ";
		public const string DimensionCursor="$DIMUPT";
		public const string DimensionNumberOfDecimalPlace="$DIMDEC";
		public const string DimensionNumberOfDecimalPlaceToDisplayToleranceValue="$DIMTDEC";
		public const string DimensionUnitFormatForAlternateUnit="$DIMALTU";
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
		string DwgCodePage{get;set;}
		int TextSize{get;set;}
		int LUnits{get;set;}
		int LUprec{get;set;}
		int MaintenanceVersion{get;set;}
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
		Vector3 ExtMin{get;set;}
		Vector3 ExtMax{get;set;}
		Vector2 LimMin{get;set;}
		Vector2 LimMax{get;set;}
		OnOff OrthogonalMode{get;set;}
		OnOff RegenMode{get;set;}
		OnOff FillMode{get;set;}
		OnOff QteExtMode{get;set;}
		double TraceWidth{get;set;}
		string TextStyle{get;set;}
		OnOff DisplaySilhouette{get;set;}
		double DimensioningScaleFactor{get;set;}
		double DimensioningArrowSize{get;set;}
		double DimensionExtensionLineOffset{get;set;}
		double DimensionLineIncrement{get;set;}
		double DimensionRoudingValue{get;set;}
		double DimensionLineExtensions{get;set;}
		double DimensionExtensionLineExtension{get;set;}
		double TolerancePlus{get;set;}
		double ToleranceMinus{get;set;}
		double DimensionTextHeight{get;set;}
		double SizeOfCenterMark{get;set;}
		double DimensioningTickSize{get;set;}
		int DimensionTolerance{get;set;}
		int DimensionLimit{get;set;}
		int TextInsideHorizontal{get;set;}
		int TextOutsideHorizontal{get;set;}
		OnOff DimensionFirstExtensionLineSuppressed{get;set;}
		OnOff DimensionSecondExtensionLineSuppressed{get;set;}
		int TextAboveDimension{get;set;}
		ControlZeroSuppression ControlsSuppressionOfZerosForPrimaryUnit{get;set;}
		string ArrowBlockName{get;set;}
		string FirstArrowBlockName{get;set;}
		string SecondArrowBlockName{get;set;}
		OnOff DimensioningAssociative{get;set;}
		OnOff DimensioningRecomputeWhileDragging{get;set;}
		string DimensioningGeneralSuffix{get;set;}
		string DimensioningAlternateSuffix{get;set;}
		int DimensioningAlternateUnit{get;set;}
		int DimensioningAlternateUnitDecimalPlace{get;set;}
		double DimensioningAlternateUnitScaleFactor{get;set;}
		double DimensioningLinearScaleFactor{get;set;}
		OnOff DimensioningForceLineExtensions{get;set;}
		double DimensionTextVerticalPosition{get;set;}
		OnOff DimensionForceTextInside{get;set;}
		OnOff DimensionSuppressOutsideExtension{get;set;}
		OnOff DimensionUseSeparateArrow{get;set;}
		AciColor DimensionLineColor{get;set;}
		AciColor DimensionExtensionLineColor{get;set;}
		AciColor DimensionTextColor{get;set;}
		double DimensionTextScalFactor{get;set;}
		double DimensionLineGap{get;set;}
		DimensionHorizontalTextPosition DimensionHorizontalTextPosition{get;set;}
		OnOff DimensionSuppressFirstExtensionLine{get;set;}
		OnOff DimensionSuppressSecondExtensionLine{get;set;}
		DimensionVerticalTextPosition DimensionVerticalTextPosition{get;set;}
		ControlZeroSuppression ControlsSuppressionOfZerosForToleranceValue{get;set;}
		ControlZeroSuppression ControlsSuppressionOfZerosForAlternateUnit{get;set;}
		ControlZeroSuppression ControlsSuppressionOfZerosForAlternateToleranceValue{get;set;}
		int DimensionCursor{get;set;}
		int DimensionNumberOfDecimalPlace{get;set;}
		int DimensionNumberOfDecimalPlaceToDisplayToleranceValue{get;set;}
		DimensionUnitFormatForAlternateUnit DimensionUnitFormatForAlternateUnit{get;set;}
	
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
			Codes[DxfHeaderCode.DwgCodePage]=new DxfHeaderValue(DxfHeaderCode.DwgCodePage,3,"ANSI_" + Encoding.ASCII.WindowsCodePage);
			Codes[DxfHeaderCode.TextSize]=new DxfHeaderValue(DxfHeaderCode.TextSize,40,3);
			Codes[DxfHeaderCode.LUnits]=new DxfHeaderValue(DxfHeaderCode.LUnits,70,2);
			Codes[DxfHeaderCode.LUprec]=new DxfHeaderValue(DxfHeaderCode.LUprec,70,2);
			Codes[DxfHeaderCode.MaintenanceVersion]=new DxfHeaderValue(DxfHeaderCode.MaintenanceVersion,70,20);
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
			Codes[DxfHeaderCode.ExtMin]=new DxfHeaderValue(DxfHeaderCode.ExtMin,-1,Vector3.Zero);
			Codes[DxfHeaderCode.ExtMax]=new DxfHeaderValue(DxfHeaderCode.ExtMax,-1,Vector3.Zero);
			Codes[DxfHeaderCode.LimMin]=new DxfHeaderValue(DxfHeaderCode.LimMin,-1,Vector2.Zero);
			Codes[DxfHeaderCode.LimMax]=new DxfHeaderValue(DxfHeaderCode.LimMax,-1,Vector2.Zero);
			Codes[DxfHeaderCode.OrthogonalMode]=new DxfHeaderValue(DxfHeaderCode.OrthogonalMode,70,OnOff.ON);
			Codes[DxfHeaderCode.RegenMode]=new DxfHeaderValue(DxfHeaderCode.RegenMode,70,OnOff.ON);
			Codes[DxfHeaderCode.FillMode]=new DxfHeaderValue(DxfHeaderCode.FillMode,70,OnOff.ON);
			Codes[DxfHeaderCode.QteExtMode]=new DxfHeaderValue(DxfHeaderCode.QteExtMode,70,OnOff.ON);
			Codes[DxfHeaderCode.TraceWidth]=new DxfHeaderValue(DxfHeaderCode.TraceWidth,40,1.0);
			Codes[DxfHeaderCode.TextStyle]=new DxfHeaderValue(DxfHeaderCode.TextStyle,7,"STANDARD");
			Codes[DxfHeaderCode.DisplaySilhouette]=new DxfHeaderValue(DxfHeaderCode.DisplaySilhouette,70,OnOff.OFF);
			Codes[DxfHeaderCode.DimensioningScaleFactor]=new DxfHeaderValue(DxfHeaderCode.DimensioningScaleFactor,40,1.0);
			Codes[DxfHeaderCode.DimensioningArrowSize]=new DxfHeaderValue(DxfHeaderCode.DimensioningArrowSize,40,1.0);
			Codes[DxfHeaderCode.DimensionExtensionLineOffset]=new DxfHeaderValue(DxfHeaderCode.DimensionExtensionLineOffset,40,1.0);
			Codes[DxfHeaderCode.DimensionLineIncrement]=new DxfHeaderValue(DxfHeaderCode.DimensionLineIncrement,40,1.0);
			Codes[DxfHeaderCode.DimensionRoudingValue]=new DxfHeaderValue(DxfHeaderCode.DimensionRoudingValue,40,1.0);
			Codes[DxfHeaderCode.DimensionLineExtensions]=new DxfHeaderValue(DxfHeaderCode.DimensionLineExtensions,40,1.0);
			Codes[DxfHeaderCode.DimensionExtensionLineExtension]=new DxfHeaderValue(DxfHeaderCode.DimensionExtensionLineExtension,40,1.0);
			Codes[DxfHeaderCode.TolerancePlus]=new DxfHeaderValue(DxfHeaderCode.TolerancePlus,40,0.0);
			Codes[DxfHeaderCode.ToleranceMinus]=new DxfHeaderValue(DxfHeaderCode.ToleranceMinus,40,0.0);
			Codes[DxfHeaderCode.DimensionTextHeight]=new DxfHeaderValue(DxfHeaderCode.DimensionTextHeight,40,3.0);
			Codes[DxfHeaderCode.SizeOfCenterMark]=new DxfHeaderValue(DxfHeaderCode.SizeOfCenterMark,40,-0.05);
			Codes[DxfHeaderCode.DimensioningTickSize]=new DxfHeaderValue(DxfHeaderCode.DimensioningTickSize,40,0.0);
			Codes[DxfHeaderCode.DimensionTolerance]=new DxfHeaderValue(DxfHeaderCode.DimensionTolerance,70,0);
			Codes[DxfHeaderCode.DimensionLimit]=new DxfHeaderValue(DxfHeaderCode.DimensionLimit,70,0);
			Codes[DxfHeaderCode.TextInsideHorizontal]=new DxfHeaderValue(DxfHeaderCode.TextInsideHorizontal,70,0);
			Codes[DxfHeaderCode.TextOutsideHorizontal]=new DxfHeaderValue(DxfHeaderCode.TextOutsideHorizontal,70,0);
			Codes[DxfHeaderCode.DimensionFirstExtensionLineSuppressed]=new DxfHeaderValue(DxfHeaderCode.DimensionFirstExtensionLineSuppressed,70,OnOff.OFF);
			Codes[DxfHeaderCode.DimensionSecondExtensionLineSuppressed]=new DxfHeaderValue(DxfHeaderCode.DimensionSecondExtensionLineSuppressed,70,OnOff.OFF);
			Codes[DxfHeaderCode.TextAboveDimension]=new DxfHeaderValue(DxfHeaderCode.TextAboveDimension,70,1);
			Codes[DxfHeaderCode.ControlsSuppressionOfZerosForPrimaryUnit]=new DxfHeaderValue(DxfHeaderCode.ControlsSuppressionOfZerosForPrimaryUnit,70,ControlZeroSuppression.RemoveZeroFeetRemoveInch);
			Codes[DxfHeaderCode.ArrowBlockName]=new DxfHeaderValue(DxfHeaderCode.ArrowBlockName,1,"");
			Codes[DxfHeaderCode.FirstArrowBlockName]=new DxfHeaderValue(DxfHeaderCode.FirstArrowBlockName,1,"");
			Codes[DxfHeaderCode.SecondArrowBlockName]=new DxfHeaderValue(DxfHeaderCode.SecondArrowBlockName,1,"");
			Codes[DxfHeaderCode.DimensioningAssociative]=new DxfHeaderValue(DxfHeaderCode.DimensioningAssociative,70,OnOff.OFF);
			Codes[DxfHeaderCode.DimensioningRecomputeWhileDragging]=new DxfHeaderValue(DxfHeaderCode.DimensioningRecomputeWhileDragging,70,OnOff.OFF);
			Codes[DxfHeaderCode.DimensioningGeneralSuffix]=new DxfHeaderValue(DxfHeaderCode.DimensioningGeneralSuffix,1,"");
			Codes[DxfHeaderCode.DimensioningAlternateSuffix]=new DxfHeaderValue(DxfHeaderCode.DimensioningAlternateSuffix,1,"");
			Codes[DxfHeaderCode.DimensioningAlternateUnit]=new DxfHeaderValue(DxfHeaderCode.DimensioningAlternateUnit,70,0);
			Codes[DxfHeaderCode.DimensioningAlternateUnitDecimalPlace]=new DxfHeaderValue(DxfHeaderCode.DimensioningAlternateUnitDecimalPlace,70,0);
			Codes[DxfHeaderCode.DimensioningAlternateUnitScaleFactor]=new DxfHeaderValue(DxfHeaderCode.DimensioningAlternateUnitScaleFactor,40,1.0);
			Codes[DxfHeaderCode.DimensioningLinearScaleFactor]=new DxfHeaderValue(DxfHeaderCode.DimensioningLinearScaleFactor,40,1.0);
			Codes[DxfHeaderCode.DimensioningForceLineExtensions]=new DxfHeaderValue(DxfHeaderCode.DimensioningForceLineExtensions,70,OnOff.OFF);
			Codes[DxfHeaderCode.DimensionTextVerticalPosition]=new DxfHeaderValue(DxfHeaderCode.DimensionTextVerticalPosition,40,0.0);
			Codes[DxfHeaderCode.DimensionForceTextInside]=new DxfHeaderValue(DxfHeaderCode.DimensionForceTextInside,70,OnOff.OFF);
			Codes[DxfHeaderCode.DimensionSuppressOutsideExtension]=new DxfHeaderValue(DxfHeaderCode.DimensionSuppressOutsideExtension,70,OnOff.OFF);
			Codes[DxfHeaderCode.DimensionUseSeparateArrow]=new DxfHeaderValue(DxfHeaderCode.DimensionUseSeparateArrow,70,OnOff.OFF);
			Codes[DxfHeaderCode.DimensionLineColor]=new DxfHeaderValue(DxfHeaderCode.DimensionLineColor,70,AciColor.FromIndex(1));
			Codes[DxfHeaderCode.DimensionExtensionLineColor]=new DxfHeaderValue(DxfHeaderCode.DimensionExtensionLineColor,70,AciColor.FromIndex(1));
			Codes[DxfHeaderCode.DimensionTextColor]=new DxfHeaderValue(DxfHeaderCode.DimensionTextColor,70,AciColor.FromIndex(1));
			Codes[DxfHeaderCode.DimensionTextScalFactor]=new DxfHeaderValue(DxfHeaderCode.DimensionTextScalFactor,40,1.0);
			Codes[DxfHeaderCode.DimensionLineGap]=new DxfHeaderValue(DxfHeaderCode.DimensionLineGap,40,1.0);
			Codes[DxfHeaderCode.DimensionHorizontalTextPosition]=new DxfHeaderValue(DxfHeaderCode.DimensionHorizontalTextPosition,70,DimensionHorizontalTextPosition.AboveCenterJustifiedBetween);
			Codes[DxfHeaderCode.DimensionSuppressFirstExtensionLine]=new DxfHeaderValue(DxfHeaderCode.DimensionSuppressFirstExtensionLine,70,OnOff.OFF);
			Codes[DxfHeaderCode.DimensionSuppressSecondExtensionLine]=new DxfHeaderValue(DxfHeaderCode.DimensionSuppressSecondExtensionLine,70,OnOff.OFF);
			Codes[DxfHeaderCode.DimensionVerticalTextPosition]=new DxfHeaderValue(DxfHeaderCode.DimensionVerticalTextPosition,70,DimensionVerticalTextPosition.Top);
			Codes[DxfHeaderCode.ControlsSuppressionOfZerosForToleranceValue]=new DxfHeaderValue(DxfHeaderCode.ControlsSuppressionOfZerosForToleranceValue,70,ControlZeroSuppression.RemoveZeroFeetRemoveInch);
			Codes[DxfHeaderCode.ControlsSuppressionOfZerosForAlternateUnit]=new DxfHeaderValue(DxfHeaderCode.ControlsSuppressionOfZerosForAlternateUnit,70,ControlZeroSuppression.RemoveZeroFeetRemoveInch);
			Codes[DxfHeaderCode.ControlsSuppressionOfZerosForAlternateToleranceValue]=new DxfHeaderValue(DxfHeaderCode.ControlsSuppressionOfZerosForAlternateToleranceValue,70,ControlZeroSuppression.RemoveZeroFeetRemoveInch);
			Codes[DxfHeaderCode.DimensionCursor]=new DxfHeaderValue(DxfHeaderCode.DimensionCursor,70,0);
			Codes[DxfHeaderCode.DimensionNumberOfDecimalPlace]=new DxfHeaderValue(DxfHeaderCode.DimensionNumberOfDecimalPlace,70,1);
			Codes[DxfHeaderCode.DimensionNumberOfDecimalPlaceToDisplayToleranceValue]=new DxfHeaderValue(DxfHeaderCode.DimensionNumberOfDecimalPlaceToDisplayToleranceValue,70,1);
			Codes[DxfHeaderCode.DimensionUnitFormatForAlternateUnit]=new DxfHeaderValue(DxfHeaderCode.DimensionUnitFormatForAlternateUnit,70,DimensionUnitFormatForAlternateUnit.Decimal);
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
		public string DwgCodePage{
			get=>Codes[DxfHeaderCode.DwgCodePage].Value.ToString();
			set{Codes[DxfHeaderCode.DwgCodePage].Value=value;}
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
		public int MaintenanceVersion{
			get=>(int) Codes[DxfHeaderCode.MaintenanceVersion].Value;
			set{Codes[DxfHeaderCode.MaintenanceVersion].Value=value;}
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
		public Vector3 ExtMin{
			get=>(Vector3) Codes[DxfHeaderCode.ExtMin].Value;
			set{Codes[DxfHeaderCode.ExtMin].Value=value;}
		}
		public Vector3 ExtMax{
			get=>(Vector3) Codes[DxfHeaderCode.ExtMax].Value;
			set{Codes[DxfHeaderCode.ExtMax].Value=value;}
		}
		public Vector2 LimMin{
			get=>(Vector2) Codes[DxfHeaderCode.LimMin].Value;
			set{Codes[DxfHeaderCode.LimMin].Value=value;}
		}
		public Vector2 LimMax{
			get=>(Vector2) Codes[DxfHeaderCode.LimMax].Value;
			set{Codes[DxfHeaderCode.LimMax].Value=value;}
		}
		public OnOff OrthogonalMode{
			get=>(OnOff) Codes[DxfHeaderCode.OrthogonalMode].Value;
			set{Codes[DxfHeaderCode.OrthogonalMode].Value=value;}
		}
		public OnOff RegenMode{
			get=>(OnOff) Codes[DxfHeaderCode.RegenMode].Value;
			set{Codes[DxfHeaderCode.RegenMode].Value=value;}
		}
		public OnOff FillMode{
			get=>(OnOff) Codes[DxfHeaderCode.FillMode].Value;
			set{Codes[DxfHeaderCode.FillMode].Value=value;}
		}
		public OnOff QteExtMode{
			get=>(OnOff) Codes[DxfHeaderCode.QteExtMode].Value;
			set{Codes[DxfHeaderCode.QteExtMode].Value=value;}
		}
		public double TraceWidth{
			get=>(double) Codes[DxfHeaderCode.TraceWidth].Value;
			set{Codes[DxfHeaderCode.TraceWidth].Value=value;}
		}
		public string TextStyle{
			get=>Codes[DxfHeaderCode.TextStyle].Value.ToString();
			set{Codes[DxfHeaderCode.TextStyle].Value=value;}
		}
		public OnOff DisplaySilhouette{
			get=>(OnOff) Codes[DxfHeaderCode.DisplaySilhouette].Value;
			set{Codes[DxfHeaderCode.DisplaySilhouette].Value=value;}
		}
		public double DimensioningScaleFactor{
			get=>(double) Codes[DxfHeaderCode.DimensioningScaleFactor].Value;
			set{Codes[DxfHeaderCode.DimensioningScaleFactor].Value=value;}
		}
		public double DimensioningArrowSize{
			get=>(double) Codes[DxfHeaderCode.DimensioningArrowSize].Value;
			set{Codes[DxfHeaderCode.DimensioningArrowSize].Value=value;}
		}
		public double DimensionExtensionLineOffset{
			get=>(double) Codes[DxfHeaderCode.DimensionExtensionLineOffset].Value;
			set{Codes[DxfHeaderCode.DimensionExtensionLineOffset].Value=value;}
		}
		public double DimensionLineIncrement{
			get=>(double) Codes[DxfHeaderCode.DimensionLineIncrement].Value;
			set{Codes[DxfHeaderCode.DimensionLineIncrement].Value=value;}
		}
		public double DimensionRoudingValue{
			get=>(double) Codes[DxfHeaderCode.DimensionRoudingValue].Value;
			set{Codes[DxfHeaderCode.DimensionRoudingValue].Value=value;}
		}
		public double DimensionLineExtensions{
			get=>(double) Codes[DxfHeaderCode.DimensionLineExtensions].Value;
			set{Codes[DxfHeaderCode.DimensionLineExtensions].Value=value;}
		}
		public double DimensionExtensionLineExtension{
			get=>(double) Codes[DxfHeaderCode.DimensionExtensionLineExtension].Value;
			set{Codes[DxfHeaderCode.DimensionExtensionLineExtension].Value=value;}
		}
		public double TolerancePlus{
			get=>(double) Codes[DxfHeaderCode.TolerancePlus].Value;
			set{Codes[DxfHeaderCode.TolerancePlus].Value=value;}
		}
		public double ToleranceMinus{
			get=>(double) Codes[DxfHeaderCode.ToleranceMinus].Value;
			set{Codes[DxfHeaderCode.ToleranceMinus].Value=value;}
		}
		public double DimensionTextHeight{
			get=>(double) Codes[DxfHeaderCode.DimensionTextHeight].Value;
			set{Codes[DxfHeaderCode.DimensionTextHeight].Value=value;}
		}
		public double SizeOfCenterMark{
			get=>(double) Codes[DxfHeaderCode.SizeOfCenterMark].Value;
			set{Codes[DxfHeaderCode.SizeOfCenterMark].Value=value;}
		}
		public double DimensioningTickSize{
			get=>(double) Codes[DxfHeaderCode.DimensioningTickSize].Value;
			set{Codes[DxfHeaderCode.DimensioningTickSize].Value=value;}
		}
		public int DimensionTolerance{
			get=>(int) Codes[DxfHeaderCode.DimensionTolerance].Value;
			set{Codes[DxfHeaderCode.DimensionTolerance].Value=value;}
		}
		public int DimensionLimit{
			get=>(int) Codes[DxfHeaderCode.DimensionLimit].Value;
			set{Codes[DxfHeaderCode.DimensionLimit].Value=value;}
		}
		public int TextInsideHorizontal{
			get=>(int) Codes[DxfHeaderCode.TextInsideHorizontal].Value;
			set{Codes[DxfHeaderCode.TextInsideHorizontal].Value=value;}
		}
		public int TextOutsideHorizontal{
			get=>(int) Codes[DxfHeaderCode.TextOutsideHorizontal].Value;
			set{Codes[DxfHeaderCode.TextOutsideHorizontal].Value=value;}
		}
		public OnOff DimensionFirstExtensionLineSuppressed{
			get=>(OnOff) Codes[DxfHeaderCode.DimensionFirstExtensionLineSuppressed].Value;
			set{Codes[DxfHeaderCode.DimensionFirstExtensionLineSuppressed].Value=value;}
		}
		public OnOff DimensionSecondExtensionLineSuppressed{
			get=>(OnOff) Codes[DxfHeaderCode.DimensionSecondExtensionLineSuppressed].Value;
			set{Codes[DxfHeaderCode.DimensionSecondExtensionLineSuppressed].Value=value;}
		}
		public int TextAboveDimension{
			get=>(int) Codes[DxfHeaderCode.TextAboveDimension].Value;
			set{Codes[DxfHeaderCode.TextAboveDimension].Value=value;}
		}
		public ControlZeroSuppression ControlsSuppressionOfZerosForPrimaryUnit{
			get=>(ControlZeroSuppression) Codes[DxfHeaderCode.ControlsSuppressionOfZerosForPrimaryUnit].Value;
			set{Codes[DxfHeaderCode.ControlsSuppressionOfZerosForPrimaryUnit].Value=value;}
		}
		public string ArrowBlockName{
			get=>Codes[DxfHeaderCode.ArrowBlockName].Value.ToString();
			set{Codes[DxfHeaderCode.ArrowBlockName].Value=value;}
		}
		public string FirstArrowBlockName{
			get=>Codes[DxfHeaderCode.FirstArrowBlockName].Value.ToString();
			set{Codes[DxfHeaderCode.FirstArrowBlockName].Value=value;}
		}
		public string SecondArrowBlockName{
			get=>Codes[DxfHeaderCode.SecondArrowBlockName].Value.ToString();
			set{Codes[DxfHeaderCode.SecondArrowBlockName].Value=value;}
		}
		public OnOff DimensioningAssociative{
			get=>(OnOff) Codes[DxfHeaderCode.DimensioningAssociative].Value;
			set{Codes[DxfHeaderCode.DimensioningAssociative].Value=value;}
		}
		public OnOff DimensioningRecomputeWhileDragging{
			get=>(OnOff) Codes[DxfHeaderCode.DimensioningRecomputeWhileDragging].Value;
			set{Codes[DxfHeaderCode.DimensioningRecomputeWhileDragging].Value=value;}
		}
		public string DimensioningGeneralSuffix{
			get=>Codes[DxfHeaderCode.DimensioningGeneralSuffix].Value.ToString();
			set{Codes[DxfHeaderCode.DimensioningGeneralSuffix].Value=value;}
		}
		public string DimensioningAlternateSuffix{
			get=>Codes[DxfHeaderCode.DimensioningAlternateSuffix].Value.ToString();
			set{Codes[DxfHeaderCode.DimensioningAlternateSuffix].Value=value;}
		}
		public int DimensioningAlternateUnit{
			get=>(int) Codes[DxfHeaderCode.DimensioningAlternateUnit].Value;
			set{Codes[DxfHeaderCode.DimensioningAlternateUnit].Value=value;}
		}
		public int DimensioningAlternateUnitDecimalPlace{
			get=>(int) Codes[DxfHeaderCode.DimensioningAlternateUnitDecimalPlace].Value;
			set{Codes[DxfHeaderCode.DimensioningAlternateUnitDecimalPlace].Value=value;}
		}
		public double DimensioningAlternateUnitScaleFactor{
			get=>(double) Codes[DxfHeaderCode.DimensioningAlternateUnitScaleFactor].Value;
			set{Codes[DxfHeaderCode.DimensioningAlternateUnitScaleFactor].Value=value;}
		}
		public double DimensioningLinearScaleFactor{
			get=>(double) Codes[DxfHeaderCode.DimensioningLinearScaleFactor].Value;
			set{Codes[DxfHeaderCode.DimensioningLinearScaleFactor].Value=value;}
		}
		public OnOff DimensioningForceLineExtensions{
			get=>(OnOff) Codes[DxfHeaderCode.DimensioningForceLineExtensions].Value;
			set{Codes[DxfHeaderCode.DimensioningForceLineExtensions].Value=value;}
		}
		public double DimensionTextVerticalPosition{
			get=>(double) Codes[DxfHeaderCode.DimensionTextVerticalPosition].Value;
			set{Codes[DxfHeaderCode.DimensionTextVerticalPosition].Value=value;}
		}
		public OnOff DimensionForceTextInside{
			get=>(OnOff) Codes[DxfHeaderCode.DimensionForceTextInside].Value;
			set{Codes[DxfHeaderCode.DimensionForceTextInside].Value=value;}
		}
		public OnOff DimensionSuppressOutsideExtension{
			get=>(OnOff) Codes[DxfHeaderCode.DimensionSuppressOutsideExtension].Value;
			set{Codes[DxfHeaderCode.DimensionSuppressOutsideExtension].Value=value;}
		}
		public OnOff DimensionUseSeparateArrow{
			get=>(OnOff) Codes[DxfHeaderCode.DimensionUseSeparateArrow].Value;
			set{Codes[DxfHeaderCode.DimensionUseSeparateArrow].Value=value;}
		}
		public AciColor DimensionLineColor{
			get=>(AciColor) Codes[DxfHeaderCode.DimensionLineColor].Value;
			set{Codes[DxfHeaderCode.DimensionLineColor].Value=value;}
		}
		public AciColor DimensionExtensionLineColor{
			get=>(AciColor) Codes[DxfHeaderCode.DimensionExtensionLineColor].Value;
			set{Codes[DxfHeaderCode.DimensionExtensionLineColor].Value=value;}
		}
		public AciColor DimensionTextColor{
			get=>(AciColor) Codes[DxfHeaderCode.DimensionTextColor].Value;
			set{Codes[DxfHeaderCode.DimensionTextColor].Value=value;}
		}
		public double DimensionTextScalFactor{
			get=>(double) Codes[DxfHeaderCode.DimensionTextScalFactor].Value;
			set{Codes[DxfHeaderCode.DimensionTextScalFactor].Value=value;}
		}
		public double DimensionLineGap{
			get=>(double) Codes[DxfHeaderCode.DimensionLineGap].Value;
			set{Codes[DxfHeaderCode.DimensionLineGap].Value=value;}
		}
		public DimensionHorizontalTextPosition DimensionHorizontalTextPosition{
			get=>(DimensionHorizontalTextPosition) Codes[DxfHeaderCode.DimensionHorizontalTextPosition].Value;
			set{Codes[DxfHeaderCode.DimensionHorizontalTextPosition].Value=value;}
		}
		public OnOff DimensionSuppressFirstExtensionLine{
			get=>(OnOff) Codes[DxfHeaderCode.DimensionSuppressFirstExtensionLine].Value;
			set{Codes[DxfHeaderCode.DimensionSuppressFirstExtensionLine].Value=value;}
		}
		public OnOff DimensionSuppressSecondExtensionLine{
			get=>(OnOff) Codes[DxfHeaderCode.DimensionSuppressSecondExtensionLine].Value;
			set{Codes[DxfHeaderCode.DimensionSuppressSecondExtensionLine].Value=value;}
		}
		public DimensionVerticalTextPosition DimensionVerticalTextPosition{
			get=>(DimensionVerticalTextPosition) Codes[DxfHeaderCode.DimensionVerticalTextPosition].Value;
			set{Codes[DxfHeaderCode.DimensionVerticalTextPosition].Value=value;}
		}
		public ControlZeroSuppression ControlsSuppressionOfZerosForToleranceValue{
			get=>(ControlZeroSuppression) Codes[DxfHeaderCode.ControlsSuppressionOfZerosForToleranceValue].Value;
			set{Codes[DxfHeaderCode.ControlsSuppressionOfZerosForToleranceValue].Value=value;}
		}
		public ControlZeroSuppression ControlsSuppressionOfZerosForAlternateUnit{
			get=>(ControlZeroSuppression) Codes[DxfHeaderCode.ControlsSuppressionOfZerosForAlternateUnit].Value;
			set{Codes[DxfHeaderCode.ControlsSuppressionOfZerosForAlternateUnit].Value=value;}
		}
		public ControlZeroSuppression ControlsSuppressionOfZerosForAlternateToleranceValue{
			get=>(ControlZeroSuppression) Codes[DxfHeaderCode.ControlsSuppressionOfZerosForAlternateToleranceValue].Value;
			set{Codes[DxfHeaderCode.ControlsSuppressionOfZerosForAlternateToleranceValue].Value=value;}
		}
		public int DimensionCursor{
			get=>(int) Codes[DxfHeaderCode.DimensionCursor].Value;
			set{Codes[DxfHeaderCode.DimensionCursor].Value=value;}
		}
		public int DimensionNumberOfDecimalPlace{
			get=>(int) Codes[DxfHeaderCode.DimensionNumberOfDecimalPlace].Value;
			set{Codes[DxfHeaderCode.DimensionNumberOfDecimalPlace].Value=value;}
		}
		public int DimensionNumberOfDecimalPlaceToDisplayToleranceValue{
			get=>(int) Codes[DxfHeaderCode.DimensionNumberOfDecimalPlaceToDisplayToleranceValue].Value;
			set{Codes[DxfHeaderCode.DimensionNumberOfDecimalPlaceToDisplayToleranceValue].Value=value;}
		}
		public DimensionUnitFormatForAlternateUnit DimensionUnitFormatForAlternateUnit{
			get=>(DimensionUnitFormatForAlternateUnit) Codes[DxfHeaderCode.DimensionUnitFormatForAlternateUnit].Value;
			set{Codes[DxfHeaderCode.DimensionUnitFormatForAlternateUnit].Value=value;}
		}
	}
}


