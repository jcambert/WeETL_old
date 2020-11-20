using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WeETL.Databases.MongoDb
{
    public class MongoDbRepository<T,TKey> : IRepository<T, TKey>
        where T : class,IDocument<TKey>, new()
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly MongoClient _client;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IMongoCollection<T> _collection;

        public MongoDbRepository(IDatabaseSettings<MongoClientSettings> dbSettings)
        {
            _client = new MongoClient(dbSettings.CreateSettings());

            var database = _client.GetDatabase(dbSettings.DatabaseName);
            _collection = database.GetCollection<T>(GetCollectionName(typeof(T)));
        }
        
        public MongoClient Client => _client;
        
        public IMongoCollection<T> Collection=>_collection;
        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName ?? documentType.Name;
        }

        public virtual IQueryable<T> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public virtual IEnumerable<T> FilterBy(Expression<Func<T, bool>> filterExpression)
        => _collection.Find(filterExpression).ToEnumerable();


        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<T, bool>> filterExpression,
            Expression<Func<T, TProjected>> projectionExpression)
        => _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();


        public virtual T FindOne(Expression<Func<T, bool>> filterExpression)
        => _collection.Find(filterExpression).FirstOrDefault();


        public virtual Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression)
        => Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());


        public virtual T FindById(TKey id)
        {
            //var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
            return _collection.Find(filter).SingleOrDefault();
        }

        public virtual Task<T> FindByIdAsync(TKey id)
        {
            return Task.Run(() =>
            {
                //var objectId = new ObjectId(id);
                var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }


        public virtual void InsertOne(T document)
        {
            _collection.InsertOne(document);
        }

        public virtual Task InsertOneAsync(T document)
        {
            return Task.Run(() => _collection.InsertOneAsync(document));
        }

        public void InsertMany(ICollection<T> documents)
        {
            _collection.InsertMany(documents);
        }


        public virtual async Task InsertManyAsync(ICollection<T> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        public void ReplaceOne(T document)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, document.Id);
            _collection.FindOneAndReplace(filter, document);
        }

        public virtual async Task ReplaceOneAsync(T document)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public void DeleteOne(Expression<Func<T, bool>> filterExpression)
        {
            _collection.FindOneAndDelete(filterExpression);
        }

        public Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
        }

        public void DeleteById(TKey id)
        {
            // var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
            _collection.FindOneAndDelete(filter);
        }

        public Task DeleteByIdAsync(TKey id)
        {
            return Task.Run(() =>
            {
                //var objectId = new ObjectId(id);
                var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
                _collection.FindOneAndDeleteAsync(filter);
            });
        }

        public void DeleteMany(Expression<Func<T, bool>> filterExpression)
        {
            _collection.DeleteMany(filterExpression);
        }

        public Task DeleteManyAsync(Expression<Func<T, bool>> filterExpression)
        {
            return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
        }
    }
}
