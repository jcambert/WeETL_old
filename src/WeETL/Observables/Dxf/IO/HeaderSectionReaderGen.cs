
using System;
using Microsoft.Extensions.Logging;
using WeETL.Observables.Dxf.Units;
using WeETL.Numerics;
namespace WeETL.Observables.Dxf.IO
{
	[DxfHeaderType(DxfHeaderCode.AcadVer)]
	internal partial class HeaderSectionAcadVerReader:AbstractReader{
		public HeaderSectionAcadVerReader(IServiceProvider sp, ILogger<HeaderSectionAcadVerReader> logger):base(sp,logger){}
	}
	[DxfHeaderType(DxfHeaderCode.HandleSeed)]
	internal partial class HeaderSectionHandleSeedReader:AbstractReader{
		public HeaderSectionHandleSeedReader(IServiceProvider sp, ILogger<HeaderSectionHandleSeedReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadString(code.Item2, result => Document.Header.HandleSeed = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.Angbase)]
	internal partial class HeaderSectionAngbaseReader:AbstractReader{
		public HeaderSectionAngbaseReader(IServiceProvider sp, ILogger<HeaderSectionAngbaseReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.Angbase = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.Angdir)]
	internal partial class HeaderSectionAngdirReader:AbstractReader{
		public HeaderSectionAngdirReader(IServiceProvider sp, ILogger<HeaderSectionAngdirReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.Angdir =(AngleDirection) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.AttMode)]
	internal partial class HeaderSectionAttModeReader:AbstractReader{
		public HeaderSectionAttModeReader(IServiceProvider sp, ILogger<HeaderSectionAttModeReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.AttMode =(AttributeVisibility) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.AUnits)]
	internal partial class HeaderSectionAUnitsReader:AbstractReader{
		public HeaderSectionAUnitsReader(IServiceProvider sp, ILogger<HeaderSectionAUnitsReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.AUnits = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.AUprec)]
	internal partial class HeaderSectionAUprecReader:AbstractReader{
		public HeaderSectionAUprecReader(IServiceProvider sp, ILogger<HeaderSectionAUprecReader> logger):base(sp,logger){}
	}
	[DxfHeaderType(DxfHeaderCode.CeColor)]
	internal partial class HeaderSectionCeColorReader:AbstractReader{
		public HeaderSectionCeColorReader(IServiceProvider sp, ILogger<HeaderSectionCeColorReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.CeColor = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.CeLtScale)]
	internal partial class HeaderSectionCeLtScaleReader:AbstractReader{
		public HeaderSectionCeLtScaleReader(IServiceProvider sp, ILogger<HeaderSectionCeLtScaleReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.CeLtScale = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.CeLtype)]
	internal partial class HeaderSectionCeLtypeReader:AbstractReader{
		public HeaderSectionCeLtypeReader(IServiceProvider sp, ILogger<HeaderSectionCeLtypeReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadString(code.Item2, result => Document.Header.CeLtype = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.CeLweight)]
	internal partial class HeaderSectionCeLweightReader:AbstractReader{
		public HeaderSectionCeLweightReader(IServiceProvider sp, ILogger<HeaderSectionCeLweightReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.CeLweight = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.CLayer)]
	internal partial class HeaderSectionCLayerReader:AbstractReader{
		public HeaderSectionCLayerReader(IServiceProvider sp, ILogger<HeaderSectionCLayerReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadString(code.Item2, result => Document.Header.CLayer = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.CMLJust)]
	internal partial class HeaderSectionCMLJustReader:AbstractReader{
		public HeaderSectionCMLJustReader(IServiceProvider sp, ILogger<HeaderSectionCMLJustReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.CMLJust = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.CMLScale)]
	internal partial class HeaderSectionCMLScaleReader:AbstractReader{
		public HeaderSectionCMLScaleReader(IServiceProvider sp, ILogger<HeaderSectionCMLScaleReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.CMLScale = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.CMLStyle)]
	internal partial class HeaderSectionCMLStyleReader:AbstractReader{
		public HeaderSectionCMLStyleReader(IServiceProvider sp, ILogger<HeaderSectionCMLStyleReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadString(code.Item2, result => Document.Header.CMLStyle = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimStyle)]
	internal partial class HeaderSectionDimStyleReader:AbstractReader{
		public HeaderSectionDimStyleReader(IServiceProvider sp, ILogger<HeaderSectionDimStyleReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadString(code.Item2, result => Document.Header.DimStyle = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DwgCodePage)]
	internal partial class HeaderSectionDwgCodePageReader:AbstractReader{
		public HeaderSectionDwgCodePageReader(IServiceProvider sp, ILogger<HeaderSectionDwgCodePageReader> logger):base(sp,logger){}
	}
	[DxfHeaderType(DxfHeaderCode.TextSize)]
	internal partial class HeaderSectionTextSizeReader:AbstractReader{
		public HeaderSectionTextSizeReader(IServiceProvider sp, ILogger<HeaderSectionTextSizeReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.TextSize = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.LUnits)]
	internal partial class HeaderSectionLUnitsReader:AbstractReader{
		public HeaderSectionLUnitsReader(IServiceProvider sp, ILogger<HeaderSectionLUnitsReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.LUnits = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.LUprec)]
	internal partial class HeaderSectionLUprecReader:AbstractReader{
		public HeaderSectionLUprecReader(IServiceProvider sp, ILogger<HeaderSectionLUprecReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.LUprec = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.MaintenanceVersion)]
	internal partial class HeaderSectionMaintenanceVersionReader:AbstractReader{
		public HeaderSectionMaintenanceVersionReader(IServiceProvider sp, ILogger<HeaderSectionMaintenanceVersionReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.MaintenanceVersion = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.Extnames)]
	internal partial class HeaderSectionExtnamesReader:AbstractReader{
		public HeaderSectionExtnamesReader(IServiceProvider sp, ILogger<HeaderSectionExtnamesReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.Extnames = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.InsBase)]
	internal partial class HeaderSectionInsBaseReader:AbstractVector3Reader{
		public HeaderSectionInsBaseReader(IServiceProvider sp, ILogger<HeaderSectionInsBaseReader> logger):base(sp,logger){}
		protected override void SetValue(Vector3 value)
        {
            Document.Header.InsBase = value;
        }
	}
	[DxfHeaderType(DxfHeaderCode.InsUnits)]
	internal partial class HeaderSectionInsUnitsReader:AbstractReader{
		public HeaderSectionInsUnitsReader(IServiceProvider sp, ILogger<HeaderSectionInsUnitsReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.InsUnits = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.LwDisplay)]
	internal partial class HeaderSectionLwDisplayReader:AbstractReader{
		public HeaderSectionLwDisplayReader(IServiceProvider sp, ILogger<HeaderSectionLwDisplayReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.LwDisplay = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.LtScale)]
	internal partial class HeaderSectionLtScaleReader:AbstractReader{
		public HeaderSectionLtScaleReader(IServiceProvider sp, ILogger<HeaderSectionLtScaleReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.LtScale = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.MirrText)]
	internal partial class HeaderSectionMirrTextReader:AbstractReader{
		public HeaderSectionMirrTextReader(IServiceProvider sp, ILogger<HeaderSectionMirrTextReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.MirrText = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.PdMode)]
	internal partial class HeaderSectionPdModeReader:AbstractReader{
		public HeaderSectionPdModeReader(IServiceProvider sp, ILogger<HeaderSectionPdModeReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.PdMode = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.PdSize)]
	internal partial class HeaderSectionPdSizeReader:AbstractReader{
		public HeaderSectionPdSizeReader(IServiceProvider sp, ILogger<HeaderSectionPdSizeReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.PdSize = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.PLineGen)]
	internal partial class HeaderSectionPLineGenReader:AbstractReader{
		public HeaderSectionPLineGenReader(IServiceProvider sp, ILogger<HeaderSectionPLineGenReader> logger):base(sp,logger){}
	}
	[DxfHeaderType(DxfHeaderCode.PsLtScale)]
	internal partial class HeaderSectionPsLtScaleReader:AbstractReader{
		public HeaderSectionPsLtScaleReader(IServiceProvider sp, ILogger<HeaderSectionPsLtScaleReader> logger):base(sp,logger){}
	}
	[DxfHeaderType(DxfHeaderCode.TdCreate)]
	internal partial class HeaderSectionTdCreateReader:AbstractReader{
		public HeaderSectionTdCreateReader(IServiceProvider sp, ILogger<HeaderSectionTdCreateReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDateTime(code.Item2, result => Document.Header.TdCreate = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.TduCreate)]
	internal partial class HeaderSectionTduCreateReader:AbstractReader{
		public HeaderSectionTduCreateReader(IServiceProvider sp, ILogger<HeaderSectionTduCreateReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDateTime(code.Item2, result => Document.Header.TduCreate = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.TdUpdate)]
	internal partial class HeaderSectionTdUpdateReader:AbstractReader{
		public HeaderSectionTdUpdateReader(IServiceProvider sp, ILogger<HeaderSectionTdUpdateReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDateTime(code.Item2, result => Document.Header.TdUpdate = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.TduUpdate)]
	internal partial class HeaderSectionTduUpdateReader:AbstractReader{
		public HeaderSectionTduUpdateReader(IServiceProvider sp, ILogger<HeaderSectionTduUpdateReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDateTime(code.Item2, result => Document.Header.TduUpdate = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.TdinDwg)]
	internal partial class HeaderSectionTdinDwgReader:AbstractReader{
		public HeaderSectionTdinDwgReader(IServiceProvider sp, ILogger<HeaderSectionTdinDwgReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadTimeSpan(code.Item2, result => Document.Header.TdinDwg = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.UcsOrg)]
	internal partial class HeaderSectionUcsOrgReader:AbstractVector3Reader{
		public HeaderSectionUcsOrgReader(IServiceProvider sp, ILogger<HeaderSectionUcsOrgReader> logger):base(sp,logger){}
		protected override void SetValue(Vector3 value)
        {
            Document.Header.UcsOrg = value;
        }
	}
	[DxfHeaderType(DxfHeaderCode.UcsXDir)]
	internal partial class HeaderSectionUcsXDirReader:AbstractVector3Reader{
		public HeaderSectionUcsXDirReader(IServiceProvider sp, ILogger<HeaderSectionUcsXDirReader> logger):base(sp,logger){}
		protected override void SetValue(Vector3 value)
        {
            Document.Header.UcsXDir = value;
        }
	}
	[DxfHeaderType(DxfHeaderCode.UcsYDir)]
	internal partial class HeaderSectionUcsYDirReader:AbstractVector3Reader{
		public HeaderSectionUcsYDirReader(IServiceProvider sp, ILogger<HeaderSectionUcsYDirReader> logger):base(sp,logger){}
		protected override void SetValue(Vector3 value)
        {
            Document.Header.UcsYDir = value;
        }
	}
	[DxfHeaderType(DxfHeaderCode.ExtMin)]
	internal partial class HeaderSectionExtMinReader:AbstractVector3Reader{
		public HeaderSectionExtMinReader(IServiceProvider sp, ILogger<HeaderSectionExtMinReader> logger):base(sp,logger){}
		protected override void SetValue(Vector3 value)
        {
            Document.Header.ExtMin = value;
        }
	}
	[DxfHeaderType(DxfHeaderCode.ExtMax)]
	internal partial class HeaderSectionExtMaxReader:AbstractVector3Reader{
		public HeaderSectionExtMaxReader(IServiceProvider sp, ILogger<HeaderSectionExtMaxReader> logger):base(sp,logger){}
		protected override void SetValue(Vector3 value)
        {
            Document.Header.ExtMax = value;
        }
	}
	[DxfHeaderType(DxfHeaderCode.LimMin)]
	internal partial class HeaderSectionLimMinReader:AbstractVector2Reader{
		public HeaderSectionLimMinReader(IServiceProvider sp, ILogger<HeaderSectionLimMinReader> logger):base(sp,logger){}
		protected override void SetValue(Vector2 value)
        {
            Document.Header.LimMin = value;
        }
	}
	[DxfHeaderType(DxfHeaderCode.LimMax)]
	internal partial class HeaderSectionLimMaxReader:AbstractVector2Reader{
		public HeaderSectionLimMaxReader(IServiceProvider sp, ILogger<HeaderSectionLimMaxReader> logger):base(sp,logger){}
		protected override void SetValue(Vector2 value)
        {
            Document.Header.LimMax = value;
        }
	}
	[DxfHeaderType(DxfHeaderCode.OrthogonalMode)]
	internal partial class HeaderSectionOrthogonalModeReader:AbstractReader{
		public HeaderSectionOrthogonalModeReader(IServiceProvider sp, ILogger<HeaderSectionOrthogonalModeReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.OrthogonalMode =(OnOff) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.RegenMode)]
	internal partial class HeaderSectionRegenModeReader:AbstractReader{
		public HeaderSectionRegenModeReader(IServiceProvider sp, ILogger<HeaderSectionRegenModeReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.RegenMode =(OnOff) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.FillMode)]
	internal partial class HeaderSectionFillModeReader:AbstractReader{
		public HeaderSectionFillModeReader(IServiceProvider sp, ILogger<HeaderSectionFillModeReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.FillMode =(OnOff) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.QteExtMode)]
	internal partial class HeaderSectionQteExtModeReader:AbstractReader{
		public HeaderSectionQteExtModeReader(IServiceProvider sp, ILogger<HeaderSectionQteExtModeReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.QteExtMode =(OnOff) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.TraceWidth)]
	internal partial class HeaderSectionTraceWidthReader:AbstractReader{
		public HeaderSectionTraceWidthReader(IServiceProvider sp, ILogger<HeaderSectionTraceWidthReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.TraceWidth = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.TextStyle)]
	internal partial class HeaderSectionTextStyleReader:AbstractReader{
		public HeaderSectionTextStyleReader(IServiceProvider sp, ILogger<HeaderSectionTextStyleReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadString(code.Item2, result => Document.Header.TextStyle = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DisplaySilhouette)]
	internal partial class HeaderSectionDisplaySilhouetteReader:AbstractReader{
		public HeaderSectionDisplaySilhouetteReader(IServiceProvider sp, ILogger<HeaderSectionDisplaySilhouetteReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DisplaySilhouette =(OnOff) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensioningScaleFactor)]
	internal partial class HeaderSectionDimensioningScaleFactorReader:AbstractReader{
		public HeaderSectionDimensioningScaleFactorReader(IServiceProvider sp, ILogger<HeaderSectionDimensioningScaleFactorReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.DimensioningScaleFactor = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensioningArrowSize)]
	internal partial class HeaderSectionDimensioningArrowSizeReader:AbstractReader{
		public HeaderSectionDimensioningArrowSizeReader(IServiceProvider sp, ILogger<HeaderSectionDimensioningArrowSizeReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.DimensioningArrowSize = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionExtensionLineOffset)]
	internal partial class HeaderSectionDimensionExtensionLineOffsetReader:AbstractReader{
		public HeaderSectionDimensionExtensionLineOffsetReader(IServiceProvider sp, ILogger<HeaderSectionDimensionExtensionLineOffsetReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.DimensionExtensionLineOffset = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionLineIncrement)]
	internal partial class HeaderSectionDimensionLineIncrementReader:AbstractReader{
		public HeaderSectionDimensionLineIncrementReader(IServiceProvider sp, ILogger<HeaderSectionDimensionLineIncrementReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.DimensionLineIncrement = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionRoudingValue)]
	internal partial class HeaderSectionDimensionRoudingValueReader:AbstractReader{
		public HeaderSectionDimensionRoudingValueReader(IServiceProvider sp, ILogger<HeaderSectionDimensionRoudingValueReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.DimensionRoudingValue = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionLineExtensions)]
	internal partial class HeaderSectionDimensionLineExtensionsReader:AbstractReader{
		public HeaderSectionDimensionLineExtensionsReader(IServiceProvider sp, ILogger<HeaderSectionDimensionLineExtensionsReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.DimensionLineExtensions = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionExtensionLineExtension)]
	internal partial class HeaderSectionDimensionExtensionLineExtensionReader:AbstractReader{
		public HeaderSectionDimensionExtensionLineExtensionReader(IServiceProvider sp, ILogger<HeaderSectionDimensionExtensionLineExtensionReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.DimensionExtensionLineExtension = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.TolerancePlus)]
	internal partial class HeaderSectionTolerancePlusReader:AbstractReader{
		public HeaderSectionTolerancePlusReader(IServiceProvider sp, ILogger<HeaderSectionTolerancePlusReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.TolerancePlus = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.ToleranceMinus)]
	internal partial class HeaderSectionToleranceMinusReader:AbstractReader{
		public HeaderSectionToleranceMinusReader(IServiceProvider sp, ILogger<HeaderSectionToleranceMinusReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.ToleranceMinus = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionTextHeight)]
	internal partial class HeaderSectionDimensionTextHeightReader:AbstractReader{
		public HeaderSectionDimensionTextHeightReader(IServiceProvider sp, ILogger<HeaderSectionDimensionTextHeightReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.DimensionTextHeight = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.SizeOfCenterMark)]
	internal partial class HeaderSectionSizeOfCenterMarkReader:AbstractReader{
		public HeaderSectionSizeOfCenterMarkReader(IServiceProvider sp, ILogger<HeaderSectionSizeOfCenterMarkReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.SizeOfCenterMark = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensioningTickSize)]
	internal partial class HeaderSectionDimensioningTickSizeReader:AbstractReader{
		public HeaderSectionDimensioningTickSizeReader(IServiceProvider sp, ILogger<HeaderSectionDimensioningTickSizeReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.DimensioningTickSize = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionTolerance)]
	internal partial class HeaderSectionDimensionToleranceReader:AbstractReader{
		public HeaderSectionDimensionToleranceReader(IServiceProvider sp, ILogger<HeaderSectionDimensionToleranceReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionTolerance = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionLimit)]
	internal partial class HeaderSectionDimensionLimitReader:AbstractReader{
		public HeaderSectionDimensionLimitReader(IServiceProvider sp, ILogger<HeaderSectionDimensionLimitReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionLimit = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.TextInsideHorizontal)]
	internal partial class HeaderSectionTextInsideHorizontalReader:AbstractReader{
		public HeaderSectionTextInsideHorizontalReader(IServiceProvider sp, ILogger<HeaderSectionTextInsideHorizontalReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.TextInsideHorizontal = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.TextOutsideHorizontal)]
	internal partial class HeaderSectionTextOutsideHorizontalReader:AbstractReader{
		public HeaderSectionTextOutsideHorizontalReader(IServiceProvider sp, ILogger<HeaderSectionTextOutsideHorizontalReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.TextOutsideHorizontal = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionFirstExtensionLineSuppressed)]
	internal partial class HeaderSectionDimensionFirstExtensionLineSuppressedReader:AbstractReader{
		public HeaderSectionDimensionFirstExtensionLineSuppressedReader(IServiceProvider sp, ILogger<HeaderSectionDimensionFirstExtensionLineSuppressedReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionFirstExtensionLineSuppressed =(OnOff) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionSecondExtensionLineSuppressed)]
	internal partial class HeaderSectionDimensionSecondExtensionLineSuppressedReader:AbstractReader{
		public HeaderSectionDimensionSecondExtensionLineSuppressedReader(IServiceProvider sp, ILogger<HeaderSectionDimensionSecondExtensionLineSuppressedReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionSecondExtensionLineSuppressed =(OnOff) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.TextAboveDimension)]
	internal partial class HeaderSectionTextAboveDimensionReader:AbstractReader{
		public HeaderSectionTextAboveDimensionReader(IServiceProvider sp, ILogger<HeaderSectionTextAboveDimensionReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.TextAboveDimension = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.ControlsSuppressionOfZerosForPrimaryUnit)]
	internal partial class HeaderSectionControlsSuppressionOfZerosForPrimaryUnitReader:AbstractReader{
		public HeaderSectionControlsSuppressionOfZerosForPrimaryUnitReader(IServiceProvider sp, ILogger<HeaderSectionControlsSuppressionOfZerosForPrimaryUnitReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.ControlsSuppressionOfZerosForPrimaryUnit =(ControlZeroSuppression) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.ArrowBlockName)]
	internal partial class HeaderSectionArrowBlockNameReader:AbstractReader{
		public HeaderSectionArrowBlockNameReader(IServiceProvider sp, ILogger<HeaderSectionArrowBlockNameReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadString(code.Item2, result => Document.Header.ArrowBlockName = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.FirstArrowBlockName)]
	internal partial class HeaderSectionFirstArrowBlockNameReader:AbstractReader{
		public HeaderSectionFirstArrowBlockNameReader(IServiceProvider sp, ILogger<HeaderSectionFirstArrowBlockNameReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadString(code.Item2, result => Document.Header.FirstArrowBlockName = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.SecondArrowBlockName)]
	internal partial class HeaderSectionSecondArrowBlockNameReader:AbstractReader{
		public HeaderSectionSecondArrowBlockNameReader(IServiceProvider sp, ILogger<HeaderSectionSecondArrowBlockNameReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadString(code.Item2, result => Document.Header.SecondArrowBlockName = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensioningAssociative)]
	internal partial class HeaderSectionDimensioningAssociativeReader:AbstractReader{
		public HeaderSectionDimensioningAssociativeReader(IServiceProvider sp, ILogger<HeaderSectionDimensioningAssociativeReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensioningAssociative =(OnOff) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensioningRecomputeWhileDragging)]
	internal partial class HeaderSectionDimensioningRecomputeWhileDraggingReader:AbstractReader{
		public HeaderSectionDimensioningRecomputeWhileDraggingReader(IServiceProvider sp, ILogger<HeaderSectionDimensioningRecomputeWhileDraggingReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensioningRecomputeWhileDragging =(OnOff) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensioningGeneralSuffix)]
	internal partial class HeaderSectionDimensioningGeneralSuffixReader:AbstractReader{
		public HeaderSectionDimensioningGeneralSuffixReader(IServiceProvider sp, ILogger<HeaderSectionDimensioningGeneralSuffixReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadString(code.Item2, result => Document.Header.DimensioningGeneralSuffix = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensioningAlternateSuffix)]
	internal partial class HeaderSectionDimensioningAlternateSuffixReader:AbstractReader{
		public HeaderSectionDimensioningAlternateSuffixReader(IServiceProvider sp, ILogger<HeaderSectionDimensioningAlternateSuffixReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadString(code.Item2, result => Document.Header.DimensioningAlternateSuffix = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensioningAlternateUnit)]
	internal partial class HeaderSectionDimensioningAlternateUnitReader:AbstractReader{
		public HeaderSectionDimensioningAlternateUnitReader(IServiceProvider sp, ILogger<HeaderSectionDimensioningAlternateUnitReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensioningAlternateUnit = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensioningAlternateUnitDecimalPlace)]
	internal partial class HeaderSectionDimensioningAlternateUnitDecimalPlaceReader:AbstractReader{
		public HeaderSectionDimensioningAlternateUnitDecimalPlaceReader(IServiceProvider sp, ILogger<HeaderSectionDimensioningAlternateUnitDecimalPlaceReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensioningAlternateUnitDecimalPlace = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensioningAlternateUnitScaleFactor)]
	internal partial class HeaderSectionDimensioningAlternateUnitScaleFactorReader:AbstractReader{
		public HeaderSectionDimensioningAlternateUnitScaleFactorReader(IServiceProvider sp, ILogger<HeaderSectionDimensioningAlternateUnitScaleFactorReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.DimensioningAlternateUnitScaleFactor = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensioningLinearScaleFactor)]
	internal partial class HeaderSectionDimensioningLinearScaleFactorReader:AbstractReader{
		public HeaderSectionDimensioningLinearScaleFactorReader(IServiceProvider sp, ILogger<HeaderSectionDimensioningLinearScaleFactorReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.DimensioningLinearScaleFactor = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensioningForceLineExtensions)]
	internal partial class HeaderSectionDimensioningForceLineExtensionsReader:AbstractReader{
		public HeaderSectionDimensioningForceLineExtensionsReader(IServiceProvider sp, ILogger<HeaderSectionDimensioningForceLineExtensionsReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensioningForceLineExtensions =(OnOff) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionTextVerticalPosition)]
	internal partial class HeaderSectionDimensionTextVerticalPositionReader:AbstractReader{
		public HeaderSectionDimensionTextVerticalPositionReader(IServiceProvider sp, ILogger<HeaderSectionDimensionTextVerticalPositionReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.DimensionTextVerticalPosition = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionForceTextInside)]
	internal partial class HeaderSectionDimensionForceTextInsideReader:AbstractReader{
		public HeaderSectionDimensionForceTextInsideReader(IServiceProvider sp, ILogger<HeaderSectionDimensionForceTextInsideReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionForceTextInside =(OnOff) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionSuppressOutsideExtension)]
	internal partial class HeaderSectionDimensionSuppressOutsideExtensionReader:AbstractReader{
		public HeaderSectionDimensionSuppressOutsideExtensionReader(IServiceProvider sp, ILogger<HeaderSectionDimensionSuppressOutsideExtensionReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionSuppressOutsideExtension =(OnOff) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionUseSeparateArrow)]
	internal partial class HeaderSectionDimensionUseSeparateArrowReader:AbstractReader{
		public HeaderSectionDimensionUseSeparateArrowReader(IServiceProvider sp, ILogger<HeaderSectionDimensionUseSeparateArrowReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionUseSeparateArrow =(OnOff) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionLineColor)]
	internal partial class HeaderSectionDimensionLineColorReader:AbstractAciColorReader{
		public HeaderSectionDimensionLineColorReader(IServiceProvider sp, ILogger<HeaderSectionDimensionLineColorReader> logger):base(sp,logger){}
		protected override void SetValue(AciColor value)
        {
            Document.Header.DimensionLineColor = value;
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionExtensionLineColor)]
	internal partial class HeaderSectionDimensionExtensionLineColorReader:AbstractAciColorReader{
		public HeaderSectionDimensionExtensionLineColorReader(IServiceProvider sp, ILogger<HeaderSectionDimensionExtensionLineColorReader> logger):base(sp,logger){}
		protected override void SetValue(AciColor value)
        {
            Document.Header.DimensionExtensionLineColor = value;
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionTextColor)]
	internal partial class HeaderSectionDimensionTextColorReader:AbstractAciColorReader{
		public HeaderSectionDimensionTextColorReader(IServiceProvider sp, ILogger<HeaderSectionDimensionTextColorReader> logger):base(sp,logger){}
		protected override void SetValue(AciColor value)
        {
            Document.Header.DimensionTextColor = value;
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionTextScalFactor)]
	internal partial class HeaderSectionDimensionTextScalFactorReader:AbstractReader{
		public HeaderSectionDimensionTextScalFactorReader(IServiceProvider sp, ILogger<HeaderSectionDimensionTextScalFactorReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.DimensionTextScalFactor = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionLineGap)]
	internal partial class HeaderSectionDimensionLineGapReader:AbstractReader{
		public HeaderSectionDimensionLineGapReader(IServiceProvider sp, ILogger<HeaderSectionDimensionLineGapReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.DimensionLineGap = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionHorizontalTextPosition)]
	internal partial class HeaderSectionDimensionHorizontalTextPositionReader:AbstractReader{
		public HeaderSectionDimensionHorizontalTextPositionReader(IServiceProvider sp, ILogger<HeaderSectionDimensionHorizontalTextPositionReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionHorizontalTextPosition =(DimensionHorizontalTextPosition) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionSuppressFirstExtensionLine)]
	internal partial class HeaderSectionDimensionSuppressFirstExtensionLineReader:AbstractReader{
		public HeaderSectionDimensionSuppressFirstExtensionLineReader(IServiceProvider sp, ILogger<HeaderSectionDimensionSuppressFirstExtensionLineReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionSuppressFirstExtensionLine =(OnOff) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionSuppressSecondExtensionLine)]
	internal partial class HeaderSectionDimensionSuppressSecondExtensionLineReader:AbstractReader{
		public HeaderSectionDimensionSuppressSecondExtensionLineReader(IServiceProvider sp, ILogger<HeaderSectionDimensionSuppressSecondExtensionLineReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionSuppressSecondExtensionLine =(OnOff) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionVerticalTextPosition)]
	internal partial class HeaderSectionDimensionVerticalTextPositionReader:AbstractReader{
		public HeaderSectionDimensionVerticalTextPositionReader(IServiceProvider sp, ILogger<HeaderSectionDimensionVerticalTextPositionReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionVerticalTextPosition =(DimensionVerticalTextPosition) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.ControlsSuppressionOfZerosForToleranceValue)]
	internal partial class HeaderSectionControlsSuppressionOfZerosForToleranceValueReader:AbstractReader{
		public HeaderSectionControlsSuppressionOfZerosForToleranceValueReader(IServiceProvider sp, ILogger<HeaderSectionControlsSuppressionOfZerosForToleranceValueReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.ControlsSuppressionOfZerosForToleranceValue =(ControlZeroSuppression) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.ControlsSuppressionOfZerosForAlternateUnit)]
	internal partial class HeaderSectionControlsSuppressionOfZerosForAlternateUnitReader:AbstractReader{
		public HeaderSectionControlsSuppressionOfZerosForAlternateUnitReader(IServiceProvider sp, ILogger<HeaderSectionControlsSuppressionOfZerosForAlternateUnitReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.ControlsSuppressionOfZerosForAlternateUnit =(ControlZeroSuppression) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.ControlsSuppressionOfZerosForAlternateToleranceValue)]
	internal partial class HeaderSectionControlsSuppressionOfZerosForAlternateToleranceValueReader:AbstractReader{
		public HeaderSectionControlsSuppressionOfZerosForAlternateToleranceValueReader(IServiceProvider sp, ILogger<HeaderSectionControlsSuppressionOfZerosForAlternateToleranceValueReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.ControlsSuppressionOfZerosForAlternateToleranceValue =(ControlZeroSuppression) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionCursor)]
	internal partial class HeaderSectionDimensionCursorReader:AbstractReader{
		public HeaderSectionDimensionCursorReader(IServiceProvider sp, ILogger<HeaderSectionDimensionCursorReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionCursor = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionNumberOfDecimalPlace)]
	internal partial class HeaderSectionDimensionNumberOfDecimalPlaceReader:AbstractReader{
		public HeaderSectionDimensionNumberOfDecimalPlaceReader(IServiceProvider sp, ILogger<HeaderSectionDimensionNumberOfDecimalPlaceReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionNumberOfDecimalPlace = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionNumberOfDecimalPlaceToDisplayToleranceValue)]
	internal partial class HeaderSectionDimensionNumberOfDecimalPlaceToDisplayToleranceValueReader:AbstractReader{
		public HeaderSectionDimensionNumberOfDecimalPlaceToDisplayToleranceValueReader(IServiceProvider sp, ILogger<HeaderSectionDimensionNumberOfDecimalPlaceToDisplayToleranceValueReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionNumberOfDecimalPlaceToDisplayToleranceValue = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionUnitFormatForAlternateUnit)]
	internal partial class HeaderSectionDimensionUnitFormatForAlternateUnitReader:AbstractReader{
		public HeaderSectionDimensionUnitFormatForAlternateUnitReader(IServiceProvider sp, ILogger<HeaderSectionDimensionUnitFormatForAlternateUnitReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionUnitFormatForAlternateUnit =(DimensionUnitFormatForAlternateUnit) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionNumberDecimaPlacesToleranceOfAlternateUnits)]
	internal partial class HeaderSectionDimensionNumberDecimaPlacesToleranceOfAlternateUnitsReader:AbstractReader{
		public HeaderSectionDimensionNumberDecimaPlacesToleranceOfAlternateUnitsReader(IServiceProvider sp, ILogger<HeaderSectionDimensionNumberDecimaPlacesToleranceOfAlternateUnitsReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionNumberDecimaPlacesToleranceOfAlternateUnits = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionTextStyle)]
	internal partial class HeaderSectionDimensionTextStyleReader:AbstractReader{
		public HeaderSectionDimensionTextStyleReader(IServiceProvider sp, ILogger<HeaderSectionDimensionTextStyleReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadString(code.Item2, result => Document.Header.DimensionTextStyle = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionAngleFormatForAngular)]
	internal partial class HeaderSectionDimensionAngleFormatForAngularReader:AbstractReader{
		public HeaderSectionDimensionAngleFormatForAngularReader(IServiceProvider sp, ILogger<HeaderSectionDimensionAngleFormatForAngularReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionAngleFormatForAngular =(AngleFormat) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionNumberPrecisionPlacesDisplayedAngular)]
	internal partial class HeaderSectionDimensionNumberPrecisionPlacesDisplayedAngularReader:AbstractReader{
		public HeaderSectionDimensionNumberPrecisionPlacesDisplayedAngularReader(IServiceProvider sp, ILogger<HeaderSectionDimensionNumberPrecisionPlacesDisplayedAngularReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionNumberPrecisionPlacesDisplayedAngular = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionRoundingAlternateUnits)]
	internal partial class HeaderSectionDimensionRoundingAlternateUnitsReader:AbstractReader{
		public HeaderSectionDimensionRoundingAlternateUnitsReader(IServiceProvider sp, ILogger<HeaderSectionDimensionRoundingAlternateUnitsReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.DimensionRoundingAlternateUnits = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionControlZeroForAngular)]
	internal partial class HeaderSectionDimensionControlZeroForAngularReader:AbstractReader{
		public HeaderSectionDimensionControlZeroForAngularReader(IServiceProvider sp, ILogger<HeaderSectionDimensionControlZeroForAngularReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionControlZeroForAngular =(DimensionControlZeroForAngular) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionSingleCharacterDecimalSeparator)]
	internal partial class HeaderSectionDimensionSingleCharacterDecimalSeparatorReader:AbstractReader{
		public HeaderSectionDimensionSingleCharacterDecimalSeparatorReader(IServiceProvider sp, ILogger<HeaderSectionDimensionSingleCharacterDecimalSeparatorReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionSingleCharacterDecimalSeparator = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionControlsTextAndArrowPlacementWhenSpaceIsNotSufficient)]
	internal partial class HeaderSectionDimensionControlsTextAndArrowPlacementWhenSpaceIsNotSufficientReader:AbstractReader{
		public HeaderSectionDimensionControlsTextAndArrowPlacementWhenSpaceIsNotSufficientReader(IServiceProvider sp, ILogger<HeaderSectionDimensionControlsTextAndArrowPlacementWhenSpaceIsNotSufficientReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionControlsTextAndArrowPlacementWhenSpaceIsNotSufficient = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionArrowBlockNameForLeaders)]
	internal partial class HeaderSectionDimensionArrowBlockNameForLeadersReader:AbstractReader{
		public HeaderSectionDimensionArrowBlockNameForLeadersReader(IServiceProvider sp, ILogger<HeaderSectionDimensionArrowBlockNameForLeadersReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadString(code.Item2, result => Document.Header.DimensionArrowBlockNameForLeaders = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionUnitsExceptAngular)]
	internal partial class HeaderSectionDimensionUnitsExceptAngularReader:AbstractReader{
		public HeaderSectionDimensionUnitsExceptAngularReader(IServiceProvider sp, ILogger<HeaderSectionDimensionUnitsExceptAngularReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionUnitsExceptAngular =(GeneralUnit) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionLineWeight)]
	internal partial class HeaderSectionDimensionLineWeightReader:AbstractReader{
		public HeaderSectionDimensionLineWeightReader(IServiceProvider sp, ILogger<HeaderSectionDimensionLineWeightReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionLineWeight = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionExtensionLineWeight)]
	internal partial class HeaderSectionDimensionExtensionLineWeightReader:AbstractReader{
		public HeaderSectionDimensionExtensionLineWeightReader(IServiceProvider sp, ILogger<HeaderSectionDimensionExtensionLineWeightReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionExtensionLineWeight = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.DimensionTextMovementRule)]
	internal partial class HeaderSectionDimensionTextMovementRuleReader:AbstractReader{
		public HeaderSectionDimensionTextMovementRuleReader(IServiceProvider sp, ILogger<HeaderSectionDimensionTextMovementRuleReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.DimensionTextMovementRule =(DimensionTextMovementRule) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.SketchRecordIncrement)]
	internal partial class HeaderSectionSketchRecordIncrementReader:AbstractReader{
		public HeaderSectionSketchRecordIncrementReader(IServiceProvider sp, ILogger<HeaderSectionSketchRecordIncrementReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.SketchRecordIncrement = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.FilletRadius)]
	internal partial class HeaderSectionFilletRadiusReader:AbstractReader{
		public HeaderSectionFilletRadiusReader(IServiceProvider sp, ILogger<HeaderSectionFilletRadiusReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.FilletRadius = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.NameOfMenuFile)]
	internal partial class HeaderSectionNameOfMenuFileReader:AbstractReader{
		public HeaderSectionNameOfMenuFileReader(IServiceProvider sp, ILogger<HeaderSectionNameOfMenuFileReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadString(code.Item2, result => Document.Header.NameOfMenuFile = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.CurrentElevation)]
	internal partial class HeaderSectionCurrentElevationReader:AbstractReader{
		public HeaderSectionCurrentElevationReader(IServiceProvider sp, ILogger<HeaderSectionCurrentElevationReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.CurrentElevation = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.CurrentPaperSpaceElevation)]
	internal partial class HeaderSectionCurrentPaperSpaceElevationReader:AbstractReader{
		public HeaderSectionCurrentPaperSpaceElevationReader(IServiceProvider sp, ILogger<HeaderSectionCurrentPaperSpaceElevationReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.CurrentPaperSpaceElevation = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.CurrentThicknessSetByElevation)]
	internal partial class HeaderSectionCurrentThicknessSetByElevationReader:AbstractReader{
		public HeaderSectionCurrentThicknessSetByElevationReader(IServiceProvider sp, ILogger<HeaderSectionCurrentThicknessSetByElevationReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.CurrentThicknessSetByElevation = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.NonzeroIfLimitsCheckingIsOn)]
	internal partial class HeaderSectionNonzeroIfLimitsCheckingIsOnReader:AbstractReader{
		public HeaderSectionNonzeroIfLimitsCheckingIsOnReader(IServiceProvider sp, ILogger<HeaderSectionNonzeroIfLimitsCheckingIsOnReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.NonzeroIfLimitsCheckingIsOn = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.FirstChamferDistance)]
	internal partial class HeaderSectionFirstChamferDistanceReader:AbstractReader{
		public HeaderSectionFirstChamferDistanceReader(IServiceProvider sp, ILogger<HeaderSectionFirstChamferDistanceReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.FirstChamferDistance = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.SecondChamferDistance)]
	internal partial class HeaderSectionSecondChamferDistanceReader:AbstractReader{
		public HeaderSectionSecondChamferDistanceReader(IServiceProvider sp, ILogger<HeaderSectionSecondChamferDistanceReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.SecondChamferDistance = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.ChamferLength)]
	internal partial class HeaderSectionChamferLengthReader:AbstractReader{
		public HeaderSectionChamferLengthReader(IServiceProvider sp, ILogger<HeaderSectionChamferLengthReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.ChamferLength = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.ChamferAngle)]
	internal partial class HeaderSectionChamferAngleReader:AbstractReader{
		public HeaderSectionChamferAngleReader(IServiceProvider sp, ILogger<HeaderSectionChamferAngleReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.ChamferAngle = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.SketchLineOrPloyline)]
	internal partial class HeaderSectionSketchLineOrPloylineReader:AbstractReader{
		public HeaderSectionSketchLineOrPloylineReader(IServiceProvider sp, ILogger<HeaderSectionSketchLineOrPloylineReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.SketchLineOrPloyline =(OnOff) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.UserElapsedTimer)]
	internal partial class HeaderSectionUserElapsedTimerReader:AbstractReader{
		public HeaderSectionUserElapsedTimerReader(IServiceProvider sp, ILogger<HeaderSectionUserElapsedTimerReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.UserElapsedTimer = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.UserTimerEnabled)]
	internal partial class HeaderSectionUserTimerEnabledReader:AbstractReader{
		public HeaderSectionUserTimerEnabledReader(IServiceProvider sp, ILogger<HeaderSectionUserTimerEnabledReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.UserTimerEnabled =(OnOff) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.PolylineDefaultWidth)]
	internal partial class HeaderSectionPolylineDefaultWidthReader:AbstractReader{
		public HeaderSectionPolylineDefaultWidthReader(IServiceProvider sp, ILogger<HeaderSectionPolylineDefaultWidthReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadDouble(code.Item2, result => Document.Header.PolylineDefaultWidth = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.EnableMeshSpline)]
	internal partial class HeaderSectionEnableMeshSplineReader:AbstractReader{
		public HeaderSectionEnableMeshSplineReader(IServiceProvider sp, ILogger<HeaderSectionEnableMeshSplineReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.EnableMeshSpline =(OnOff) result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.SplineCurveType)]
	internal partial class HeaderSectionSplineCurveTypeReader:AbstractReader{
		public HeaderSectionSplineCurveTypeReader(IServiceProvider sp, ILogger<HeaderSectionSplineCurveTypeReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.SplineCurveType = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.SplineNumberOfLine)]
	internal partial class HeaderSectionSplineNumberOfLineReader:AbstractReader{
		public HeaderSectionSplineNumberOfLineReader(IServiceProvider sp, ILogger<HeaderSectionSplineNumberOfLineReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.SplineNumberOfLine = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.NumberOfMeshTabulationsInFirstFirection)]
	internal partial class HeaderSectionNumberOfMeshTabulationsInFirstFirectionReader:AbstractReader{
		public HeaderSectionNumberOfMeshTabulationsInFirstFirectionReader(IServiceProvider sp, ILogger<HeaderSectionNumberOfMeshTabulationsInFirstFirectionReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.NumberOfMeshTabulationsInFirstFirection = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.NumberOfMeshTabulationsInSecondFirection)]
	internal partial class HeaderSectionNumberOfMeshTabulationsInSecondFirectionReader:AbstractReader{
		public HeaderSectionNumberOfMeshTabulationsInSecondFirectionReader(IServiceProvider sp, ILogger<HeaderSectionNumberOfMeshTabulationsInSecondFirectionReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.NumberOfMeshTabulationsInSecondFirection = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.SurfaceTypeForSmooth)]
	internal partial class HeaderSectionSurfaceTypeForSmoothReader:AbstractReader{
		public HeaderSectionSurfaceTypeForSmoothReader(IServiceProvider sp, ILogger<HeaderSectionSurfaceTypeForSmoothReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.SurfaceTypeForSmooth = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.SurfaceDensityInMDirection)]
	internal partial class HeaderSectionSurfaceDensityInMDirectionReader:AbstractReader{
		public HeaderSectionSurfaceDensityInMDirectionReader(IServiceProvider sp, ILogger<HeaderSectionSurfaceDensityInMDirectionReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.SurfaceDensityInMDirection = result);
        }
	}
	[DxfHeaderType(DxfHeaderCode.SurfaceDensityInNDirection)]
	internal partial class HeaderSectionSurfaceDensityInNDirectionReader:AbstractReader{
		public HeaderSectionSurfaceDensityInNDirectionReader(IServiceProvider sp, ILogger<HeaderSectionSurfaceDensityInNDirectionReader> logger):base(sp,logger){}
		public override void Read<DxfEntityTypeAttribute>((int, string) code)
        {
             ReadInt(code.Item2, result => Document.Header.SurfaceDensityInNDirection = result);
        }
	}
}
