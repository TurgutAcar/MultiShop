using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    [Route("Admin/Product")]
    public class ProductController : Controller
    {
      
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.v0 = "Ürün İşlemleri";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Listesi";

           var client=_httpClientFactory.CreateClient();
           var responseMessage =await client.GetAsync("https://localhost:7070/api/products");
            if(responseMessage.IsSuccessStatusCode)
            {
                var jsonData=await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }

            return View();

        }
        [HttpGet]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.v0 = "Ürün İşlemleri";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Listesi";
            var client =_httpClientFactory.CreateClient();
            var responseMessage =await client.GetAsync("https://localhost:7070/api/Categories");
            var jsonData=await responseMessage.Content.ReadAsStringAsync();
            var values= JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
            List<SelectListItem> categoryValues = (from x in values
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId
                                                   }).ToList();
            ViewBag.CategoryValues = categoryValues;
            return View();
        }
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var client =_httpClientFactory.CreateClient();
            var value=JsonConvert.SerializeObject(createProductDto);
            StringContent content = new StringContent(value,Encoding.UTF8, "application/json");
            var responseMessage =await client.PostAsync("https://localhost:7070/api/Products", content);
            if (responseMessage.IsSuccessStatusCode) 
            {
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }
            return View();
        }
        [Route("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var client =_httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7070/api/Products?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }
            return View();
        }


        [HttpGet]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            ViewBag.v0 = "Ürün İşlemleri";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Listesi";

            var client1 = _httpClientFactory.CreateClient();
            var responseMessage1 = await client1.GetAsync("https://localhost:7070/api/Categories");
            var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
            var values1 = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData1);
            List<SelectListItem> categoryValues = (from x in values1
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId
                                                   }).ToList();
            ViewBag.CategoryValues = categoryValues;




            var client =_httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7070/api/Products/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData=await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpPost]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            ViewBag.v0 = "Ürün İşlemleri";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Listesi";
            var client =_httpClientFactory.CreateClient();
            var value= JsonConvert.SerializeObject(updateProductDto);
            StringContent content = new StringContent(value, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7070/api/Products", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Product", new { area = "Admin" });

            }
            return View();
        }
        [Route("ProductListWithCategory")]
        public async Task<IActionResult> ProductListWithCategory()
        {
            ViewBag.v0 = "Ürün İşlemleri";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Listesi";

            var client =_httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7070/api/Products/ProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData =await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductsWithCategoryDto>>(jsonData);
                return View(values);
            }
            return View();
        }


    }
}
