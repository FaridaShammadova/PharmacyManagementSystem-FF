using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserIdAsync(string userId);
        Task UpdateAsync(Cart cart);
        Task DeleteAsync(Cart cart);
    }
}
