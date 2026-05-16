namespace PharmacyManagementSystem.Models
{
    public class Seller : BaseEntity
    {
        public string DefaultPassword { get; set; }
        public bool PasswordChanged { get; set; }
        public bool IsActive { get; set; }
    }
}
