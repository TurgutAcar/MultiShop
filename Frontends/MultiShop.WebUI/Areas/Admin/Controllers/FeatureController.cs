using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.WebUI.Services.FeatureService;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Route("Admin/Feature")]
    [Area("Admin")]
    public class FeatureController : Controller
    {
        private IHttpClientFactory _httpClientFactory;
        private IFeatureService _featureService;

        public FeatureController(IHttpClientFactory httpClientFactory, IFeatureService featureService)
        {
            _httpClientFactory = httpClientFactory;
            _featureService = featureService;
        }
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            FeatureViewbagList();
            var values=await _featureService.FeatureListAsync();
            return View(values);
            
        }
        [Route("CreateFeature")]
        public IActionResult CreateFeature()
        {
            FeatureViewbagList();

            return View();

        }
        [Route("CreateFeature")]
        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            await _featureService.CreateFeatureAsync(createFeatureDto);
            return RedirectToAction("Index", "Feature", new { Area = "Admin" });

           
        }
        [Route("UpdateFeature/{id}")]
        public async Task<IActionResult> UpdateFeature(string id)
        {

            FeatureViewbagList();
            var value=await _featureService.GetByIdFeatureAsync(id);
             return View(value);
        }
        [Route("UpdateFeature/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            await _featureService.UpdateFeatureAsync(updateFeatureDto);
            return RedirectToAction("Index", "Feature", new { Area = "Admin" });
          
        }
        [Route("DeleteFeature/{id}")]
        public async Task<IActionResult> DeleteFeature(string id)
        {
            await _featureService.DeleteFeatureAsync(id);
            return RedirectToAction("Index", "Feature", new { Area = "Admin" });
         
        }
        void FeatureViewbagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Özellikler";
            ViewBag.v3 = "Özellik Listesi";
            ViewBag.v0 = "Özellik İşlemlei";
        }
    }
}
