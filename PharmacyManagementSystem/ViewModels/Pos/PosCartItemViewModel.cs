namespace PharmacyManagementSystem.ViewModels.Pos
{
    public class PosCartItemViewModel
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int MedicineId { get; set; }
        public string Name { get; set; }
    }
}
