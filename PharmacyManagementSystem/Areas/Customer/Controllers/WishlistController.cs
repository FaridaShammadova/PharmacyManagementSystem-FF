using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PharmacyManagementSystem.Services.Implementations;

namespace PharmacyManagementSystem.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class WishlistController : Controller
    {
        private readonly WishlistService _service;

        public WishlistController(WishlistService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var model = await _service.GetWishlistAsync(userId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int medicineId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            await _service.AddAsync(userId, medicineId);

            return RedirectToAction("Index", "Wishlist", new { area = "Customer" });
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int medicineId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Json(new
                {
                    success = false,
                    message = "User tapılmadı"
                });

            await _service.RemoveAsync(userId, medicineId);

            return Json(new
            {
                success = true
            });
        }
    }
}
