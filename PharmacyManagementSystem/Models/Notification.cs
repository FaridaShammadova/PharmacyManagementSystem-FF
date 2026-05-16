namespace PharmacyManagementSystem.Models
{
    public class Notification : BaseEntity
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
