namespace PharmacyManagementSystem.ViewModels.Notification
{
    public class NotificationViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
    }
}
