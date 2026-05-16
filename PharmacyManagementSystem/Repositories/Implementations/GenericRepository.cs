using System.Linq.Expressions;
using PharmacyManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.Repositories.Interfaces;

namespace PharmacyManagementSystem.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<T?> GetByIdAsync(int id)
           => await _dbSet.FindAsync(id);

        public async Task<List<T>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<CartItem>> GetCartItemsWithMedicineAsync()
        {
            return await _context.CartItems
                .Include(x => x.Medicine)
                .ToListAsync();
        }

        public async Task<List<T>> GetAllWithIncludesAsync(
    params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
                query = query.Include(include);

            return await query.ToListAsync();
        }
    }
}
