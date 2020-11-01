using System;
using System.Collections.Generic;
using System.Text;

namespace WeETL
{
    public class TMap<TInputSchema, TOutputSchema, TLookupSchema> :ETLComponent<TInputSchema,TOutputSchema>
        where TInputSchema:class,new()
        where TOutputSchema: class, new()
        where TLookupSchema : class, new()
    {
    }
}
