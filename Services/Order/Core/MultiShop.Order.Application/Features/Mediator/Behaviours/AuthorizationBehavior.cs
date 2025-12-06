using MediatR;
using Microsoft.AspNetCore.Http;
using MultiShop.Order.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Behaviours
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse> // Tüm Komut/Sorgular için
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IResourceOwnerService _resourceOwnerService;

        public AuthorizationBehavior(
            IHttpContextAccessor httpContextAccessor,
            IResourceOwnerService resourceOwnerService)
        {
            _httpContextAccessor = httpContextAccessor;
            _resourceOwnerService = resourceOwnerService;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // 1. Yetkilendirme gerektirip gerektirmediğini kontrol edin
            if (request is IAuthorizeResourceRequest<int> resourceRequest)
            {
                // 2. Kullanıcıyı ve Kaynak ID'yi alın
                var user = _httpContextAccessor.HttpContext?.User;
                var resourceId = resourceRequest.ResourceId;

                // Eğer kimlik bilgileri yoksa, temel yetkilendirme (Authorize attribute) bunu yakalamalıdır.
                if (user == null || user.Identity?.IsAuthenticated != true)
                {
                    // Zaten yetkilendirilmiş olmalı, ancak yine de kontrol.
                    throw new UnauthorizedAccessException("Kimlik doğrulanmadı.");
                }

                // 3. Kaynak Sahibi Kontrolünü Çalıştırın (IResourceOwnerService çağrılır)
                bool isOwner = await _resourceOwnerService.IsResourceOwnerAsync(user, resourceId);

                if (!isOwner)
                {
                    // Yetkilendirme başarısız: Kullanıcı, kaynağın sahibi değil!
                    throw new AccessViolationException("Bu kaynağa erişim yetkiniz (403 Forbidden) bulunmamaktadır.");
                }
            }

            // 4. Yetkilendirme başarılıysa, bir sonraki adıma (Handler'a) geçin
            return await next();
        }
    }
}