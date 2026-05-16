namespace PharmacyManagementSystem.Models
{
    public class Medicine : BaseEntity
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
        public Category Category { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
        public ICollection<StockLog> StockLogs { get; set; } = new List<StockLog>();
    }
}
