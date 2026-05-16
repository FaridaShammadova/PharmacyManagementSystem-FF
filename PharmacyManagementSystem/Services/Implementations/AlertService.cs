using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Services.Implementations
{
    public class AlertService
    {
        private readonly AppDbContext _context;

        public AlertService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Medicine>> GetCriticalMedicinesAsync()
        {
            var today = DateTime.UtcNow;

            return await _context.Medicines
                .Where(m =>
                    m.StockQuantity <= 5 ||
                    m.ExpireDate <= today.AddMonths(1) ||
                    m.StockQuantity == 0
                )
                .ToListAsync();
        }
    }
}
