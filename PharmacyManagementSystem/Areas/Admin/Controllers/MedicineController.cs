using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using PharmacyManagementSystem.ViewModels.Medicine;
using PharmacyManagementSystem.Services.Implementations;

namespace PharmacyManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MedicineController : Controller
    {
        private readonly MedicineService _service;
        private readonly CategoryService _categoryService;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public MedicineController(
            MedicineService service,
            CategoryService categoryService,
            IWebHostEnvironment env,
            AppDbContext context)
        {
            _service = service;
            _categoryService = categoryService;
            _env = env;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var medicines = await _service.GetAllAsync(userId);

            return View(medicines);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();

            ViewBag.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MedicineCreateViewModel model, IFormFile? ImageFile)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllAsync();
                ViewBag.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

                return View(model);
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads", "medicines");
                Directory.CreateDirectory(uploadPath);

                var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await ImageFile.CopyToAsync(stream);

                model.ImageUrl = "/uploads/medicines/" + fileName;
            }

            await _service.CreateAsync(model);

            await AddNotificationAsync(
                "Yeni dərman əlavə edildi",
                $"{model.Name} adlı dərman sistemə əlavə olundu.");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var medicine = await _service.GetByIdAsync(id);

            var model = new MedicineEditViewModel
            {
                Id = medicine.Id,
                Name = medicine.Name,
                Description = medicine.Description,
                Composition = medicine.Composition,
                Manufacturer = medicine.Manufacturer,
                Barcode = medicine.Barcode,
                Price = medicine.Price,
                DiscountPrice = medicine.DiscountPrice,
                StockQuantity = medicine.StockQuantity,
                ExpireDate = medicine.ExpireDate,
                RequiresPrescription = medicine.RequiresPrescription,
                ImageUrl = medicine.ImageUrl,
                CategoryId = medicine.CategoryId
            };

            ViewBag.Categories = (await _categoryService.GetAllAsync())
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MedicineEditViewModel model, IFormFile? ImageFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = (await _categoryService.GetAllAsync())
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList();

                return View(model);
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads", "medicines");
                Directory.CreateDirectory(uploadPath);

                var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await ImageFile.CopyToAsync(stream);

                model.ImageUrl = "/uploads/medicines/" + fileName;
            }

            await _service.UpdateAsync(model);

            await AddNotificationAsync(
                "Dərman yeniləndi",
                $"{model.Name} adlı dərmanın məlumatları yeniləndi.");

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var medicine = await _service.GetByIdAsync(id);

            if (medicine == null)
                return NotFound();

            await _service.DeleteAsync(id);

            await AddNotificationAsync(
                "Dərman silindi",
                $"{medicine.Name} adlı dərman silindi.");

            return RedirectToAction(nameof(Index));
        }

        private async Task AddNotificationAsync(string title, string message)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return;

            var notification = new Notification
            {
                Title = title,
                Message = message,
                UserId = userId,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }
    }
}
