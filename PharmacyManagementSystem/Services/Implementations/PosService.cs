using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.ViewModels.Pos;

namespace PharmacyManagementSystem.Services.Implementations
{
    public class PosService
    {
        private readonly AppDbContext _context;

        public PosService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Medicine>> SearchMedicinesAsync(string term)
        {
            return await _context.Medicines
                .Where(x => x.Name.Contains(term))
                .Take(10)
                .ToListAsync();
        }

        public async Task<int> CreateSaleAsync(List<PosCartItemViewModel> cart, string userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var sale = new Sale
                {
                    SellerId = userId,
                    CreatedAt = DateTime.Now,
                    ReceiptNumber = "PH-" + DateTime.Now.ToString("yyyyMMdd") + "-" + new Random().Next(1000, 9999),
                    TotalPrice = cart.Sum(x => x.Price * x.Quantity)
                };

                _context.Sales.Add(sale);
                await _context.SaveChangesAsync();

                foreach (var item in cart)
                {
                    var medicine = await _context.Medicines
                        .FirstOrDefaultAsync(x => x.Id == item.MedicineId);

                    if (medicine == null)
                        throw new Exception("Medicine tapılmadı");

                    if (medicine.StockQuantity < item.Quantity)
                        throw new Exception($"{medicine.Name} üçün kifayət qədər stok yoxdur");

                    medicine.StockQuantity -= item.Quantity;

                    // SaleItem əlavə et
                    _context.SaleItems.Add(new SaleItem
                    {
                        SaleId = sale.Id,
                        MedicineId = item.MedicineId,
                        Quantity = item.Quantity,
                        Price = item.Price
                    });
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return sale.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Sale> GetReceiptAsync(int id)
        {
            var sale = await _context.Sales
                .Include(x => x.SaleItems)
                    .ThenInclude(x => x.Medicine)
                .FirstOrDefaultAsync(x => x.Id == id);

            return sale;
        }
    }
}
