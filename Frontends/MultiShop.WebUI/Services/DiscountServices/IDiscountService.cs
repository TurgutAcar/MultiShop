using MultiShop.DtoLayer.DiscountDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.DiscountServices
{
    public interface IDiscountService
    {
        Task<Result<GetDiscountCodeDetailByCode>> GetDiscountCode(string code);
        Task<Result<int>> GetDiscountCouponCountRate(string code);
    }
}
