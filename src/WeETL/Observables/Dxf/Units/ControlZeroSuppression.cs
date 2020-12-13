using System;

namespace WeETL.Observables.Dxf.Units
{
    [Flags]
    public enum ControlZeroSuppression
    {
        RemoveZeroFeetRemoveInch=0,
        IncludeZeroFeetIncludeInch=1,
        IncludeZeroFeetRemoveInch=2,
        RemoveZeroFeetIncludeInch=3,
        RemoveZeroStartDecimal=4,
        RemoveZeroEndDecimal=8,
        RemoveZeroStartEndDecimal=12

    }
}
