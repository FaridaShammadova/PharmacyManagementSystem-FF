using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PharmacyManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using PharmacyManagementSystem.ViewModels.Order;
using PharmacyManagementSystem.Services.Implementations;

namespace PharmacyManagementSystem.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly CartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(
            OrderService orderService,
            UserManager<ApplicationUser> userManager,
            CartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var orders = await _orderService.GetByUserIdAsync(user.Id);

            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var orders = await _orderService.GetAllAsync();

            var order = orders.FirstOrDefault(x => x.Id == id && x.UserId == user.Id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = await _cartService.GetCartAsync(userId);

            var model = new OrderCreateViewModel
            {
                OrderItems = cart.Items.Select(x => new OrderItemViewModel
                {
                    MedicineName = x.MedicineName,
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList(),

                TotalPrice = cart.Total
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(OrderCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var cart = await _cartService.GetCartAsync(userId);

                model.OrderItems = cart.Items.Select(x => new OrderItemViewModel
                {
                    MedicineName = x.MedicineName,
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList();

                model.TotalPrice = cart.Total;

                return View(model);
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _orderService.CreateAsync(model, currentUserId);

            return RedirectToAction("Index");
        }
    }
}
