namespace PharmacyManagementSystem.ViewModels.PrescriptionRequest
{
    public class PrescriptionRequestViewModel : BaseViewModel
    {
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }

        public string UserId { get; set; }
        public string? ReviewedByName { get; set; }

        public int OrderId { get; set; }
    }
}
