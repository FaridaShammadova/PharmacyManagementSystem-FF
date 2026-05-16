namespace PharmacyManagementSystem.Models
{
    public class CartItem : BaseEntity
    {
        public int Quantity { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }

        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
    }
}
