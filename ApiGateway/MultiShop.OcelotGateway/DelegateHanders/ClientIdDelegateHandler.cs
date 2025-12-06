using System.IdentityModel.Tokens.Jwt;

namespace MultiShop.OcelotGateway.DelegateHanders
{
    public class ClientIdDelegateHandler
    {
        private readonly RequestDelegate _next;

        public ClientIdDelegateHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                // ResourceOwnerPassword → sub
                //var userId = context.User.FindFirst("sub")?.Value;
                var userId = context.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

                // ClientCredentials → client_id
                var appId = context.User.FindFirst("client_id")?.Value;

                var clientId = userId.Value ?? appId; // hangisi varsa onu kullan

                if (!string.IsNullOrEmpty(clientId))
                {
                    context.Request.Headers["ClientId"] = clientId;
                }
            }

            await _next(context);
        }


    }
}
