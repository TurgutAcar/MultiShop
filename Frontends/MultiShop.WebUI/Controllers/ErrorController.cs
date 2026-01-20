using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Critical()
        {
            return View();
        }
    }

}
