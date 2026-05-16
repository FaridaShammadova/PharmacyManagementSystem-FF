namespace PharmacyManagementSystem.ViewModels.Sale
{
    public class SaleItemViewModel
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public int MedicineId { get; set; }
        public string MedicineName { get; set; }

        public decimal Total => Price * Quantity;
    }
}
