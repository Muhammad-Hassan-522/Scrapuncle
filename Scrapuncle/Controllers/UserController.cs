using System.Security.Claims;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Scrapuncle.Models;
using Scrapuncle.Models.Interface;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Scrapuncle.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize(Policy="UserOnly")]
    public class UserController : Controller
    {
        private readonly IPickupRepository pickupRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IPickupRepository pickupRepository, UserManager<ApplicationUser> userManager)
        {
            this.pickupRepository = pickupRepository;
            _userManager = userManager;
        }

        //[Microsoft.AspNetCore.Authorization.Authorize(Policy = "LahoreCityAccess")]
        public ViewResult UserDashboard()
        {
            return View();
        }

        public ViewResult ScrapRates()
        {
            ViewBag.Layout = "~/Views/Shared/_Layout.cshtml";
            return View("~/Views/Shared/ScrapRates.cshtml");
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "BusinessHoursOnly")]
        public ViewResult SchedulePickup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SchedulePickup(Pickup pickup)
        {
            var user = await _userManager.GetUserAsync(User);
            pickup.UserId = user.Id;

            pickup.Landmark = pickup.Landmark ?? "";
            pickup.AddressType = pickup.AddressType ?? "";
            pickup.Remarks = pickup.Remarks ?? "";
            pickup.Status = "SCHEDULED";
            pickupRepository.AddPickup(pickup);
            return RedirectToAction("PickupComplete");
        }

        public ViewResult PickupComplete()
        {
            return View();
        }

        public async Task<IActionResult> CheckPickupsAsync(string status = "SCHEDULED", int page = 1, int itemsPerPage = 5)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var allPickups = pickupRepository.GetUserPickups(userId, status);
            var totalItems = allPickups.Count();
            var pickups = allPickups
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();

            ViewBag.CurrentStatus = status;
            ViewBag.CurrentPage = page;
            ViewBag.ItemsPerPage = itemsPerPage;
            ViewBag.TotalItems = totalItems;

            return View(pickups);
        }

        public IActionResult CancelPickup(int pickupId)
        {
            var pickup = pickupRepository.GetPickupById(pickupId);
            if (pickup == null || pickup.Status != "SCHEDULED")
            {
                TempData["Error"] = "Pickup not found or cannot be cancelled.";
                return RedirectToAction("CheckPickups", new { status = "SCHEDULED" });
            }

            pickup.Status = "CANCELLED";
            pickupRepository.UpdatePickupStatus(pickup.Id, "CANCELLED", null);

            TempData["Success"] = "Pickup cancelled successfully.";
            return RedirectToAction("CheckPickups", new { status = "SCHEDULED" });
        }
    }
}
