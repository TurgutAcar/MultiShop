using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.OrderDtos.OrderAddressDtos;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.OrderServices.OrderAddressServices;

namespace MultiShop.WebUI.Controllers
{

    public class OrderController : Controller
    {
        private readonly IOrderAddressService _orderAddressService;
        private readonly IUserService _userService;

        public OrderController(IUserService userService, IOrderAddressService orderAddressService = null)
        {
            _userService = userService;
            _orderAddressService = orderAddressService;
        }
        public IActionResult Index()
        {
            ViewBag.directory1 = "MultiShop";
            ViewBag.directory3 = "Siparişler";
            ViewBag.directory2 = "Sipariş İşlemleri";

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateOrderAddressDto createOrderAddressDto)
        {
            ViewBag.directory1 = "MultiShop";
            ViewBag.directory3 = "Siparişler";
            ViewBag.directory2 = "Sipariş İşlemleri";

            var values = await _userService.GetUserInfo();
            createOrderAddressDto.UserId = values.Data.Id;
            createOrderAddressDto.Description = "aaa";
            await _orderAddressService.CreateOrderAddressesAsync(createOrderAddressDto);
            return RedirectToAction("Index","Payment");
        }
    }
}
