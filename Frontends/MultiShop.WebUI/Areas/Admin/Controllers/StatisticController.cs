using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.CommentStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.DiscountStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.MessageStatisticService;
using MultiShop.WebUI.Services.StatisticServices.UserStatisticServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatisticController : Controller
    {
        private readonly ICatalogStatisticService _catalogStatisticService;
        private readonly IUserStatisticService _userStatisticService;
        private readonly ICommentStatisticService _commentStatisticService;
        private readonly IDiscountStatisticService _discountStatisticService;
        private readonly IMessageStatisticService _messageStatisticService;

        public StatisticController(IMessageStatisticService messageStatisticServic,ICatalogStatisticService catalogStatisticService, IUserStatisticService userStatisticService, ICommentStatisticService commentStatisticService, IDiscountStatisticService discountStatisticService)
        {
            _messageStatisticService = messageStatisticServic;
            _catalogStatisticService = catalogStatisticService;
            _userStatisticService = userStatisticService;
            _commentStatisticService = commentStatisticService;
            _discountStatisticService = discountStatisticService;
        }

        public async Task<IActionResult> Index()
        {
            var getBrandCount = await _catalogStatisticService.GetBrandCount();
            var getProductCount = await _catalogStatisticService.GetProductCount();
            var getCategoryCount = await _catalogStatisticService.GetCategoryCount();
            var getMaxPriceProductName = await _catalogStatisticService.GetMaxPriceProductName();
            var getMinPriceProductName = await _catalogStatisticService.GetMinPriceProductName();
            var getProductAvgPrice = await _catalogStatisticService.GetProductAvgPrice();
            var getUserCount = await _userStatisticService.GetUserCount();
            var getPassiveCommentCount = await _commentStatisticService.GetPassiveCommentCount();
            var getActiveCommentCount = await _commentStatisticService.GetActiveCommentCount();
            var getTotalCommentCount = await _commentStatisticService.GetTotalCommentCount();
            var getDiscountCouponCount = await _discountStatisticService.GetDiscountCouponCount();
            var getTotalMessageCount = await _messageStatisticService.GetTotalMessageCount();


            ViewBag.getBrandCount = getBrandCount;
            ViewBag.getProductCount = getProductCount;
            ViewBag.getCategoryCount = getCategoryCount;
            ViewBag.getMaxPriceProductName = getMaxPriceProductName;
            ViewBag.getMinPriceProductName = getMinPriceProductName;
            ViewBag.getProductAvgPrice = getProductAvgPrice;
            ViewBag.getUserCount = getUserCount;
            ViewBag.getPassiveCommentCount = getPassiveCommentCount;
            ViewBag.getActiveCommentCount = getActiveCommentCount;
            ViewBag.getTotalCommentCount = getTotalCommentCount;
            ViewBag.getDiscountCouponCount = getDiscountCouponCount;
            ViewBag.getTotalMessageCount = getTotalMessageCount;
            return View();
        }
    }
}
