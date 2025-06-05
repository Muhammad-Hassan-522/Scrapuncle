using Microsoft.AspNetCore.Mvc;

namespace Scrapuncle.Controllers
{
    public class AccountController : Controller
    {
        public ViewResult Register()
        {
            return View();
        }

        public ViewResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Account");
        }
    }
}
