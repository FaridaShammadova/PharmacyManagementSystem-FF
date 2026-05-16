using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PharmacyManagementSystem.Services.Implementations;

namespace PharmacyManagementSystem.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class MedicineController : Controller
    {
        private readonly MedicineService _service;

        public MedicineController(MedicineService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var medicines = await _service.GetAllAsync(userId);

            if (!string.IsNullOrWhiteSpace(search))
            {
                medicines = medicines
                    .Where(x => x.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return View(medicines);
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _service.GetByIdAsync(id));
        }
    }
}
