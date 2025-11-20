using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.AboutDtos;
using MultiShop.Catalog.Services.AboutServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : ControllerBase
    {
        private readonly IAboutService _aboutService;

        public AboutsController(IAboutService aboutService)
        {
            this._aboutService = aboutService;
        }
        [HttpGet]
        public async Task<IActionResult> AboutList()
        {
            var response = await _aboutService.AboutListAsync();
            return StatusCode(response.StatusCode, response);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAboutById(string id)
        {
            var response = await _aboutService.GetByIdAboutAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            var response=await _aboutService.CreateAboutAsync(createAboutDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            var response=await _aboutService.UpdateAboutAsync(updateAboutDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAbout(string id)
        {
            var response=await _aboutService.DeleteAboutAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
