namespace PharmacyManagementSystem.ViewModels.Sale
{
    public class SaleViewModel : BaseViewModel
    {
        public decimal TotalPrice { get; set; }
        public string ReceiptNumber { get; set; }

        public List<SaleItemViewModel> Items { get; set; } = new();
    }
}
