using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.Dto;
using MultiShop.IdentityServer.Models;
using MultiShop.IdentityServer.Services;

namespace MultiShop.IdentityServer.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly IUserService _userService;

        public RegistersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> UserRegister(UserRegisterDto userRegisterDto)
        {
            var response=await _userService.RegisterAsync(userRegisterDto);
            return StatusCode(response.StatusCode, response);

          
        }
    }
}
