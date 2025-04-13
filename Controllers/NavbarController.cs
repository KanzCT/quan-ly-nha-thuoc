using Microsoft.AspNetCore.Mvc;

namespace QuanLyThuoc.Controllers
{
    public class NavbarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PrivacyPolicy()
        {
            return View();
        }

        public IActionResult TermsOfUse()
        {
            return View();
        }

        public IActionResult SalesAndRefunds()
        {
            return View();
        }
    }
}
