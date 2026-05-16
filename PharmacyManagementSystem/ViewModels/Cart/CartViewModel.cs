namespace PharmacyManagementSystem.ViewModels.Cart
{
    public class CartViewModel : BaseViewModel
    {
        public decimal Total { get; set; }

        public List<CartItemViewModel> Items { get; set; } = new();
    }
}
