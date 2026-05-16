namespace PharmacyManagementSystem.Models
{
    public class SaleItem : BaseEntity
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public int SaleId { get; set; }
        public Sale Sale { get; set; }

        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
    }
}
