namespace PharmacyManagementSystem.ViewModels.Order
{
    public class OrderItemViewModel
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal Total => Price * Quantity;

        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
    }
}
