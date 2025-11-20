using MultiShop.Discount.Dtos;
using MultiShop.Shared.Responses;

namespace MultiShop.Discount.Services
{
    public interface IDiscountService
    {
        Task<Result<List<ResultDiscountCouponDto>>> GetAllDiscountCouponAsync();
        Task<Result<string>> CreateDiscountCouponAsync(CreateDiscountCouponDto coupon);
        Task<Result<string>> UpdateDiscountCouponAsync(UpdateDiscountCouponDto coupon);
        Task<Result<string>> DeleteDiscountCouponAsync(int couponId);
        Task<Result<GetByIdDiscountCouponDto>> GetByIdDiscountCouponAsync(int couponId);
        Task<Result<ResultDiscountCouponDto>> GetCodeDetailByCodeAsync(string code);
        Task<Result<int>> GetDiscountCouponCountRate(string code);
        Task<Result<int>> GetDiscountCouponCount();
    }
}
