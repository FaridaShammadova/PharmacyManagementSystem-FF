using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PharmacyManagementSystem.Services.Implementations;

namespace PharmacyManagementSystem.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CartController : Controller
    {
        private readonly CartService _service;

        public CartController(CartService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var model = await _service.GetCartAsync(userId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int medicineId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            await _service.AddToCartAsync(userId, medicineId, 1);

            return RedirectToAction("Index", "Cart", new { area = "Customer" });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int id, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Json(new { success = false, message = "İstifadəçi tapılmadı" });

            if (quantity < 1)
                return Json(new { success = false, message = "Say 1-dən az ola bilməz" });

            await _service.UpdateItemQuantityAsync(userId, id, quantity);

            var cart = await _service.GetCartAsync(userId);
            var item = cart.Items.FirstOrDefault(x => x.MedicineId == id);

            if (item == null)
                return Json(new { success = false, message = "Məhsul tapılmadı" });

            decimal cartSubTotal = cart.Total;
            decimal deliveryFee = cartSubTotal > 30 ? 0 : 3.00m;
            decimal cartFinalTotal = cartSubTotal + deliveryFee;

            return Json(new
            {
                success = true,
                itemTotal = item.Total.ToString("0.00"),
                cartSubTotal = cartSubTotal.ToString("0.00"),
                deliveryFee = deliveryFee,
                cartFinalTotal = cartFinalTotal.ToString("0.00")
            });
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Json(new { success = false });

            await _service.RemoveFromCartAsync(userId, id);

            var cart = await _service.GetCartAsync(userId);
            decimal subTotal = cart.Total;
            decimal delivery = subTotal == 0 ? 0 : (subTotal > 30 ? 0 : 3.00m);

            return Json(new
            {
                success = true,
                isEmpty = !cart.Items.Any(),
                cartSubTotal = subTotal.ToString("0.00"),
                deliveryFee = delivery,
                cartFinalTotal = (subTotal + delivery).ToString("0.00")
            });
        }
    }
}
