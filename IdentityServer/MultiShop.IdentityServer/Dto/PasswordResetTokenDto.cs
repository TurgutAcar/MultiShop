using System;

namespace MultiShop.IdentityServer.Dto
{
    public class PasswordResetToken
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string TokenHash { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool Used { get; set; }
    }

}
