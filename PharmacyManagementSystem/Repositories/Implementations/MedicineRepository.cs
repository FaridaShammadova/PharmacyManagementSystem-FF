using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Repositories.Implementations
{
    public class MedicineRepository
    {
        private readonly AppDbContext _context;

        public MedicineRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Medicine>> GetAllAsync()
        {
            return await _context.Medicines
                .Include(x => x.Category)
                .ToListAsync();
        }

        public async Task<Medicine?> GetByIdAsync(int id)
        {
            return await _context.Medicines
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
