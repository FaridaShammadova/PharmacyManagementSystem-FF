using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PharmacyManagementSystem.ViewModels.Pos;
using PharmacyManagementSystem.Services.Implementations;

namespace PharmacyManagementSystem.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = "Seller")]
    public class PosController : Controller
    {
        private readonly PosService _posService;

        public PosController(PosService posService)
        {
            _posService = posService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Search
        [HttpGet]
        public async Task<IActionResult> Search(string term)
        {
            var result = await _posService.SearchMedicinesAsync(term);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Receipt(int id)
        {
            var sale = await _posService.GetReceiptAsync(id);

            if (sale == null)
                return NotFound();

            return View(sale);
        }

        //Checkout
        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] List<PosCartItemViewModel> cart)
        {
            try
            {
                if (cart == null)
                    return Json(new { success = false, message = "Cart null gəldi" });

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var saleId = await _posService.CreateSaleAsync(cart, userId);

                return Json(new { success = true, saleId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.ToString() });
            }
        }
    }
}
