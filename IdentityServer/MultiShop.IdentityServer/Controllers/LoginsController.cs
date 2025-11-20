using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.Dto;
using MultiShop.IdentityServer.Models;
using MultiShop.IdentityServer.Services;
using MultiShop.IdentityServer.Tools;

namespace MultiShop.IdentityServer.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginDto userLoginDto)
        {
            var response = await _userService.LoginAsync(userLoginDto);
            return StatusCode(response.StatusCode, response);

        
        }
    }
}
