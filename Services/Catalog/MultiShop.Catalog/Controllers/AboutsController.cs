using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.AboutDtos;
using MultiShop.Catalog.Services.AboutServices;

namespace MultiShop.Catalog.Controllers
{
    [AllowAnonymous]
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
            var values = await _aboutService.AboutListAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAboutById(string id)
        {
            var value=await _aboutService.GetByIdAboutAsync(id);
            return Ok(value);   
        }
        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            await _aboutService.CreateAboutAsync(createAboutDto);
            return Ok("About olusturuldu");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            await _aboutService.UpdateAboutAsync(updateAboutDto);
            return Ok("About güncellendi");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAbout(string id)
        {
            await _aboutService.DeleteAboutAsync(id);
            return Ok("About silindi");
        }
    }
}
