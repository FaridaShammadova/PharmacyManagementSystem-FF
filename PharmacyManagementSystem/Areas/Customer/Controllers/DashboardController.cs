using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PharmacyManagementSystem.Services.Implementations;

namespace PharmacyManagementSystem.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class DashboardController : Controller
    {
        private readonly MedicineService _service;

        public DashboardController(MedicineService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var medicines = await _service.GetAllAsync(userId);

            return View(medicines);
        }
    }
}
