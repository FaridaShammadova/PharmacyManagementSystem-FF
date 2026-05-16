using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PharmacyManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using PharmacyManagementSystem.ViewModels.Medicine;
using PharmacyManagementSystem.Areas.Admin.ViewModels;
using PharmacyManagementSystem.Services.Implementations;

namespace PharmacyManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly MedicineService _medicineService;
        private readonly OrderService _orderService;
        private readonly CategoryService _categoryService;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(
            MedicineService medicineService,
            OrderService orderService,
            CategoryService categoryService,
            UserManager<ApplicationUser> userManager)
        {
            _medicineService = medicineService;
            _orderService = orderService;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var medicines = await _medicineService.GetAllForAdminAsync();
            var categories = await _categoryService.GetAllForAdminAsync();
            var orders = await _orderService.GetAllAsync();

            var model = new AdminDashboardViewModel
            {
                TotalMedicines = medicines.Count,
                TotalCategories = categories.Count,
                TotalOrders = orders.Count,
                CriticalStockCount = GetCriticalStock(medicines),

                LatestMedicines = GetLatestMedicines(medicines),
                Categories = GetCategoryStats(categories, medicines),
                Sellers = await GetSellersAsync()
            };

            model.Categories = GetCategoryStats(categories, medicines);

            return View(model);
        }

        //Critical stock
        private int GetCriticalStock(List<Medicine> medicines)
        {
            return medicines.Count(x => x.StockQuantity <= 5);
        }

        //Latest medicines
        private List<MedicineViewModel> GetLatestMedicines(List<Medicine> medicines)
        {
            return medicines
                .OrderByDescending(x => x.Id)
                .Take(5)
                .Select(x => new MedicineViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    StockQuantity = x.StockQuantity,
                    ImageUrl = x.ImageUrl
                })
                .ToList();
        }

        //Category analytics
        private List<CategoryAnalyticsViewModel> GetCategoryStats(List<Category> categories, List<Medicine> medicines)
        {
            var total = medicines.Count;

            return categories.Select(c =>
            {
                var count = medicines.Count(m => m.CategoryId == c.Id);

                return new CategoryAnalyticsViewModel
                {
                    Name = c.Name,
                    MedicineCount = count,
                    Percentage = total == 0
                        ? 0
                        : Math.Round((double)count / total * 100, 1)
                };
            }).ToList();
        }

        //Sellers
        private async Task<List<SellerViewModel>> GetSellersAsync()
        {
            var sellerUsers = await _userManager.GetUsersInRoleAsync("Seller");

            return sellerUsers.Select(x => new SellerViewModel
            {
                FullName = x.FullName,
                Email = x.Email,
                IsOnline = false
            }).ToList();
        }
    }
}
