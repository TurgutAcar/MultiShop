using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    [Route("Admin/OfferDiscount")]
    public class OfferDiscountController : Controller
    {
        private IHttpClientFactory _httpClientFactory;

        public OfferDiscountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client =_httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7070/api/OfferDiscounts");
            if (response.IsSuccessStatusCode) { 
              var content=await response.Content.ReadAsStringAsync();
               var values=JsonConvert.DeserializeObject<List<ResultOfferDiscountDto>>(content);
                return View(values);
            }
            return View();
        }
        [Route("CreateOfferDiscount")]
        public IActionResult CreateOfferDiscount()
        {
            return View();
        }
        [HttpPost]
        [Route("CreateOfferDiscount")]
        public async Task<IActionResult> CreateOfferDiscount(CreateOfferDiscountDto createOfferDiscountDto)
        {
            var client=_httpClientFactory.CreateClient();
            var jsonData=JsonConvert.SerializeObject(createOfferDiscountDto);
            StringContent stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json");
            var response = await client.PostAsync("https://localhost:7070/api/OfferDiscounts",stringContent);
            if (response.IsSuccessStatusCode) {
                return RedirectToAction("Index", "OfferDiscount", new { Area = "Admin" });
            }
            return View();
        }
        [Route("UpdateOfferDiscount/{id}")]
        public async Task<IActionResult> UpdateOfferDiscount(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7070/api/OfferDiscounts/{id}");
            if (response.IsSuccessStatusCode) {
             var content=await response.Content.ReadAsStringAsync();
              var value=JsonConvert.DeserializeObject<UpdateOfferDiscountDto>(content);
                return View(value);
            }
            return View();
        }
        [Route("UpdateOfferDiscount/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateOfferDiscount(UpdateOfferDiscountDto updateOfferDiscountDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateOfferDiscountDto);
            StringContent stringContent= new StringContent(jsonData,Encoding.UTF8,"application/json");
            var response = await client.PutAsync($"https://localhost:7070/api/OfferDiscounts", stringContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "OfferDiscount", new { Area = "Admin" });

            }
            return View();
        }
        [Route("DeleteOfferDiscount/{id}")]
        public async Task<IActionResult> DeleteOfferDiscount(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7070/api/OfferDiscounts?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "OfferDiscount", new { Area = "Admin" });

            }
            return View();
        }
    }
}
