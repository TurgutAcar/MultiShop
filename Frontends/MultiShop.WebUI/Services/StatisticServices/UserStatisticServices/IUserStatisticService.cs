using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.StatisticServices.UserStatisticServices
{
    public interface IUserStatisticService
    {
        Task<Result<int>> GetUserCount();
    }
}
