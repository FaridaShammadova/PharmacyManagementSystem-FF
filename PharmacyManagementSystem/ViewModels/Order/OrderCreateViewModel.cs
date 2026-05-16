using PharmacyManagementSystem.Enums;

namespace PharmacyManagementSystem.ViewModels.Order
{
    public class OrderCreateViewModel
    {
        public string DeliveryAddress { get; set; }
        public string? Notes { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal TotalPrice { get; set; }
        public string? PrescriptionImageUrl { get; set; }
        public int TotalItemsCount { get; set; }

        public string? CardHolderName { get; set; }
        public string? CardNumber { get; set; }
        public string? ExpireDate { get; set; }
        public string? CVV { get; set; }


        public List<OrderItemViewModel> OrderItems { get; set; } = new();
    }
}
