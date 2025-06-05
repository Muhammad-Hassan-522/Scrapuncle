using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scrapuncle.Models;
using Scrapuncle.Models.Interface;

namespace Scrapuncle.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize(Policy = "DealerOnly")]
    public class DealerController : Controller
    {
        private readonly IPickupRepository pickupRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public DealerController(IPickupRepository pickupRepository, UserManager<ApplicationUser> userManager)
        {
            this.pickupRepository = pickupRepository;
            _userManager = userManager;
        }

        public async Task<ViewResult> DashboardAsync()
        {
            var dealer = await _userManager.GetUserAsync(User);
            var dealerId = dealer.Id;


            // get all pickups assigned to this logged in dealer
            var allPickups = pickupRepository.GetDealerPickups(dealerId);

            // stats
            var totalPickups = allPickups.Count();
            //var pendingPickups = allPickups.Count(p => p.Status.ToUpper() == "SCHEDULED");
            var completedToday = allPickups.Count(p => p.Status.ToUpper() == "COMPLETED" && p.PickupDate.Date == DateTime.Today);

            // recent 3 pickups
            var recentPickups = allPickups
                .OrderByDescending(p => p.PickupDate)
                .Take(3)
                .ToList();

            var model = new DealerDashboard
            {
                TotalPickups = totalPickups,
                //PendingPickups = pendingPickups,
                CompletedToday = completedToday,
                RecentPickups = recentPickups
            };

            return View(model);
        }

        public ViewResult AllPickups()
        {
            var pickups = pickupRepository.GetAllPickups();
            return View(pickups);
        }

        public async Task<IActionResult> UpdatePickupStatusAsync(int id, string status)
        {
            var dealer = await _userManager.GetUserAsync(User);

            var pickup = pickupRepository.UpdatePickupStatus(id, status, dealer.Id);

            if (pickup)
            {
                TempData["Success"] = "Pickup status updated successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to update pickup status.";
            }
            return RedirectToAction("AllPickups");
        }

        public IActionResult CancelPickup(int pickupId)
        {
            var pickup = pickupRepository.CancelPickup(pickupId);
            if (pickup)
            {
                TempData["Success"] = "Pickup cancelled successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to cancel pickup.";
            }
            return RedirectToAction("AllPickups");
        }


        //public async Task<IActionResult> ProfileAsync()
        //{
        //    var dealer = await _userManager.GetUserAsync(User);
        //    if (dealer == null)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    var dealerProfile = new DealerProfile()
        //    {
        //        Id = dealer.Id,
        //        FullName = dealer.FullName,
        //        BusinessName = dealer.BusinessName,
        //        Email = dealer.Email,
        //        PhoneNumber = dealer.PhoneNumber,
        //        Location = dealer.Location,
        //        ExistingProfileImagePath = dealer.ProfileImagePath,
        //        ServiceablePincodes = string.IsNullOrEmpty(dealer.ServiceablePincodes) ? new List<string>() : dealer.ServiceablePincodes.Split(',').ToList()
        //    };
        //    return View(dealerProfile);
        //}

        //public async Task<IActionResult> ProfileAsync()
        //{
        //    var dealer = await _userManager.GetUserAsync(User);
        //    if (dealer == null)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    var dealerProfile = new DealerProfile
        //    {
        //        Id = dealer.Id,
        //        FullName = dealer.FullName,
        //        BusinessName = dealer.BusinessName,
        //        Email = dealer.Email,
        //        PhoneNumber = dealer.PhoneNumber,
        //        Location = dealer.Location,
        //        ExistingProfileImagePath = dealer.ProfileImagePath,
        //        ServiceablePincodes = string.IsNullOrEmpty(dealer.ServiceablePincodes) ? new List<string>() : dealer.ServiceablePincodes.Split(',').ToList()
        //    };

        //    return View(dealerProfile);
        //}

        //[HttpPost]
        //public async Task<ViewResult> ProfileAsync(DealerProfile dealerProfile)
        //{
        //    var dealer = await _userManager.GetUserAsync(User);
        //    if (dealer == null)
        //    {
        //        TempData["Error"] = "Dealer not found.";
        //        return View();
        //    }

        //    if (dealerProfile.ProfileImage != null)
        //    {
        //        // handle profile image upload
        //        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "dealers");
        //        if (!Directory.Exists(uploadsFolder))
        //        {
        //            Directory.CreateDirectory(uploadsFolder);
        //        }
        //        var fileName = $"{dealer.Id}_{Path.GetFileName(dealerProfile.ProfileImage.FileName)}";
        //        var filePath = Path.Combine(uploadsFolder, fileName);
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await dealerProfile.ProfileImage.CopyToAsync(stream);
        //        }
        //        dealer.ProfileImagePath = $"/images/dealers/{fileName}";
        //    }
        //    else
        //    {
        //        dealer.ProfileImagePath = dealerProfile.ExistingProfileImagePath;
        //    }

        //    // update the dealer in the database
        //    var result = await _userManager.UpdateAsync(dealer);
        //    if (result.Succeeded)
        //    {
        //        TempData["Success"] = "Profile updated successfully.";
        //    }
        //    else
        //    {
        //        TempData["Error"] = "Failed to update profile. Please try again later.";
        //    }

        //    // updating basic info
        //    dealer.FullName = dealerProfile.FullName;
        //    dealer.BusinessName = dealerProfile.BusinessName;
        //    dealer.PhoneNumber = dealer.PhoneNumber;
        //    dealer.Location = dealerProfile.Location;
        //    //dealer.Email = dealerProfile.Email;
        //    dealer.ServiceablePincodes = string.Join(',', dealerProfile.ServiceablePincodes);

        //    return View(dealerProfile);
        //}

        //[HttpPost]
        //public async Task<ViewResult> ProfileAsync(DealerProfile dealerProfile)
        //{
        //    var dealer = await _userManager.GetUserAsync(User);
        //    if (dealer == null)
        //    {
        //        TempData["Error"] = "Dealer not found.";
        //        return View();
        //    }

        //    // ✅ Updating basic info FIRST
        //    dealer.FullName = dealerProfile.FullName;
        //    dealer.BusinessName = dealerProfile.BusinessName;
        //    dealer.PhoneNumber = dealerProfile.PhoneNumber;
        //    dealer.Location = dealerProfile.Location;
        //    dealer.ServiceablePincodes = string.Join(',', dealerProfile.ServiceablePincodes);

        //    // ✅ Profile image upload
        //    if (dealerProfile.ProfileImage != null)
        //    {
        //        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "dealers");
        //        if (!Directory.Exists(uploadsFolder))
        //        {
        //            Directory.CreateDirectory(uploadsFolder);
        //        }

        //        var fileName = $"{dealer.Id}_{Path.GetFileName(dealerProfile.ProfileImage.FileName)}";
        //        var filePath = Path.Combine(uploadsFolder, fileName);
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await dealerProfile.ProfileImage.CopyToAsync(stream);
        //        }
        //        dealer.ProfileImagePath = $"/images/dealers/{fileName}";
        //    }
        //    else
        //    {
        //        dealer.ProfileImagePath = dealerProfile.ExistingProfileImagePath;
        //    }

        //    // ✅ Save to database AFTER assigning all fields
        //    var result = await _userManager.UpdateAsync(dealer);
        //    if (result.Succeeded)
        //    {
        //        TempData["Success"] = "Profile updated successfully.";
        //    }
        //    else
        //    {
        //        TempData["Error"] = "Failed to update profile. Please try again later.";
        //    }

        //    return View(dealerProfile);
        //}

        //[HttpPost]
        //public async Task<IActionResult> ProfileAsync(DealerProfile dealerProfile)
        //{
        //    var dealer = await _userManager.GetUserAsync(User);
        //    if (dealer == null)
        //    {
        //        TempData["Error"] = "Dealer not found.";
        //        return View(dealerProfile);
        //    }

        //    // 1. Handle image upload
        //    if (dealerProfile.ProfileImage != null)
        //    {
        //        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "dealers");
        //        if (!Directory.Exists(uploadsFolder))
        //        {
        //            Directory.CreateDirectory(uploadsFolder);
        //        }

        //        var fileName = $"{dealer.Id}_{Path.GetFileName(dealerProfile.ProfileImage.FileName)}";
        //        var filePath = Path.Combine(uploadsFolder, fileName);

        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await dealerProfile.ProfileImage.CopyToAsync(stream);
        //        }

        //        dealer.ProfileImagePath = $"/images/dealers/{fileName}";
        //    }
        //    else
        //    {
        //        dealer.ProfileImagePath = dealerProfile.ExistingProfileImagePath;
        //    }

        //    // 2. Update fields
        //    dealer.FullName = dealerProfile.FullName;
        //    dealer.BusinessName = dealerProfile.BusinessName;
        //    dealer.PhoneNumber = dealerProfile.PhoneNumber;
        //    dealer.Location = dealerProfile.Location;
        //    dealer.ServiceablePincodes = string.Join(',', dealerProfile.ServiceablePincodes);

        //    // 3. Save changes
        //    var result = await _userManager.UpdateAsync(dealer);
        //    if (result.Succeeded)
        //    {
        //        TempData["Success"] = "Profile updated successfully.";
        //    }
        //    else
        //    {
        //        TempData["Error"] = "Failed to update profile.";
        //    }

        //    return View(dealerProfile);
        //}

        //[HttpPost]
        //public async Task<IActionResult> ProfileAsync(DealerProfile model)
        //{
        //    var dealer = await _userManager.GetUserAsync(User);
        //    if (dealer == null)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    // Update basic fields
        //    dealer.FullName = model.FullName;
        //    dealer.BusinessName = model.BusinessName;
        //    dealer.Location = model.Location;
        //    dealer.PhoneNumber = model.PhoneNumber;

        //    // Convert list to comma-separated string
        //    dealer.ServiceablePincodes = model.ServiceablePincodes != null
        //        ? string.Join(",", model.ServiceablePincodes)
        //        : null;

        //    // Handle image upload
        //    if (model.ProfileImage != null && model.ProfileImage.Length > 0)
        //    {
        //        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ProfileImage.FileName);
        //        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

        //        using (var stream = new FileStream(path, FileMode.Create))
        //        {
        //            await model.ProfileImage.CopyToAsync(stream);
        //        }

        //        dealer.ProfileImagePath = "/images/" + fileName;
        //    }

        //    await _userManager.UpdateAsync(dealer);

        //    TempData["Success"] = "Profile updated successfully.";
        //    return RedirectToAction("Profile");
        //}


        public async Task<IActionResult> ProfileAsync()
        {
            var dealer = await _userManager.GetUserAsync(User);
            if (dealer == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var dealerProfile = new DealerProfile
            {
                Id = dealer.Id,
                FullName = dealer.FullName,
                BusinessName = dealer.BusinessName,
                Email = dealer.Email,
                PhoneNumber = dealer.PhoneNumber,
                Location = dealer.Location,
                ExistingProfileImagePath = dealer.ProfileImagePath,
                // Keep ServiceablePincodes as string for the view
                ServiceablePincodes = dealer.ServiceablePincodes ?? string.Empty
            };

            return View(dealerProfile);
        }

        [HttpPost]
        public async Task<IActionResult> ProfileAsync(DealerProfile model)
        {
            var dealer = await _userManager.GetUserAsync(User);
            if (dealer == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Update basic fields
            dealer.FullName = model.FullName;
            dealer.BusinessName = model.BusinessName;
            dealer.Location = model.Location;
            dealer.PhoneNumber = model.PhoneNumber;

            // ServiceablePincodes is already a string from the form
            dealer.ServiceablePincodes = model.ServiceablePincodes;

            // Handle image upload
            if (model.ProfileImage != null && model.ProfileImage.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ProfileImage.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.ProfileImage.CopyToAsync(stream);
                }

                dealer.ProfileImagePath = "/images/" + fileName;
            }

            await _userManager.UpdateAsync(dealer);
            TempData["Success"] = "Profile updated successfully.";
            return RedirectToAction("Profile");
        }

        public ViewResult ScrapRates()
        {
            ViewBag.Layout = "~/Views/Shared/_DealerLayout.cshtml";
            return View("~/Views/Shared/ScrapRates.cshtml");
        }

    }
}
