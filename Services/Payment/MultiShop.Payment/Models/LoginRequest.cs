namespace MultiShop.Payment.Models
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string UserPass { get; set; }
        public string CorporateCode { get; set; }
    }

    // Ana Response Modeli
    public class AuthResponse
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CorporateId { get; set; }
        public string CorporateCode { get; set; }
        public string CorporateName { get; set; }
        public TokenModel Token { get; set; }
        public string RoleName { get; set; }
    }

    // Token Detay Modeli
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenEndDate { get; set; }
    }
}
