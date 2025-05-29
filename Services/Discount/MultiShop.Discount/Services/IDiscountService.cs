using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services
{
    public interface IDiscountService
    {
        Task<List<ResultDiscountCouponDto>> GetAllDiscountCouponAsync();
        Task CreateDiscountCouponAsync(CreateDiscountCouponDto coupon);
        Task UpdateDiscountCouponAsync(UpdateDiscountCouponDto coupon);
        Task DeleteDiscountCouponAsync(int couponId);
        Task<GetByIdDiscountCouponDto> GetByIdDiscountCouponAsync(int couponId);
        Task<ResultDiscountCouponDto> GetCodeDetailByCodeAsync(string code);
        Task<int> GetDiscountCouponCountRate(string code);
    }
}
