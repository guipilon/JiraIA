using JiraIA.Domain.Models;

namespace JiraIA.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task InsertAsync(T obj);

        Task InsertRangeAsync(List<T> obj);

        Task UpdateAsync(T obj);

        Task DeleteAsync(string id);

        IQueryable<T> GetQueryble();
    }
}
