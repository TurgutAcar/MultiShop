using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiShop.Stock.Application.Features.Mediator.Handlers.StockItemHandlers;


namespace MultiShop.Services.Stock.Core.Application.Services
{
    public static class ServiceRegistiration
    {
        public static void AddApplicationService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ServiceRegistiration).Assembly);
        

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistiration).Assembly));
            services.Scan(scan => scan
             .FromAssemblyOf<GetStockItemByIdQueryHandler>()
             .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Handler")))
             .AsSelf() // Interface yoksa AsSelf kullanılır
             .WithScopedLifetime());


         
        }
    }
}
