namespace PharmacyManagementSystem.Models
{
    public class AuditLog : BaseEntity
    {
        public string Action { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }

        public int EntityId { get; set; }
        public string EntityName { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
