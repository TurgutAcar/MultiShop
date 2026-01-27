using MultiShop.DtoLayer.OrderDtos.OrderOrderingDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.OrderServices.OrderOrderingServices
{
    public interface IOrderOrderingService
    {
        Task<Result<List<ResultOrderingByUserIdDto>>> GetOrderingByUserId(string id);
    }
}
