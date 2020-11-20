using WeETL.Core;
using WeETL.Databases;

namespace WeETL
{
    public class TOutputDb< TOutputSchema,TKey> : ETLComponent<TOutputSchema, TOutputSchema>
        where TOutputSchema : class, new()
    {
        private readonly IRepository<TOutputSchema, TKey> _repository;

        public TOutputDb(IRepository<TOutputSchema,TKey> repository)
        {
            this._repository = repository;
        }

        protected override void InternalOnInputAfterTransform(int index, TOutputSchema row)
        {
            base.InternalOnInputAfterTransform(index, row);
            _repository.InsertOne(row);
        }
    }
}
