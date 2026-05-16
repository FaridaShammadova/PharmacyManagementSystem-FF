using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.ViewModels.Wishlist;
using PharmacyManagementSystem.Repositories.Interfaces;

namespace PharmacyManagementSystem.Services.Implementations
{
    public class WishlistService
    {
        private readonly IGenericRepository<Wishlist> _wishlistRepository;
        private readonly IGenericRepository<WishlistItem> _wishlistItemRepository;
        public WishlistService(IGenericRepository<Wishlist> wishlistRepository, IGenericRepository<WishlistItem> wishlistItemRepository)
        {
            _wishlistRepository = wishlistRepository;
            _wishlistItemRepository = wishlistItemRepository;
        }

        public async Task AddAsync(string userId, int medicineId)
        {
            var wishlist = (await _wishlistRepository.GetAllAsync())
                .FirstOrDefault(x => x.UserId == userId);

            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    UserId = userId,
                    Name = "My Wishlist"
                };

                await _wishlistRepository.AddAsync(wishlist);
                await _wishlistRepository.SaveAsync();
            }

            var items = await _wishlistItemRepository
                .GetAllAsync();

            var exists = items.Any(x =>
                x.WishlistId == wishlist.Id &&
                x.MedicineId == medicineId);

            if (exists)
                return;

            var item = new WishlistItem
            {
                WishlistId = wishlist.Id,
                MedicineId = medicineId
            };

            await _wishlistItemRepository.AddAsync(item);
            await _wishlistItemRepository.SaveAsync();
        }

        public async Task<List<WishlistItemViewModel>> GetWishlistAsync(string userId)
        {
            var wishlist = (await _wishlistRepository.GetAllAsync())
                .FirstOrDefault(x => x.UserId == userId);

            if (wishlist == null)
                return new List<WishlistItemViewModel>();

            var items = (await _wishlistItemRepository
                .GetAllWithIncludesAsync(x => x.Medicine))
                .Where(x => x.WishlistId == wishlist.Id)
                .Select(x => new WishlistItemViewModel
                {
                    Id = x.Id,
                    MedicineId = x.MedicineId,
                    MedicineName = x.Medicine.Name,
                    Price = x.Medicine.Price,
                    ImageUrl = x.Medicine.ImageUrl
                })
                .ToList();

            return items;
        }

        public async Task RemoveAsync(string userId, int medicineId)
        {
            var wishlist = (await _wishlistRepository.GetAllAsync())
                .FirstOrDefault(x => x.UserId == userId);

            if (wishlist == null)
                return;

            var item = (await _wishlistItemRepository.GetAllAsync())
                .FirstOrDefault(x =>
                    x.WishlistId == wishlist.Id &&
                    x.MedicineId == medicineId);

            if (item == null)
                return;

            _wishlistItemRepository.Delete(item);

            await _wishlistItemRepository.SaveAsync();
        }
    }
}
