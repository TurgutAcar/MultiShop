using Microsoft.Extensions.DependencyInjection;

namespace MultiShop.Cargo.WebApi.Service
{
    public static class ServiceRegistiration
    {
        public static void AddAutoMapRegistiration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServiceRegistiration).Assembly);


         
        }
    }
}
