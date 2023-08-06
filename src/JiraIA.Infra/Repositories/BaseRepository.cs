using JiraIA.Domain.Interfaces.Repositories;
using JiraIA.Domain.Models;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace JiraIA.Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly IClientSessionHandle _sessionHandle;
        private readonly JiraIAContext _jiraIAContext;
        private readonly string _collection;

        public BaseRepository(JiraIAContext context, IClientSessionHandle sessionHandle)
        {
            _sessionHandle = sessionHandle;
            _jiraIAContext = context;
            _collection = typeof(T).Name;
        }

        protected virtual IMongoCollection<T> Collection => _jiraIAContext.MongoDatabase.GetCollection<T>(_collection);

        private void OpenTransaction()
        {
            if (!_sessionHandle.IsInTransaction)
                _sessionHandle.StartTransaction();
        }

        public async Task DeleteAsync(string id)
        {
            OpenTransaction();
            await Collection.DeleteOneAsync(_sessionHandle, f => f.Id == id);
        }

        public IQueryable<T> GetQueryble()
        {
            var result = Collection.AsQueryable();
            return result;
        }

        public async Task InsertAsync(T obj)
        {
            OpenTransaction();
            await Collection.InsertOneAsync(_sessionHandle, obj);
        }

        public async Task InsertRangeAsync(List<T> obj)
        {
            OpenTransaction();
            await Collection.InsertManyAsync(_sessionHandle, obj);
        }

        public async Task UpdateAsync(T obj)
        {
            OpenTransaction();
            Expression<Func<T, string>> func = f => f.Id;
            var value = (string)obj.GetType().GetProperty(func.Body.ToString().Split(".")[1]).GetValue(obj, null);
            var filter = Builders<T>.Filter.Eq(func, value);

            if (filter != null)
                await Collection.ReplaceOneAsync(_sessionHandle, filter, obj);
        }
    }
}
