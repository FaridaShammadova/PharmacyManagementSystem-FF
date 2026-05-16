namespace PharmacyManagementSystem.Models
{
    public class StockLog : BaseEntity
    {
        public int OldQuantity { get; set; }
        public int NewQuantity { get; set; }
        public string ChangedByUserId { get; set; }
        public ApplicationUser ChangedByUser { get; set; }
        public string Note { get; set; }

        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
    }
}
