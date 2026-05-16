namespace PharmacyManagementSystem.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
    }
}
