using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeETL.DependencyInjection;
using WeETL.Observables.Dxf.Header;
using WeETL.Observables.Dxf.Units;

namespace WeETL.Observables.Dxf.IO
{

    [DxfSection(DxfObjectCode.HeaderSection)]
    public class HeaderSectionReader : SectionReader
    {
        public HeaderSectionReader(IServiceProvider serviceProvider,ILogger<HeaderSectionReader> logger) : base(serviceProvider,logger)
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
            if (double.TryParse(code.Item2, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
                Document.Header.Angbase = result;
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
            if (int.TryParse(code.Item2, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
                Document.Header.Angdir = (AngleDirection) result;
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
            if (int.TryParse(code.Item2, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
                Document.Header.AttMode = (AttributeVisibility)result;
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
            if (int.TryParse(code.Item2, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
                Document.Header.AUnits = result;
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
            if (short.TryParse(code.Item2, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
                Document.Header.AUprec = result;
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
            if (int.TryParse(code.Item2, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
                Document.Header.CeColor = result;
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
            if (double.TryParse(code.Item2, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
                Document.Header.CeLtScale = result;
        }
    }
}
