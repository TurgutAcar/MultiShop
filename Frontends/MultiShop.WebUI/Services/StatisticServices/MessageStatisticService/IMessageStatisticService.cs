using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.StatisticServices.MessageStatisticService
{
    public interface IMessageStatisticService
    {
        Task<Result<int>> GetTotalMessageCount();
        Task<Result<int>> GetTotalMessageCountByReceiverId(string id);

    }
}
