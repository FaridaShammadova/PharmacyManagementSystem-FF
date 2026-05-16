namespace PharmacyManagementSystem.Models
{
    public class Cart : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
