using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.StatisticServices.DiscountStatisticServices
{
    public interface IDiscountStatisticService
    {
        Task<Result<int>> GetDiscountCouponCount();
    }
}
