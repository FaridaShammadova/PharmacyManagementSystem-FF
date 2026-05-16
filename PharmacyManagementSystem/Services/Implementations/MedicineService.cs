using AutoMapper;
using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.ViewModels.Medicine;
using PharmacyManagementSystem.Repositories.Interfaces;
using PharmacyManagementSystem.Repositories.Implementations;

namespace PharmacyManagementSystem.Services.Implementations
{
    public class MedicineService
    {
        private readonly IGenericRepository<Medicine> _medicineRepository;
        private readonly IGenericRepository<Wishlist> _wishlistRepository;
        private readonly IGenericRepository<WishlistItem> _wishlistItemRepository;

        private readonly MedicineRepository _repository;

        private readonly IMapper _mapper;

        public MedicineService(
            IGenericRepository<Medicine> medicineRepository,
            IGenericRepository<Wishlist> wishlistRepository,
            IGenericRepository<WishlistItem> wishlistItemRepository,
            IMapper mapper,
            MedicineRepository repository)
        {
            _medicineRepository = medicineRepository;
            _wishlistRepository = wishlistRepository;
            _wishlistItemRepository = wishlistItemRepository;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<MedicineViewModel>> GetAllAsync(string userId)
        {
            var medicines = await _repository.GetAllAsync();

            var wishlist = (await _wishlistRepository.GetAllAsync())
                .FirstOrDefault(x => x.UserId == userId);

            List<int> wishlistMedicineIds = new();

            if (wishlist != null)
            {
                wishlistMedicineIds = (await _wishlistItemRepository.GetAllAsync())
                    .Where(x => x.WishlistId == wishlist.Id)
                    .Select(x => x.MedicineId)
                    .ToList();
            }

            var result = medicines.Select(medicine => new MedicineViewModel
            {
                Id = medicine.Id,
                Name = medicine.Name,
                Description = medicine.Description,
                Composition = medicine.Composition,
                Manufacturer = medicine.Manufacturer,
                Barcode = medicine.Barcode,

                Price = medicine.Price,
                DiscountPrice = medicine.DiscountPrice,

                StockQuantity = medicine.StockQuantity,
                ExpireDate = medicine.ExpireDate,

                RequiresPrescription = medicine.RequiresPrescription,

                ImageUrl = medicine.ImageUrl,

                CategoryId = medicine.CategoryId,

                CategoryName = medicine.Category != null
                    ? medicine.Category.Name
                    : "Kateqoriyasız",

                IsInWishlist = wishlistMedicineIds.Contains(medicine.Id)

            }).ToList();

            return result;
        }

        public async Task<MedicineViewModel?> GetByIdAsync(int id)
        {
            var data = await _repository.GetByIdAsync(id);

            if (data == null)
                return null;

            return _mapper.Map<MedicineViewModel>(data);
        }

        public async Task CreateAsync(MedicineCreateViewModel model)
        {
            var entity = _mapper.Map<Medicine>(model);

            await _medicineRepository.AddAsync(entity);
            await _medicineRepository.SaveAsync();
        }

        public async Task UpdateAsync(MedicineEditViewModel model)
        {
            var entity = await _repository.GetByIdAsync(model.Id);

            if (entity == null)
                return;

            _mapper.Map(model, entity);

            _medicineRepository.Update(entity);

            await _medicineRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return;

            _medicineRepository.Delete(entity);

            await _medicineRepository.SaveAsync();
        }

        public async Task<List<MedicineViewModel>> GetLowStockAsync()
        {
            var data = await _repository.GetAllAsync();

            var filtered = data
                .Where(x => x.StockQuantity < 5)
                .ToList();

            return _mapper.Map<List<MedicineViewModel>>(filtered);
        }

        public async Task<List<MedicineViewModel>> GetExpiredAsync()
        {
            var data = await _repository.GetAllAsync();

            var filtered = data
                .Where(x => x.ExpireDate <= DateTime.Now)
                .ToList();

            return _mapper.Map<List<MedicineViewModel>>(filtered);
        }

        public async Task DecreaseStockAsync(int medicineId, int quantity)
        {
            var medicine = await _repository.GetByIdAsync(medicineId);

            if (medicine == null)
                return;

            if (medicine.StockQuantity < quantity)
                medicine.StockQuantity = 0;
            else
                medicine.StockQuantity -= quantity;

            _medicineRepository.Update(medicine);

            await _medicineRepository.SaveAsync();
        }

        public async Task IncreaseStockAsync(int medicineId, int quantity)
        {
            var medicine = await _repository.GetByIdAsync(medicineId);

            if (medicine == null)
                return;

            medicine.StockQuantity += quantity;

            _medicineRepository.Update(medicine);

            await _medicineRepository.SaveAsync();
        }

        public async Task<List<Medicine>> GetAllForAdminAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
