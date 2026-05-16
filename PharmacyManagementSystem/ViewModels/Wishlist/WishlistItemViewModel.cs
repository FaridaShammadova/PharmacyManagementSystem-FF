namespace PharmacyManagementSystem.ViewModels.Wishlist
{
    public class WishlistItemViewModel : BaseViewModel
    {
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }

        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
    }
}
