using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.ViewModels.Cart;
using PharmacyManagementSystem.Repositories.Interfaces;

namespace PharmacyManagementSystem.Services.Implementations
{
    public class CartService
    {
        private readonly IGenericRepository<Cart> _repository;
        private readonly IGenericRepository<CartItem> _cartItemRepository;
        private readonly ICartRepository _cartRepository;

        public CartService(
            IGenericRepository<Cart> repository,
            ICartRepository cartRepository,
            IGenericRepository<CartItem> cartItemRepository)
        {
            _repository = repository;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }

        public async Task<CartViewModel> GetCartAsync(string userId)
        {
            var carts = await _repository.GetAllAsync();
            var cart = carts.FirstOrDefault(x => x.UserId == userId);

            if (cart == null)
                return new CartViewModel();

            var cartItems = await _cartItemRepository.GetAllWithIncludesAsync(x => x.Medicine);

            var items = cartItems
                .Where(x => x.CartId == cart.Id)
                .Select(x => new CartItemViewModel
                {
                    Id = x.Id,
                    MedicineId = x.MedicineId,
                    MedicineName = x.Medicine?.Name ?? "—",
                    ImageUrl = x.Medicine?.ImageUrl ?? "",
                    Price = x.Medicine?.Price ?? 0,
                    Quantity = x.Quantity
                })
                .ToList();

            return new CartViewModel
            {
                Items = items,
                Total = items.Sum(x => x.Total)
            };
        }

        public async Task AddToCartAsync(string userId, int medicineId, int quantity = 1)
        {
            var carts = await _repository.GetAllAsync();

            var cart = carts.FirstOrDefault(x => x.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId
                };

                await _repository.AddAsync(cart);
                await _repository.SaveAsync();
            }

            var cartItems = await _cartItemRepository.GetAllAsync();

            var existingItem = cartItems
                .FirstOrDefault(x => x.CartId == cart.Id &&
                                     x.MedicineId == medicineId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;

                _cartItemRepository.Update(existingItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    MedicineId = medicineId,
                    Quantity = quantity
                };

                await _cartItemRepository.AddAsync(cartItem);
            }

            await _cartItemRepository.SaveAsync();
        }

        public async Task UpdateItemQuantityAsync(string userId, int medicineId, int newQuantity)
        {
            var carts = await _repository.GetAllAsync();
            var cart = carts.FirstOrDefault(x => x.UserId == userId);

            if (cart != null)
            {
                var cartItems = await _cartItemRepository.GetAllAsync();
                var existingItem = cartItems.FirstOrDefault(x => x.CartId == cart.Id && x.MedicineId == medicineId);

                if (existingItem != null)
                {
                    existingItem.Quantity = newQuantity;
                    _cartItemRepository.Update(existingItem);
                    await _cartItemRepository.SaveAsync();
                }
            }
        }

        public async Task RemoveFromCartAsync(string userId, int medicineId)
        {
            var cart = (await _repository.GetAllAsync())
                .FirstOrDefault(x => x.UserId == userId);

            if (cart == null) return;

            var cartItems = await _cartItemRepository.GetAllAsync();
            var item = cartItems.FirstOrDefault(x => x.CartId == cart.Id && x.MedicineId == medicineId);

            if (item != null)
            {
                _cartItemRepository.Delete(item);
                await _cartItemRepository.SaveAsync();
            }
        }

        public async Task ClearCartAsync(string userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
                return;

            cart.CartItems.Clear();

            await _cartRepository.UpdateAsync(cart);
        }
    }
}
