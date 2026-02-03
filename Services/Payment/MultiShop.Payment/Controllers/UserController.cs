using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MultiShop.Payment.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MultiShop.Payment.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // 1. Kullanıcıyı veritabanında doğrula (Örnek: if (user == null) return Unauthorized();)

            // 2. Token oluştur (Gerçek bir projede bu kod bir 'TokenHandler' servisinde olmalıdır)
            var tokenData = GenerateToken(request.UserName, "Admin");

            // 3. Response Modelini doldur
            var response = new AuthResponse
            {
                UserName = request.UserName,
                FirstName = "Ahmet",
                LastName = "Yılmaz",
                CorporateId = 123,
                CorporateCode = request.CorporateCode,
                CorporateName = "Tetra Yazılım",
                RoleName = "Admin",
                Token = new TokenModel
                {
                    AccessToken = tokenData.AccessToken,
                    Expiration = tokenData.Expiration,
                    RefreshToken = tokenData.RefreshToken,
                    RefreshTokenEndDate = tokenData.RefreshTokenEndDate
                }
            };

            return Ok(response);
        }
        [NonAction]
        public TokenModel GenerateToken(string username, string role)
        {
            // 1. Secret Key tanımı (Normalde bu AppSettings'ten gelmeli)
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BuSizinCokGizliVeGucluAnahtariniz123!"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // 2. Token içine gömülecek bilgiler (Claims)
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Role, role),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            // 3. Access Token Oluşturma
            var tokenDescriptor = new JwtSecurityToken(
                issuer: "tetra-api.com",
                audience: "tetra-api.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(15), // Genelde kısa tutulur
                signingCredentials: credentials);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            // 4. Refresh Token Oluşturma (Rastgele bir string)
            var refreshToken = GenerateRefreshToken();

            return new TokenModel
            {
                AccessToken = accessToken,
                Expiration = tokenDescriptor.ValidTo,
                RefreshToken = refreshToken,
                RefreshTokenEndDate = DateTime.Now.AddDays(7) // Daha uzun ömürlü
            };
        }

        // Güvenli rastgele karakterler üretir
        [NonAction]
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
