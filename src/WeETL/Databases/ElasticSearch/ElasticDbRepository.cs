using MongoDB.Bson;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeETL.Core;
using WeETL.Properties;

namespace WeETL.Databases.ElasticSearch
{
    public class ElasticDbRepository<T, TKey> : IRepository<T, TKey>
         where T : class,IDocument<TKey>, new()
    {
        private readonly ElasticClient _client;

        public ElasticDbRepository(IDatabaseSettings<ConnectionSettings> dbSettings)
        {
            _client = new ElasticClient(dbSettings.CreateSettings());
            var response = _client.Ping();
            
            if(!response.IsValid)
            {
                throw new DatabaseException($"An error happened while connecting to {nameof(ElasticClient)} in {nameof(ElasticDbRepository<T,TKey>)}", response.OriginalException);
            }
        }
        public ElasticClient Client => _client;
        public IQueryable<T> AsQueryable()
        {
            throw new NotImplementedException();
        }

        public void DeleteById(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public void DeleteMany(Expression<Func<T, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public Task DeleteManyAsync(Expression<Func<T, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public void DeleteOne(Expression<Func<T, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FilterBy(Expression<Func<T, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, TProjected>> projectionExpression)
        {
            throw new NotImplementedException();
        }

        public T FindById(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByIdAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public T FindOne(Expression<Func<T, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public void InsertMany(ICollection<T> documents)
        {
            throw new NotImplementedException();
        }

        public Task InsertManyAsync(ICollection<T> documents)
        {
            throw new NotImplementedException();
        }

        public void InsertOne(T document)
        {
            if(document is IDocument<Guid> )
                ((IDocument<Guid>)document).Id=((IDocument<Guid>)document).Id.GetIfIsNullOrEmpty(()=>Guid.NewGuid());
            if (document is IDocument<ObjectId>)
                ((IDocument<ObjectId>)document).Id = ((IDocument<ObjectId>)document).Id.GetIfIsNullOrEmpty(() => ObjectId.GenerateNewId());
           var response= _client.IndexDocument(document);
            if (!response?.IsValid ?? true) throw new ConnectorException(AbstractionsStrings.DbError(nameof(InsertOne) ), response?.OriginalException);
          
        }

        public async Task InsertOneAsync(T document)
        {
            if (document is IDocument<Guid>)
                ((IDocument<Guid>)document).Id = ((IDocument<Guid>)document).Id.GetIfIsNullOrEmpty(() => Guid.NewGuid());
            if (document is IDocument<ObjectId>)
                ((IDocument<ObjectId>)document).Id = ((IDocument<ObjectId>)document).Id.GetIfIsNullOrEmpty(() => ObjectId.GenerateNewId());
            var response = await _client.IndexDocumentAsync(document);
            if (!response?.IsValid ?? true) throw new ConnectorException(AbstractionsStrings.DbError(nameof(InsertOne)), response?.OriginalException);
        }

        public void ReplaceOne(T document)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceOneAsync(T document)
        {
            throw new NotImplementedException();
        }
    }
}
