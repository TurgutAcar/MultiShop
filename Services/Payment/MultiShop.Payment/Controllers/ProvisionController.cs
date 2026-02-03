using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Payment.Models;

namespace MultiShop.Payment.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProvisionController : ControllerBase
    {
        [HttpPost]
        public IActionResult DoPayment(ParkingTransaction parkingTransaction)
        {
            var response = new ApiResponse
            {
                Version = 1,
                StatusCode = 200,
                Message = "Success",
                Result = new ProvisionResult
                {
                    CorporateCode = "TETRA00034",
                    CorporateReferenceNo = "10008",
                    Plate = "35AYT45",
                    ProvisionReferenceNo = "80010140040002025110510393853000",
                    ProvisionSonuc = 0,
                    ProvisionDescription = "HGS PARK GECIS UCRETI",
                    ProvisionFee = 1000
                }
            };


            return StatusCode(response.StatusCode, response);
        }
    }
}
