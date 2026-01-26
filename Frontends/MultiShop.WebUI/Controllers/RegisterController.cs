using System;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.RegisterDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Controllers
{
    [AllowAnonymous]

    public class RegisterController : Controller
    {
        private IHttpClientFactory _httpClientFactory;
        private IValidator<CreateRegisterDto> _validator;


        public RegisterController(IHttpClientFactory httpClientFactory, IValidator<CreateRegisterDto> validator)
        {
            _httpClientFactory = httpClientFactory;
            _validator = validator;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateRegisterDto createRegisterDto)
        {
            ValidationResult result = await _validator.ValidateAsync(createRegisterDto);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View(createRegisterDto);

                // Copy the validation results into ModelState.
                // ASP.NET uses the ModelState collection to populate 
                // error messages in the View.
                // result.AddToModelState(this.ModelState);

                // re-render the view when validation failed.
            }
            else
            {
                if (createRegisterDto.Password == createRegisterDto.ConfirmPassword)
                {
                    var client = _httpClientFactory.CreateClient();
                    var jsonData = JsonConvert.SerializeObject(createRegisterDto);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var responseMessage = await client.PostAsync("http://localhost:5001/api/Registers", content);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Login");
                    }
                }
            }

          
            return View();
        }
    }
}
