using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.Interface;

namespace MultiShop.WebUI.Controllers
{
    [AllowAnonymous]

    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly IIdentityService _identityService;

        public LoginController(IIdentityService identityService,IHttpClientFactory httpClientFactory,ILoginService loginService)
        {
            this._identityService = identityService;
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Default");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(SignInDto signInDto)
        {
            var result= await _identityService.SignIn(signInDto);
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError(string.Empty, string.Join("<br/>", result.ErrorMessages));
                return View();
            }
            return RedirectToAction("Index","Default");
        }
   
      

    }
}
