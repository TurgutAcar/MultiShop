using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    [Route("Admin/About")]
    public class AboutController : Controller
    {
        private IHttpClientFactory _httpClientFactory;

        public AboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Hakkımda";
            ViewBag.v3 = "Hakkımda Listesi";
            ViewBag.v0 = "Hakkımda İşlemleri";
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7070/api/Abouts");
            if(responseMessage.IsSuccessStatusCode)
            {
                var content= await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(content);
                return View(values);
            }
            return View();
        }
        [Route("CreateAbout")]
        public IActionResult CreateAbout()
        {
          
            return View();
        }
        [HttpPost]
        [Route("CreateAbout")]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Hakkımda";
            ViewBag.v3 = "Hakkımda Listesi";
            ViewBag.v0 = "Hakkımda İşlemleri";
            var client = _httpClientFactory.CreateClient();
            var data= JsonConvert.SerializeObject(createAboutDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7070/api/Abouts",content);
            if(responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "About", new { Area = "Admin" });
            }
            return View();
        }
        [Route("UpdateAbout/{id}")]
        public async Task<IActionResult> UpdateAbout(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7070/api/Abouts/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateAboutDto>(content);
                return View(value);
            }
            return View();
        }
        [HttpPost]
        [Route("UpdateAbout/{id}")]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Hakkımda";
            ViewBag.v3 = "Hakkımda Listesi";
            ViewBag.v0 = "Hakkımda İşlemleri";
            var client = _httpClientFactory.CreateClient();
            var data = JsonConvert.SerializeObject(updateAboutDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var responseMessage = await client.PutAsync("https://localhost:7070/api/Abouts", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "About", new { Area = "Admin" });
            }
            return View();
        }
        [Route("DeleteAbout/{id}")]
        public async Task<IActionResult> DeleteAbout(string id)
        {
            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.DeleteAsync($"https://localhost:7070/api/Abouts?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "About", new { Area = "Admin" });
            }
            return View();
        }
    }
}
