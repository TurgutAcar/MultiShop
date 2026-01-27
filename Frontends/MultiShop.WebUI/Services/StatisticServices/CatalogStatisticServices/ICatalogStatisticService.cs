using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices
{
    public interface ICatalogStatisticService
    {
        Task<Result<long>> GetCategoryCount();
        Task<Result<long>> GetProductCount();
        Task<Result<long>> GetBrandCount();
        Task<Result<decimal>> GetProductAvgPrice();
        Task<Result<string>> GetMaxPriceProductName();
        Task<Result<string>> GetMinPriceProductName();
    }
}
