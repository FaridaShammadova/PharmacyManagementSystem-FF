using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Services.Interfaces
{
    public interface IGenericService<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
