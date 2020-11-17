using WeETL.Core;
using WeETL.Databases;

namespace WeETL
{
    public class TOutputDb< TOutputSchema,TKey> : ETLComponent<TOutputSchema, TOutputSchema>
        where TOutputSchema : class, new()
    {
        

        public TOutputDb(IRepository<TOutputSchema,TKey> repository)
        {
           
        }

        public virtual void OpenConnection()
        {
            
        }
    }
}
