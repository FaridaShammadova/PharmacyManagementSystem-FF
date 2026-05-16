using AutoMapper;
using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.ViewModels.Sale;
using PharmacyManagementSystem.Repositories.Interfaces;

namespace PharmacyManagementSystem.Services.Implementations
{
    public class SaleService
    {
        private readonly IGenericRepository<Sale> _saleRepository;
        private readonly IGenericRepository<Medicine> _medicineRepository;
        private readonly IMapper _mapper;

        public SaleService(IGenericRepository<Sale> saleRepository, IGenericRepository<Medicine> medicineRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _medicineRepository = medicineRepository;
            _mapper = mapper;
        }

        public async Task<Sale> GetByIdAsync(int id)
            => await _saleRepository.GetByIdAsync(id);

        public async Task CreateSaleAsync(SaleCreateViewModel model, string sellerId)
        {
            var sale = new Sale
            {
                SellerId = sellerId,
                ReceiptNumber = Guid.NewGuid().ToString("N")[..8],
                SaleItems = new List<SaleItem>()
            };

            decimal total = 0;

            foreach (var item in model.Items)
            {
                var medicine = await _medicineRepository.GetByIdAsync(item.MedicineId);

                if (medicine == null)
                    continue;

                if (medicine.StockQuantity <= 0)
                    continue;

                var quantityToSell = item.Quantity;

                if (medicine.StockQuantity < quantityToSell)
                    quantityToSell = medicine.StockQuantity;

                var saleItem = new SaleItem
                {
                    MedicineId = medicine.Id,
                    Quantity = quantityToSell,
                    Price = medicine.Price
                };

                total += quantityToSell * medicine.Price;

                medicine.StockQuantity -= quantityToSell;

                _medicineRepository.Update(medicine);

                sale.SaleItems.Add(saleItem);
            }

            sale.TotalPrice = total;

            await _saleRepository.AddAsync(sale);
            await _saleRepository.SaveAsync();

            await _medicineRepository.SaveAsync();
        }
    }
}
