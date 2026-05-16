using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Enums;
using Microsoft.AspNetCore.Authorization;
using PharmacyManagementSystem.Services.Implementations;

namespace PharmacyManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index(OrderStatus? status)
        {
            var orders = await _orderService.GetAllAsync();

            if (status != null)
            {
                orders = orders.Where(x => x.Status == status).ToList();
            }

            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetByIdAsync(id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(
    int id,
    OrderStatus status)
        {
            await _orderService.UpdateStatusAsync(id, status);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var orders = await _orderService.GetAllAsync();
            var order = orders.FirstOrDefault(x => x.Id == id);

            if (order == null)
                return NotFound();

            await _orderService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
