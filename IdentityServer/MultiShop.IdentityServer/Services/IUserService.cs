using Microsoft.AspNetCore.Identity;
using MultiShop.IdentityServer.Dto;
using MultiShop.IdentityServer.Models;
using MultiShop.IdentityServer.Tools;
using MultiShop.Shared.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Services
{
    public interface IUserService
    {
        Task<Result<IdentityResult>> RegisterAsync(UserRegisterDto dto);
        Task<Result<string>> ForgotPasswordAsync(ForgotPasswordDto dto);
        Task<Result<string>> ResetPasswordAsync(ResetPasswordDto dto);

        Task<Result<TokenResponseViewModel>> LoginAsync(UserLoginDto dto);
        Task<Result<ApplicationUser>> GetUserAsync(string id);
        Task<Result<List<ApplicationUser>>> GetUserListAsync();
        Task<Result<int>> GetUserCountAsync();

    }
}
