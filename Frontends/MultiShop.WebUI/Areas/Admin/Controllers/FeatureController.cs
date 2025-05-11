using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Route("Admin/Feature")]
    [Area("Admin")]
    public class FeatureController : Controller
    {
        private IHttpClientFactory _httpClientFactory;

        public FeatureController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Özellikler";
            ViewBag.v3 = "Özellik Listesi";
            ViewBag.v0 = "Özellik İşlemlei";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7070/api/Features");
            if (response.IsSuccessStatusCode)
            {
                var jsonData= await response.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);
                return View(values);
            }



            return View();
        }
        [Route("CreateFeature")]
        public IActionResult CreateFeature()
        {
            return View();

        }
        [Route("CreateFeature")]
        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            var client= _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createFeatureDto);
            StringContent stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json");
            var responseMeeage = await client.PostAsync("https://localhost:7070/api/Features",stringContent);
            if(responseMeeage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Feature",new { Area="Admin"});
            }
            return View();
        }
        [Route("UpdateFeature/{id}")]
        public async Task<IActionResult> UpdateFeature(string id)
        {
            var client= _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7070/api/Features/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var content= await responseMessage.Content.ReadAsStringAsync();
                var value=JsonConvert.DeserializeObject<UpdateFeatureDto>(content);

                return View(value);
            }
            return View();
        }
        [Route("UpdateFeature/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateFeatureDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMeeage = await client.PutAsync("https://localhost:7070/api/Features", stringContent);
            if (responseMeeage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Feature", new { Area = "Admin" });
            }
            return View();
        }
        [Route("DeleteFeature/{id}")]
        public async Task<IActionResult> DeleteFeature(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7070/api/Features?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Feature", new { Area = "Admin" });
            }
            return View();
        }
    }
}
