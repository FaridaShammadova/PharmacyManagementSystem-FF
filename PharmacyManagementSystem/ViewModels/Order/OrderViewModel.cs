using PharmacyManagementSystem.Enums;

namespace PharmacyManagementSystem.ViewModels.Order
{
    public class OrderViewModel : BaseViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Guid OrderNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public string DeliveryAddress { get; set; }
        public string? Notes { get; set; }
        public int TotalItemsCount { get; set; }
        public bool PrescriptionUploaded { get; set; }
        public DateTime CreatedAt { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? PrescriptionImageUrl { get; set; }

        public string UserId { get; set; }
        public string? UserName { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
    }
}
