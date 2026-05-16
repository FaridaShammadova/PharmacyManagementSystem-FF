namespace PharmacyManagementSystem.ViewModels.Cart
{
    public class CartItemViewModel : BaseViewModel
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }

        public decimal Total => Price * Quantity;

        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
    }
}
