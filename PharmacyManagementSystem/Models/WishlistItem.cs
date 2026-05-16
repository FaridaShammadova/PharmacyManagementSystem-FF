namespace PharmacyManagementSystem.Models
{
    public class WishlistItem : BaseEntity
    {
        public int WishlistId { get; set; }
        public Wishlist Wishlist { get; set; }

        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
    }
}
