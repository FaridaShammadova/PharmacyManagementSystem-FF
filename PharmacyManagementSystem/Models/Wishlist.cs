namespace PharmacyManagementSystem.Models
{
    public class Wishlist : BaseEntity
    {
        public string Name { get; set; } = "My Wishlist";

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
    }
}
