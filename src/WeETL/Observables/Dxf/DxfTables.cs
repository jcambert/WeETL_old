using WeETL.Observables.Dxf.Collections;
using WeETL.Utilities;

namespace WeETL.Observables.Dxf
{
    public interface IDxfTables :IDxfObject
    {
        
        ITextStyles TextStyles { get; }
    }
    public class DxfTables : DxfObject, IDxfTables
    {
        public DxfTables(ITextStyles textStyles)
        {
            TextStyles =  Check.NotNull( textStyles,nameof(textStyles));
          
        }

        public ITextStyles TextStyles { get; }



        public override string CodeName => "TABLES";


    }
}
