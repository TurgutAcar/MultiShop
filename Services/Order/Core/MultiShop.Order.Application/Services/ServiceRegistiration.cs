using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;

namespace MultiShop.Order.Application.Services
{
    public static class ServiceRegistiration
    {
        public static void AddApplicationService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ServiceRegistiration).Assembly);

          
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistiration).Assembly));
            services.Scan(scan => scan
             .FromAssemblyOf<GetAddressByIdQueryHandler>() 
             .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Handler")))
             .AsSelf() // Interface yoksa AsSelf kullanılır
             .WithScopedLifetime());
        }
    }
}
