using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeETL.DependencyInjection;
using WeETL.Numerics;
using WeETL.Observables.Dxf.Header;
using WeETL.Observables.Dxf.Units;

namespace WeETL.Observables.Dxf.IO
{

    [DxfSection(DxfObjectCode.HeaderSection)]
    public class HeaderSectionReader : SectionReader
    {
        public HeaderSectionReader(IServiceProvider serviceProvider, ILogger<HeaderSectionReader> logger) : base(serviceProvider, logger)
        {
        }
        protected override int GroupCode => EntityGroupCode.HeaderVar;
    }
    [DxfEntityType(DxfHeaderCode.AcadVer)]
    internal class HeaderSectionAcadVerReader : AbstractReader
    {
        public HeaderSectionAcadVerReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            var version = ServiceProvider.ResolveKeyed<IDxfVersion>(code.Item2);
            if (version == null)
                version = ServiceProvider.GetLastSupported() ?? throw new Exception("There is no support version ABORT..");
            Document.Header.AcadVer = version;
        }
    }
    [DxfEntityType(DxfHeaderCode.Angbase)]
    internal class HeaderSectionAngBaseReader : AbstractReader
    {
        public HeaderSectionAngBaseReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, (value)=> Document.Header.Angbase=value);
        }
    }
    [DxfEntityType(DxfHeaderCode.Angdir)]
    internal class HeaderSectionAngdirReader : AbstractReader
    {
        public HeaderSectionAngdirReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, value => Document.Header.Angdir = (AngleDirection)value);
        }
    }
    [DxfEntityType(DxfHeaderCode.AttMode)]
    internal class HeaderSectionAttModeReader : AbstractReader
    {
        public HeaderSectionAttModeReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.AttMode=(AttributeVisibility)result);
        }
    }
    [DxfEntityType(DxfHeaderCode.AUnits)]
    internal class HeaderSectionAUnitsReader : AbstractReader
    {
        public HeaderSectionAUnitsReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.AUnits = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.AUprec)]
    internal class HeaderSectionAUprecReader : AbstractReader
    {
        public HeaderSectionAUprecReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadShort(code.Item2, result => Document.Header.AUprec = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.CeColor)]
    internal class HeaderSectionCeColorReader : AbstractReader
    {
        public HeaderSectionCeColorReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.CeColor = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.CeLtScale)]
    internal class HeaderSectionCeLtScaleReader : AbstractReader
    {
        public HeaderSectionCeLtScaleReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2,result=> Document.Header.CeLtScale = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.CeLtype)]
    internal class HeaderSectionCeLtypeReader : AbstractReader
    {
        public HeaderSectionCeLtypeReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            Document.Header.CeLtype = code.Item2;
        }
    }
    [DxfEntityType(DxfHeaderCode.CeLweight)]
    internal class HeaderSectionCeLweightReader : AbstractReader
    {
        public HeaderSectionCeLweightReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2,result=> Document.Header.CeLweight = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.CLayer)]
    internal class HeaderSectionCLayerReader : AbstractReader
    {
        public HeaderSectionCLayerReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            Document.Header.CLayer = code.Item2;
        }
    }
    [DxfEntityType(DxfHeaderCode.CMLJust)]
    internal class HeaderSectionCMLJustReader : AbstractReader
    {
        public HeaderSectionCMLJustReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2,result=> Document.Header.CMLJust = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.CMLScale)]
    internal class HeaderSectionCMLScaleReader : AbstractReader
    {
        public HeaderSectionCMLScaleReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.CMLScale = result);
        }

    }
    [DxfEntityType(DxfHeaderCode.CMLStyle)]
    internal class HeaderSectionCMLStyleReader : AbstractReader
    {
        public HeaderSectionCMLStyleReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadString(code.Item2, result => Document.Header.CMLStyle = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimStyle)]
    internal class HeaderSectionDimStyleReader : AbstractReader
    {
        public HeaderSectionDimStyleReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadString(code.Item2, result => Document.Header.DimStyle = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DwgCodePage)]
    internal class HeaderSectionDwgCodePageReader : AbstractReader
    {
        public HeaderSectionDwgCodePageReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadString(code.Item2, result => Document.Header.DwgCodePage = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.Extnames)]
    internal class HeaderSectionExtnamesReader : AbstractReader
    {
        public HeaderSectionExtnamesReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.Extnames = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.HandleSeed)]
    internal class HeaderSectionHandleSeedReader : AbstractReader
    {
        public HeaderSectionHandleSeedReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadString(code.Item2, result => Document.Header.HandleSeed = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.InsBase)]
    internal class HeaderSectionInsBaseReader : AbstractVector3Reader
    {
        public HeaderSectionInsBaseReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override void SetValue(Vector3 value)
        {
            Document.Header.InsBase = value;
        }
    }
    [DxfEntityType(DxfHeaderCode.InsUnits)]
    internal class HeaderSectionInsUnitsReader : AbstractReader
    {
        public HeaderSectionInsUnitsReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.InsUnits = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.LtScale)]
    internal class HeaderSectionLtScaleReader : AbstractReader
    {
        public HeaderSectionLtScaleReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.LtScale = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.LUnits)]
    internal class HeaderSectionLUnitsReader : AbstractReader
    {
        public HeaderSectionLUnitsReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.LUnits = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.LUprec)]
    internal class HeaderSectionLUprecReader : AbstractReader
    {
        public HeaderSectionLUprecReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.LUprec = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.LwDisplay)]
    internal class HeaderSectionLwDisplayReader : AbstractReader
    {
        public HeaderSectionLwDisplayReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.LwDisplay = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.MaintenanceVersion)]
    internal class HeaderSectionMaintenanceVersionReader : AbstractReader
    {
        public HeaderSectionMaintenanceVersionReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.MaintenanceVersion = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.MirrText)]
    internal class HeaderSectionMirrTextReader : AbstractReader
    {
        public HeaderSectionMirrTextReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.MirrText = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.PdMode)]
    internal class HeaderSectionPdModeReader : AbstractReader
    {
        public HeaderSectionPdModeReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.PdMode = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.PdSize)]
    internal class HeaderSectionPdSizeReader : AbstractReader
    {
        public HeaderSectionPdSizeReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.PdSize = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.PLineGen)]
    internal class HeaderSectionPLineGenReader : AbstractReader
    {
        public HeaderSectionPLineGenReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadShort(code.Item2, result => Document.Header.PLineGen = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.PsLtScale)]
    internal class HeaderSectionPsLtScaleReader : AbstractReader
    {
        public HeaderSectionPsLtScaleReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadShort(code.Item2, result => Document.Header.PsLtScale = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.TdCreate)]
    internal class HeaderSectionTdCreateReader : AbstractReader
    {
        public HeaderSectionTdCreateReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDateTime(code.Item2, result => Document.Header.TdCreate = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.TdinDwg)]
    internal class HeaderSectionTdinDwgReader : AbstractReader
    {
        public HeaderSectionTdinDwgReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadTimeSpan(code.Item2, result => Document.Header.TdinDwg = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.TduCreate)]
    internal class HeaderSectionTduCreateReader : AbstractReader
    {
        public HeaderSectionTduCreateReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDateTime(code.Item2, result => Document.Header.TduCreate = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.TdUpdate)]
    internal class HeaderSectionTdUpdateReader : AbstractReader
    {
        public HeaderSectionTdUpdateReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDateTime(code.Item2, result => Document.Header.TdUpdate = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.TduUpdate)]
    internal class HeaderSectionTduUpdateReader : AbstractReader
    {
        public HeaderSectionTduUpdateReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDateTime(code.Item2, result => Document.Header.TduUpdate = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.TextSize)]
    internal class HeaderSectionTextSizeReader : AbstractReader
    {
        public HeaderSectionTextSizeReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.TextSize = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.UcsOrg)]
    internal class HeaderSectionUcsOrgReader : AbstractVector3Reader
    {
        public HeaderSectionUcsOrgReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override void SetValue(Vector3 value)
        {
            Document.Header.UcsOrg = value;
        }
    }
    [DxfEntityType(DxfHeaderCode.UcsXDir)]
    internal class HeaderSectionUcsXDirReader : AbstractVector3Reader
    {
        public HeaderSectionUcsXDirReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override void SetValue(Vector3 value)
        {
            Document.Header.UcsXDir = value;
        }
    }
    [DxfEntityType(DxfHeaderCode.UcsYDir)]
    internal class HeaderSectionUcsYDirReader : AbstractVector3Reader
    {
        public HeaderSectionUcsYDirReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override void SetValue(Vector3 value)
        {
            Document.Header.UcsYDir = value;
        }
    }
    [DxfEntityType(DxfHeaderCode.ExtMin)]
    internal class HeaderSectionExtMinReader : AbstractVector3Reader
    {
        public HeaderSectionExtMinReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override void SetValue(Vector3 value)
        {
            Document.Header.ExtMin = value;
        }
    }
    [DxfEntityType(DxfHeaderCode.ExtMax)]
    internal class HeaderSectionExtMaxReader : AbstractVector3Reader
    {
        public HeaderSectionExtMaxReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override void SetValue(Vector3 value)
        {
            Document.Header.ExtMax = value;
        }
    }
    [DxfEntityType(DxfHeaderCode.LimMin)]
    internal class HeaderSectionLimMinReader : AbstractVector2Reader
    {
        public HeaderSectionLimMinReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override void SetValue(Vector2 value)
        {
            Document.Header.LimMin = value;
        }
    }
    [DxfEntityType(DxfHeaderCode.LimMax)]
    internal class HeaderSectionLimMaxReader : AbstractVector2Reader
    {
        public HeaderSectionLimMaxReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override void SetValue(Vector2 value)
        {
            Document.Header.LimMax = value;
        }
    }
    [DxfEntityType(DxfHeaderCode.OrthogonalMode)]
    internal class HeaderSectionOrthogonalModeReader : AbstractReader
    {
        public HeaderSectionOrthogonalModeReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadOnOff(code.Item2, result => Document.Header.OrthogonalMode =result);
        }
    }
    [DxfEntityType(DxfHeaderCode.RegenMode)]
    internal class HeaderSectionRegenModeReader : AbstractReader
    {
        public HeaderSectionRegenModeReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadOnOff(code.Item2, result => Document.Header.RegenMode = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.FillMode)]
    internal class HeaderSectionFillModeReader : AbstractReader
    {
        public HeaderSectionFillModeReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadOnOff(code.Item2, result => Document.Header.FillMode = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.QteExtMode)]
    internal class HeaderSectionQteExtModeReader : AbstractReader
    {
        public HeaderSectionQteExtModeReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadOnOff(code.Item2, result => Document.Header.QteExtMode =result);
        }
    }
    [DxfEntityType(DxfHeaderCode.TraceWidth)]
    internal class HeaderSectionTraceWidthReader : AbstractReader
    {
        public HeaderSectionTraceWidthReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.TraceWidth = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.TextStyle)]
    internal class HeaderSectionTextStyleReader : AbstractReader
    {
        public HeaderSectionTextStyleReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadString(code.Item2, result => Document.Header.TextStyle = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DisplaySilhouette)]
    internal class HeaderSectionDisplaySilhouetteReader : AbstractReader
    {
        public HeaderSectionDisplaySilhouetteReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadOnOff(code.Item2, result => Document.Header.DisplaySilhouette = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensioningScaleFactor)]
    internal class HeaderSectionDimensioningScaleFactorReader : AbstractReader
    {
        public HeaderSectionDimensioningScaleFactorReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.DimensioningScaleFactor = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensioningArrowSize)]
    internal class HeaderSectionDimensioningArrowSizeReader : AbstractReader
    {
        public HeaderSectionDimensioningArrowSizeReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.DimensioningArrowSize = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionExtensionLineOffset)]
    internal class HeaderSectionExtensionLineOffsetReader : AbstractReader
    {
        public HeaderSectionExtensionLineOffsetReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.DimensionExtensionLineOffset = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionLineIncrement)]
    internal class HeaderSectionDimensionLineIncrementReader : AbstractReader
    {
        public HeaderSectionDimensionLineIncrementReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.DimensionLineIncrement = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionRoudingValue)]
    internal class HeaderSectionDimensionRoudingValueReader : AbstractReader
    {
        public HeaderSectionDimensionRoudingValueReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.DimensionRoudingValue = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionLineExtensions)]
    internal class HeaderSectionDimensionLineExtensionsReader : AbstractReader
    {
        public HeaderSectionDimensionLineExtensionsReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.DimensionLineExtensions = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionExtensionLineExtension)]
    internal class HeaderSectionDimensionExtensionLineExtensionReader : AbstractReader
    {
        public HeaderSectionDimensionExtensionLineExtensionReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.DimensionExtensionLineExtension = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.TolerancePlus)]
    internal class HeaderSectionTolerancePlusReader : AbstractReader
    {
        public HeaderSectionTolerancePlusReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.TolerancePlus = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.ToleranceMinus)]
    internal class HeaderSectionToleranceMinusReader : AbstractReader
    {
        public HeaderSectionToleranceMinusReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.ToleranceMinus = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionTextHeight)]
    internal class HeaderSectionDimensionTextHeightReader : AbstractReader
    {
        public HeaderSectionDimensionTextHeightReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.DimensionTextHeight = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.SizeOfCenterMark)]
    internal class HeaderSectionSizeOfCenterMarkReader : AbstractReader
    {
        public HeaderSectionSizeOfCenterMarkReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.SizeOfCenterMark = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensioningTickSize)]
    internal class HeaderSectionDimensioningTickSizeReader : AbstractReader
    {
        public HeaderSectionDimensioningTickSizeReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.DimensioningTickSize = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionTolerance)]
    internal class HeaderSectionDimensionToleranceReader : AbstractReader
    {
        public HeaderSectionDimensionToleranceReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.DimensionTolerance = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionLimit)]
    internal class HeaderSectionDimensionLimitReader : AbstractReader
    {
        public HeaderSectionDimensionLimitReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.DimensionLimit = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.TextInsideHorizontal)]
    internal class HeaderSectionTextInsideHorizontalReader : AbstractReader
    {
        public HeaderSectionTextInsideHorizontalReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.TextInsideHorizontal = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.TextOutsideHorizontal)]
    internal class HeaderSectionTextOutsideHorizontalReader : AbstractReader
    {
        public HeaderSectionTextOutsideHorizontalReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.TextOutsideHorizontal = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionFirstExtensionLineSuppressed)]
    internal class HeaderSectionDimensionFirstExtensionLineSuppressedReader : AbstractReader
    {
        public HeaderSectionDimensionFirstExtensionLineSuppressedReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadOnOff(code.Item2, result => Document.Header.DimensionFirstExtensionLineSuppressed = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionSecondExtensionLineSuppressed)]
    internal class HeaderSectionDimensionSecondExtensionLineSuppressedReader : AbstractReader
    {
        public HeaderSectionDimensionSecondExtensionLineSuppressedReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadOnOff(code.Item2, result => Document.Header.DimensionSecondExtensionLineSuppressed = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.TextAboveDimension)]
    internal class HeaderSectionTextAboveDimensionReader : AbstractReader
    {
        public HeaderSectionTextAboveDimensionReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.TextAboveDimension = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.ControlsSuppressionOfZerosForPrimaryUnit)]
    internal class HeaderSectionControlsSuppressionOfZerosForPrimaryUnitReader : AbstractReader
    {
        public HeaderSectionControlsSuppressionOfZerosForPrimaryUnitReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.ControlsSuppressionOfZerosForPrimaryUnit =(ControlZeroSuppression) result);
        }
    }
    [DxfEntityType(DxfHeaderCode.ArrowBlockName)]
    internal class HeaderSectionArrowBlockNameReader : AbstractReader
    {
        public HeaderSectionArrowBlockNameReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadString(code.Item2, result => Document.Header.ArrowBlockName =result);
        }
    }
    [DxfEntityType(DxfHeaderCode.FirstArrowBlockName)]
    internal class HeaderSectionFirstArrowBlockNameReader : AbstractReader
    {
        public HeaderSectionFirstArrowBlockNameReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadString(code.Item2, result => Document.Header.FirstArrowBlockName = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.SecondArrowBlockName)]
    internal class HeaderSectionSecondArrowBlockNameReader : AbstractReader
    {
        public HeaderSectionSecondArrowBlockNameReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadString(code.Item2, result => Document.Header.SecondArrowBlockName = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensioningAssociative)]
    internal class HeaderSectionDimensioningAssociativeReader : AbstractReader
    {
        public HeaderSectionDimensioningAssociativeReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadOnOff(code.Item2, result => Document.Header.DimensioningAssociative = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensioningRecomputeWhileDragging)]
    internal class HeaderSectionDimensioningRecomputeWhileDraggingReader : AbstractReader
    {
        public HeaderSectionDimensioningRecomputeWhileDraggingReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadOnOff(code.Item2, result => Document.Header.DimensioningRecomputeWhileDragging = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensioningGeneralSuffix)]
    internal class HeaderSectionDimensioningGeneralSuffixReader : AbstractReader
    {
        public HeaderSectionDimensioningGeneralSuffixReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadString(code.Item2, result => Document.Header.DimensioningGeneralSuffix = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensioningAlternateSuffix)]
    internal class HeaderSectionDimensioningAlternateSuffixReader : AbstractReader
    {
        public HeaderSectionDimensioningAlternateSuffixReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadString(code.Item2, result => Document.Header.DimensioningAlternateSuffix = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensioningAlternateUnit)]
    internal class HeaderSectionDimensioningAlternateUnitReader : AbstractReader
    {
        public HeaderSectionDimensioningAlternateUnitReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.DimensioningAlternateUnit = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensioningAlternateUnitDecimalPlace)]
    internal class HeaderSectionDimensioningAlternateUnitDecimalPlaceReader : AbstractReader
    {
        public HeaderSectionDimensioningAlternateUnitDecimalPlaceReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.DimensioningAlternateUnitDecimalPlace = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensioningAlternateUnitScaleFactor)]
    internal class HeaderSectionDimensioningAlternateUnitScaleFactorReader : AbstractReader
    {
        public HeaderSectionDimensioningAlternateUnitScaleFactorReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.DimensioningAlternateUnitScaleFactor = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensioningLinearScaleFactor)]
    internal class HeaderSectionDimensioningLinearScaleFactorReader : AbstractReader
    {
        public HeaderSectionDimensioningLinearScaleFactorReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.DimensioningLinearScaleFactor = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensioningForceLineExtensions)]
    internal class HeaderSectionDimensioningForceLineExtensionsReader : AbstractReader
    {
        public HeaderSectionDimensioningForceLineExtensionsReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadOnOff(code.Item2, result => Document.Header.DimensioningForceLineExtensions = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionTextVerticalPosition)]
    internal class HeaderSectionDimensionTextVerticalPositionReader : AbstractReader
    {
        public HeaderSectionDimensionTextVerticalPositionReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.DimensionTextVerticalPosition = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionForceTextInside)]
    internal class HeaderSectionDimensionForceTextInsideReader : AbstractReader
    {
        public HeaderSectionDimensionForceTextInsideReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadOnOff(code.Item2, result => Document.Header.DimensionForceTextInside = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionSuppressOutsideExtension)]
    internal class HeaderSectionDimensionSuppressOutsideExtensionReader : AbstractReader
    {
        public HeaderSectionDimensionSuppressOutsideExtensionReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadOnOff(code.Item2, result => Document.Header.DimensionSuppressOutsideExtension = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionUseSeparateArrow)]
    internal class HeaderSectionDimensionUseSeparateArrowReader : AbstractReader
    {
        public HeaderSectionDimensionUseSeparateArrowReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadOnOff(code.Item2, result => Document.Header.DimensionUseSeparateArrow = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionLineColor)]
    internal class HeaderSectionDimensionLineColorReader : AbstractAciColorReader
    {
        public HeaderSectionDimensionLineColorReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }


        protected override void SetValue(AciColor color)
        {
            Document.Header.DimensionLineColor = color;
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionExtensionLineColor)]
    internal class HeaderSectionDimensionExtensionLineColorReader : AbstractAciColorReader
    {
        public HeaderSectionDimensionExtensionLineColorReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }


        protected override void SetValue(AciColor color)
        {
            Document.Header.DimensionExtensionLineColor = color;
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionTextColor)]
    internal class HeaderSectionDimensionTextColorReader : AbstractAciColorReader
    {
        public HeaderSectionDimensionTextColorReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }


        protected override void SetValue(AciColor color)
        {
            Document.Header.DimensionTextColor = color;
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionTextScalFactor)]
    internal class HeaderSectionDimensionTextScalFactorReader : AbstractReader
    {
        public HeaderSectionDimensionTextScalFactorReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.DimensionTextScalFactor = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionLineGap)]
    internal class HeaderSectionDimensionLineGapReader : AbstractReader
    {
        public HeaderSectionDimensionLineGapReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadDouble(code.Item2, result => Document.Header.DimensionLineGap = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionHorizontalTextPosition)]
    internal class HeaderSectionDimensionHorizontalTextPositionReader : AbstractReader
    {
        public HeaderSectionDimensionHorizontalTextPositionReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.DimensionHorizontalTextPosition =(DimensionHorizontalTextPosition) result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionSuppressFirstExtensionLine)]
    internal class HeaderSectionDimensionSuppressFirstExtensionLineReader : AbstractReader
    {
        public HeaderSectionDimensionSuppressFirstExtensionLineReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadOnOff(code.Item2, result => Document.Header.DimensionSuppressFirstExtensionLine = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionSuppressSecondExtensionLine)]
    internal class HeaderSectionDimensionSuppressSecondExtensionLineReader : AbstractReader
    {
        public HeaderSectionDimensionSuppressSecondExtensionLineReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadOnOff(code.Item2, result => Document.Header.DimensionSuppressSecondExtensionLine= result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionVerticalTextPosition)]
    internal class HeaderSectionDimensionVerticalJustificationReader : AbstractReader
    {
        public HeaderSectionDimensionVerticalJustificationReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.DimensionVerticalTextPosition = (DimensionVerticalTextPosition) result);
        }
    }
    [DxfEntityType(DxfHeaderCode.ControlsSuppressionOfZerosForToleranceValue)]
    internal class HeaderSectionControlsSuppressionOfZerosForToleranceValueReader : AbstractReader
    {
        public HeaderSectionControlsSuppressionOfZerosForToleranceValueReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.ControlsSuppressionOfZerosForToleranceValue = (ControlZeroSuppression)result);
        }
    }
    [DxfEntityType(DxfHeaderCode.ControlsSuppressionOfZerosForAlternateUnit)]
    internal class HeaderSectionControlsSuppressionOfZerosForAlternateUnitReader : AbstractReader
    {
        public HeaderSectionControlsSuppressionOfZerosForAlternateUnitReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.ControlsSuppressionOfZerosForAlternateUnit = (ControlZeroSuppression)result);
        }
    }
    [DxfEntityType(DxfHeaderCode.ControlsSuppressionOfZerosForAlternateToleranceValue)]
    internal class HeaderSectionControlsSuppressionOfZerosForAlternateToleranceValueReader : AbstractReader
    {
        public HeaderSectionControlsSuppressionOfZerosForAlternateToleranceValueReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.ControlsSuppressionOfZerosForAlternateToleranceValue = (ControlZeroSuppression)result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionCursor)]
    internal class HeaderSectionDimensionCursorReader : AbstractReader
    {
        public HeaderSectionDimensionCursorReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.DimensionCursor = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionNumberOfDecimalPlace)]
    internal class HeaderSectionDimensionNumberOfDecimalPlaceReader : AbstractReader
    {
        public HeaderSectionDimensionNumberOfDecimalPlaceReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.DimensionNumberOfDecimalPlace = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionNumberOfDecimalPlaceToDisplayToleranceValue)]
    internal class HeaderSectionDimensionNumberOfDecimalPlaceToDisplayToleranceValueReader : AbstractReader
    {
        public HeaderSectionDimensionNumberOfDecimalPlaceToDisplayToleranceValueReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.DimensionNumberOfDecimalPlaceToDisplayToleranceValue = result);
        }
    }
    [DxfEntityType(DxfHeaderCode.DimensionUnitFormatForAlternateUnit)]
    internal class HeaderSectionDimensionUnitFormatForAlternateUnitReader : AbstractReader
    {
        public HeaderSectionDimensionUnitFormatForAlternateUnitReader(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Read((int, string) code)
        {
            ReadInt(code.Item2, result => Document.Header.DimensionUnitFormatForAlternateUnit =(DimensionUnitFormatForAlternateUnit) result);
        }
    }
    
}
