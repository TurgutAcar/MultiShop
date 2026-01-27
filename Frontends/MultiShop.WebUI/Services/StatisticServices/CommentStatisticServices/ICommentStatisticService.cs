using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.StatisticServices.CommentStatisticServices
{
    public interface ICommentStatisticService
    {
        Task<Result<int>> GetActiveCommentCount();
        Task<Result<int>> GetPassiveCommentCount();
        Task<Result<int>> GetTotalCommentCount();

    }
}
