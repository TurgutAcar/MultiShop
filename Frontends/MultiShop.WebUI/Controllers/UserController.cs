using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CargoServices.CargoCustomerServices;
using MultiShop.WebUI.Services.Interface;

namespace MultiShop.WebUI.Controllers
{

    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var result=await _userService.GetUserInfo();
            if (!result.IsSuccessful && result.StatusCode == 401)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(result.Data);
        }
       
    }
}
