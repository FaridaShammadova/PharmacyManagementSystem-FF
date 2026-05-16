namespace PharmacyManagementSystem.Models
{
    public class PrescriptionRequest : BaseEntity
    {
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }
        public string ReviewedById { get; set; }
        public ApplicationUser ReviewedBy { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
