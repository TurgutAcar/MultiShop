using IdentityModel.OidcClient;
using k8s.KubeConfigModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MultiShop.IdentityServer.Data;
using MultiShop.IdentityServer.Dto;
using MultiShop.IdentityServer.Models;
using MultiShop.IdentityServer.Tools;
using MultiShop.Shared.Responses;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
namespace MultiShop.IdentityServer.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private ApplicationDbContext _context;
        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _context = context;
        }

        public async Task<Result<string>> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Result<string>.Failure("Kullanıcı bulunamadı");

            var token = Guid.NewGuid().ToString("N"); // Raw token

            var tokenHash = BCrypt.Net.BCrypt.HashPassword(token);

            _context.PasswordResetTokens.Add(new PasswordResetToken
            {
                UserId = user.Id,
                TokenHash = tokenHash,
                ExpireDate = DateTime.UtcNow.AddHours(1),
                Used = false
            });

            await _context.SaveChangesAsync();

            var link = $"https://frontend/reset-password?token={token}&email={dto.Email}";
            await _emailService.SendEmail(dto.Email, link);

            return "Email gönderildi";
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
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(e => e.Description).ToList();
                return MultiShop.Shared.Responses.Result<IdentityResult>.Failure(errorMessages);

            }

            await _userManager.AddToRoleAsync(user, "Customer");

            return result;
            // return await _userManager.CreateAsync(user,dto.Password);
        }

        public async Task<Result<string>> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Result<string>.Failure("Kullanıcı bulunamadı");

            var tokenRecord = await _context.PasswordResetTokens
                .Where(x => x.UserId == user.Id && !x.Used && x.ExpireDate > DateTime.UtcNow)
                .OrderByDescending(x => x.ExpireDate)
                .FirstOrDefaultAsync();

            if (tokenRecord == null)
                return Result<string>.Failure("Token geçersiz");

            if (!BCrypt.Net.BCrypt.Verify(dto.Token, tokenRecord.TokenHash))
                return Result<string>.Failure("Token doğrulanamadı");

            var result = await _userManager.ResetPasswordAsync(
                user,
                await _userManager.GeneratePasswordResetTokenAsync(user),
                dto.NewPassword);

            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(e => e.Description).ToList();
                return MultiShop.Shared.Responses.Result<string>.Failure(errorMessages);
            }

            tokenRecord.Used = true;
            await _context.SaveChangesAsync();

            return "Şifre değiştirildi";
        }

    }
}
