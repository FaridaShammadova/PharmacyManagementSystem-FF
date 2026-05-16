using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PharmacyManagementSystem.Services.Implementations;

namespace PharmacyManagementSystem.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = "Seller")]
    public class StockController : Controller
    {
        private readonly MedicineService _medicineService;

        public StockController(MedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _medicineService.GetAllAsync(null);
            return View(data);
        }

        //az qalan dərmanlar
        [HttpGet]
        public async Task<IActionResult> LowStock()
        {
            try
            {
                var data = await _medicineService.GetLowStockAsync();
                return View(data);
            }
            catch
            {
                return View("Error");
            }
        }

        //expiry olan dərmanlar
        [HttpGet]
        public async Task<IActionResult> Expired()
        {
            try
            {
                var data = await _medicineService.GetExpiredAsync();
                return View(data);
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> IncreaseStock(int medicineId, int quantity)
        {
            await _medicineService.IncreaseStockAsync(medicineId, quantity);
            return RedirectToAction("Index");
        }
    }
}
