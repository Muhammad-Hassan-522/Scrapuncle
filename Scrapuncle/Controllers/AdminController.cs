using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scrapuncle.Data;
using Scrapuncle.Models;
using Scrapuncle.Models.Interface;
using Scrapuncle.Models.Repositories;

namespace Scrapuncle.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext db;
        private readonly IPickupRepository pickupRepository;

        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IPickupRepository pickupRepository)
        {
            _userManager = userManager;
            this.db = db;
            this.pickupRepository = pickupRepository;
        }

        public IActionResult AdminDashboard()
        {
            var users = db.Users.Where(u=> u.Role == "User").ToList();
            var dealers = db.Users.Where(u => u.Role == "Dealer").ToList();
            var pickups = db.Pickups.ToList();
            var pendingPickups = pickups
                                    .Where(p => p.Status == "SCHEDULED")
                                    .OrderByDescending(p => p.CreatedAt)
                                    .Take(10)
                                    .ToList();

            var model = new AdminDashboard
            {
                TotalUsers = users.Count,
                TotalDealers = dealers.Count,
                TotalPickups = pickups.Count,
                PendingPickups = pendingPickups
            };

            return View(model);
        }

        public ViewResult ViewUsers()
        {
            var users = _userManager.Users
                 .Where(u => u.Role.ToLower() == "user")
                 .ToList();
            return View(users);
        }

        public ViewResult ViewDealers()
        {
            var dealers = _userManager.Users
                 .Where(u => u.Role.ToLower() == "dealer")
                 .ToList();
            return View(dealers);
        }
        public ViewResult AdminPickups()
        {
            var pickups = db.Pickups
                            .Include(p => p.User)
                            .Include(p => p.Dealer)
                            .ToList();

            return View(pickups);
        }

        [HttpPost]
        public IActionResult CancelPickup(int id)
        {
            var pickup = pickupRepository.CancelPickup(id);
            return RedirectToAction("AdminPickups");
        }

        public ViewResult ManageItems()
        {
            return View();
        }
    }
}
