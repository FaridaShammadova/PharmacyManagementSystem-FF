using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        void Update(T entity);
        void Delete(T entity);
        Task SaveAsync();

        Task<List<T>> GetAllWithIncludesAsync(
       params System.Linq.Expressions.Expression<Func<T, object>>[] includes);
    }
}
