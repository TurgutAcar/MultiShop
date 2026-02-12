using System;
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
    public class ForgotPasswordsController : ControllerBase
    {
        private readonly IUserService _userService;

        public ForgotPasswordsController(IUserService userService)
        {
            _userService = userService;
        }

    
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var response = await _userService.ForgotPasswordAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var response = await _userService.ResetPasswordAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
