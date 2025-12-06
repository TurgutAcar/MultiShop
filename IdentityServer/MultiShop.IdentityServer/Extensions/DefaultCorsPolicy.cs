using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace MultiShop.IdentityServer.Extensions
{
    public static class DefaultCorsPolicy
    {
        public static IServiceCollection AddDefaultCors(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddCors(options =>
            {
                if (env.IsDevelopment())
                {
                    // Development ortamı → her şeye izin ver
                    options.AddDefaultPolicy(policy =>
                    {
                        policy.AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials()
                              .SetIsOriginAllowed(_ => true);
                    });
                }
                else
                {
                    // Production ortamı → sadece güvenilir origin’lere izin ver
                    options.AddDefaultPolicy(policy =>
                    {
                        policy.WithOrigins("https://localhost:7288")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
                }
            });

            return services;
        }
    }
}
