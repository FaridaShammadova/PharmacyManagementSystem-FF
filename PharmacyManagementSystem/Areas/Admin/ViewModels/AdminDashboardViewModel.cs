using PharmacyManagementSystem.ViewModels.Medicine;

namespace PharmacyManagementSystem.Areas.Admin.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalMedicines { get; set; }
        public int TotalCategories { get; set; }
        public int TotalOrders { get; set; }
        public int TotalUsers { get; set; }
        public int CriticalStockCount { get; set; }

        public List<CategoryAnalyticsViewModel> Categories { get; set; } = new();
        public List<MedicineViewModel> LatestMedicines { get; set; } = new();
        public List<SellerViewModel> Sellers { get; set; } = new();
    }
}
