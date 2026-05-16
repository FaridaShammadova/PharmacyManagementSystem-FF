using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PharmacyManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using PharmacyManagementSystem.ViewModels.Seller;

namespace PharmacyManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SellerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public SellerController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var sellers = await _userManager.GetUsersInRoleAsync("Seller");
            return View(sellers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SellerCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingEmail = await _userManager.FindByEmailAsync(model.Email);

            if (existingEmail != null)
            {
                ModelState.AddModelError("Email", "Bu email artıq istifadə olunur!");
                return View(model);
            }

            var existingUsername = await _userManager.FindByNameAsync(model.UserName);

            if (existingUsername != null)
            {
                ModelState.AddModelError("UserName", "Bu istifadəçi adı artıq mövcuddur!");
                return View(model);
            }

            var seller = new ApplicationUser
            {
                FullName = model.FullName,
                UserName = model.UserName,
                Email = model.Email,
                IsActive = true,

                DefaultPassword = model.Password,
                PasswordChanged = false
            };

            var result = await _userManager.CreateAsync(seller, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(seller, "Seller");

                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            user.IsActive = !user.IsActive;

            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}
