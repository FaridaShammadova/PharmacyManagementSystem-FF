namespace PharmacyManagementSystem.ViewModels.Wishlist
{
    public class WishlistViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public List<WishlistItemViewModel> Items { get; set; } = new();

    }
}
