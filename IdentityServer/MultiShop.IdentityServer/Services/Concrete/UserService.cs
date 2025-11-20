using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MultiShop.IdentityServer.Dto;
using MultiShop.IdentityServer.Models;
using MultiShop.IdentityServer.Tools;
using MultiShop.Shared.Responses;
using SQLitePCL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Result<ApplicationUser>> GetUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;

        }

        public async Task<Result<int>> GetUserCountAsync()
        {
            int usercount =await _userManager.Users.CountAsync();
            return usercount;

        }

        public async Task<Result<List<ApplicationUser>>> GetUserListAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;

        }

        public async Task<Result<TokenResponseViewModel>> LoginAsync(UserLoginDto userLoginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(
                 userLoginDto.UserName, userLoginDto.Password, false, false
                 );
            var user = await _userManager.FindByNameAsync(userLoginDto.UserName);
            if (result.Succeeded)
            {
                GetCheckAppUserViewModel model = new GetCheckAppUserViewModel();
                model.UserName = userLoginDto.UserName;
                model.Id = user.Id;
                var token = JwtTokenGenerator.GenerateToken(model);
                return token;
            }
            else
            {
                return (500, "Kullanıcı adı veya şifre hatalı");

            }
        }

        public async Task<Result<IdentityResult>> RegisterAsync(UserRegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Name = dto.Name,
                Surname = dto.Surname,
            };
            return await _userManager.CreateAsync(user,dto.Password);
        }
    }
}
