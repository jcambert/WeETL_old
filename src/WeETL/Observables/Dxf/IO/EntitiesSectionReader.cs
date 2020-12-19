using WeETL.Observables.Dxf.Entities;
using System;
using Microsoft.Extensions.Logging;
using WeETL.DependencyInjection;
using WeETL.Numerics;
#if DEBUG
using System.Diagnostics;
#endif

namespace WeETL.Observables.Dxf.IO
{
    [DxfSection(DxfEntityCode.EntitiesSection)]
    public class EntitySectionReader : SectionReader
    {
        public EntitySectionReader(IServiceProvider serviceProvider, ILogger<EntitySectionReader> logger) : base(serviceProvider, logger)
        {
        }
    }
        
    internal partial class EntitySectionLineReader 
    {

      /*  readonly Vector3Reader v3Reader = new Vector3Reader();
        
        protected new Line DxfObject { get; set; } = new Line() {Normal = Vector3.UnitZ };
        public override void Read<TType>((int, string) code)
        {
            //Logger.LogWarning($"Line Code {code.Item1}-{code.Item2}");
            base.Read<TType>(code);
            v3Reader.Read(code, (range, v) =>
            {
                if (range == 0)
                    DxfObject.Start = v;
                else if (range == 1)
                    DxfObject.End = v;
            });
            var fn = code.Item1 switch
            {
                39 => Utilities.ReadThickness,
                210 => Utilities.ReadNormalX,
                220 => Utilities.ReadNormalY,
                230 => Utilities.ReadNormalZ,
                _ => null
            };

            fn?.Invoke( DxfObject, code.Item2);

        }*/

    }
}
