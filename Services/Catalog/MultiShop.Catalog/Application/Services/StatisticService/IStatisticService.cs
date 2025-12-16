using MultiShop.Shared.Responses;

namespace  MultiShop.Catalog.Application.Services.StatisticService
{
    public interface IStatisticService
    {
        Task<Result<long>> GetCategoryCount();
        Task<Result<long>> GetProductCount();
        Task<Result<long>> GetBrandCount();
        Task<Result<decimal>> GetProductAvgPrice();
        Task<Result<string>> GetMaxPriceProductName();
        Task<Result<string>> GetMinPriceProductName();

    }
}
