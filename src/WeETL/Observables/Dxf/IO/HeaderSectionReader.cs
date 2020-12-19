using Microsoft.Extensions.Logging;
using System;
using WeETL.DependencyInjection;
using WeETL.Numerics;
using WeETL.Observables.Dxf.Header;
using WeETL.Observables.Dxf.Units;

namespace WeETL.Observables.Dxf.IO
{

    [DxfSection(DxfHeaderCode.HeaderSection)]
    public class HeaderSectionReader : SectionReader
    {
        public HeaderSectionReader(IServiceProvider serviceProvider, ILogger<HeaderSectionReader> logger) : base(serviceProvider, logger)
        {
        }
        protected override int GroupCode => EntityGroupCode.HeaderVar;
    }
    internal partial class HeaderSectionAcadVerReader 
    {
        public override void Read<TTYpe>((int, string) code)
        {
            var version = ServiceProvider.ResolveKeyed<IDxfVersion>(code.Item2);
            if (version == null)
                version = ServiceProvider.GetLastSupported() ?? throw new Exception("There is no support version ABORT..");
            Document.Header.AcadVer = version;
        }
    }
}
