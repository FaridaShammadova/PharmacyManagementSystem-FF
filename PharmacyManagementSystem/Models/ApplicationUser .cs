using Microsoft.AspNetCore.Identity;

namespace PharmacyManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsFirstLogin { get; set; } = false;
        public DateTime? LastLoginDate { get; set; }

        public string? DefaultPassword { get; set; }
        public bool PasswordChanged { get; set; } = false;

        public Cart Cart { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
