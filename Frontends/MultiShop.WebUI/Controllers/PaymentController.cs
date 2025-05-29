using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.directory1 = "MULTISHOP";
            ViewBag.directory3 = "ÖDEME EKRANI";
            ViewBag.directory2 = "KARTLA ÖDEME";
            return View();
        }
    }
}
