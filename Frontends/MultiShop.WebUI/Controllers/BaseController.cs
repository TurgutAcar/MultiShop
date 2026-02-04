using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Mapping;
using MultiShop.WebUI.Models;

namespace MultiShop.WebUI.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult Json(Result<string> result)
        {
            if (result.IsSuccessful) return Json(JsResponse.Ok(result.Data!));
            else return Json(JsResponse.Error(result.ErrorMessages[0]!));
        }
        protected void SetUIErrorMessage(List<string> errorMessages)
        {
          
                TempData.SetUiMessage(new UiMessage
                {
                    Type = UiMessageType.Error,
                    Message = errorMessages[0]
                });
            
        
        }
        protected void SetUISuccessMessage(Result<string> result)
        {
         
            TempData.SetUiMessage(new UiMessage
            {
                Type = UiMessageType.Success,
                Message = result.Data!
            });
        }
        //    protected IActionResult JsonError(string msg) => Json(JsResponse.Error(msg));
        //   protected IActionResult JsonOk(string msg, object data = null) => Json(JsResponse.Ok(msg, data));
    }
}
