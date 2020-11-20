using WeETL.Core;

namespace WeETL
{
    public class TConvertType<TInputSchema, TOutputSchema> : ETLComponent<TInputSchema, TOutputSchema>
        where TInputSchema : class
        where TOutputSchema : class, new()
    
    {
    }
}
