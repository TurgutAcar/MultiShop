using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using MultiShop.WebUI.Services.CatologService.ProductSearchServices;
using MultiShop.WebUI.Services.CatologService.ProductService;
using Newtonsoft.Json;
namespace MultiShop.WebUI.Controllers
{

    [AllowAnonymous]
    public class ProductListController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IProductSearchService _productSearchService;

        public ProductListController(IHttpClientFactory httpClientFactory, IProductSearchService productSearchService)
        {
            _httpClientFactory = httpClientFactory;
            _productSearchService = productSearchService;
        }

        public IActionResult Index(string id)
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürünler";
            ViewBag.directory3 = "Ürün Listesi";
            ViewBag.i = id;
            return View();
        }
        public async Task<IActionResult> LoadMore(string id, double lastPrice, string lastId)
        {
            var result = await _productSearchService.GetPagedProductsByCategoryIdAsync(categoryId: id,lastPrice: lastPrice,lastId: lastId);
            return PartialView("_ProductListItemsPartial", result.Data);
        }
        public IActionResult ProductDetail(string id)
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory3 = "Ürün Listesi";
            ViewBag.directory2 = "Ürün Detayları";

            ViewBag.i = id;

            return View();
        }
        [HttpGet]

        public  PartialViewResult AddComment()
        {
           
            return PartialView();
        }
        [HttpPost]
        // SADECE POST/DELETE/PUT metotlarına uygulanmalıdır!
        [ValidateAntiForgeryToken] // CSRF Token doğrulamasını zorunlu kılar
        public async Task<IActionResult> AddComment(CreateCommentDto createCommentDto)
        {
            createCommentDto.ImageUrl = "test";
            createCommentDto.Rating = 1;
            createCommentDto.CreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            createCommentDto.Status = false;
            var client= _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCommentDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7211/api/Comments", stringContent);
            if(responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Default");
            }
            return View();
        }
    }
}
