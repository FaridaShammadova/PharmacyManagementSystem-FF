namespace PharmacyManagementSystem.Models
{
    public class Sale : BaseEntity
    {
        public decimal TotalPrice { get; set; }
        public string ReceiptNumber { get; set; }

        public string SellerId { get; set; }
        public ApplicationUser Seller { get; set; }

        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }
}
