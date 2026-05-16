using PharmacyManagementSystem.Enums;

namespace PharmacyManagementSystem.Models
{
    public class Order : BaseEntity
    {
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public string DeliveryAddress { get; set; }
        public string? Notes { get; set; }
        public bool PrescriptionUploaded { get; set; }
        public string? PrescriptionImageUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int TotalItemsCount { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
