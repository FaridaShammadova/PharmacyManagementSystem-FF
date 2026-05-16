namespace PharmacyManagementSystem.ViewModels.Review
{
    public class ReviewViewModel : BaseViewModel
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string? UserName { get; set; }

        public int MedicineId { get; set; }
    }
}
