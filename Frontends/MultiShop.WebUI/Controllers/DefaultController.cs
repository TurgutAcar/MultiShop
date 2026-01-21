using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    [Authorize]
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.directory1 = "MultiShop";
            ViewBag.directory2 = "Ana Sayfa";
            ViewBag.directory3 = "Ürün Listesi";

            return View();
        }
    }
}
