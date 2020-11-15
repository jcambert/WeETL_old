using WeETL.Core;
using WeETL.Databases;

namespace WeETL
{
    public class TOutputDb<TInputSchema, TOutputSchema> : ETLComponent<TInputSchema, TOutputSchema>
        where TInputSchema : class, new()
        where TOutputSchema : class, new()
    {
        private readonly IDbClient _client;

        public TOutputDb(IDbClient client)
        {
            this._client = client;
        }

        public virtual void OpenConnection()
        {
            this._client.
        }
    }
}
