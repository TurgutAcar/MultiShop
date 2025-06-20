namespace MultiShop.WebUI.Services.StatisticServices.MessageStatisticService
{
    public interface IMessageStatisticService
    {
        Task<int> GetTotalMessageCount();
        Task<int> GetTotalMessageCountByReceiverId(string id);

    }
}
