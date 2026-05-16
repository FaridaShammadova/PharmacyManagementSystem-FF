using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PharmacyManagementSystem.Services.Implementations;

namespace PharmacyManagementSystem.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = "Seller")]
    public class AlertController : Controller
    {
        private readonly AlertService _alertService;

        public AlertController(AlertService alertService)
        {
            _alertService = alertService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _alertService.GetCriticalMedicinesAsync();
            return View(data);
        }
    }
}
