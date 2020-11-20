using System.Diagnostics;
using System.IO;
using WeETL.Core;
using WeETL.Schemas;

namespace WeETL
{
    public class TFileDelete<TInputSchema, TOutputSchema> : ETLComponent<TInputSchema, TOutputSchema>
        where TInputSchema : class,IFilenameSchema//, new()
        where TOutputSchema :class, IFilenameSchema, new()
    {
        protected override void InternalOnInputAfterTransform(int index, TOutputSchema row)
        {
            base.InternalOnInputAfterTransform(index, row);
#if DEBUG
            Debug.WriteLine($"Deleting ${row.Filename}");
#endif
            File.Delete(row.Filename);
        }
    
    }
}
