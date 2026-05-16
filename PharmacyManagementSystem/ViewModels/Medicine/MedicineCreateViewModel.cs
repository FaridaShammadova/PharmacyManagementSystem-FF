namespace PharmacyManagementSystem.ViewModels.Medicine
{
    public class MedicineCreateViewModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Composition { get; set; }
        public string Manufacturer { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int StockQuantity { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool RequiresPrescription { get; set; }
        public string? ImageUrl { get; set; }

        public int CategoryId { get; set; }
    }
}
