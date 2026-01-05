
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MultiShop.Services.Stock.Persistence.Context;
using MultiShop.Services.Stock.Core.Domain.SeedWork;
using System.Reflection;
using Microsoft.Extensions.Configuration;
namespace MultiShop.Services.Stock.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<StockContext>(options =>
            options.UseMySql(configuration.GetConnectionString("MySQL"),
        new MySqlServerVersion(new Version(8, 0, 36))));
            services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<StockContext>());

            services.Scan(action =>
            {
                action
                .FromAssemblies(Assembly.GetExecutingAssembly())
                .AddClasses(publicOnly: false)
                .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .AsImplementedInterfaces()
                .WithScopedLifetime();
            });
            return services;


        }
    }
}
