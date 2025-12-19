
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MultiShop.Services.Stock.Persistence.Context;
using System.Reflection;
namespace MultiShop.Services.Stock.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<StockContext>(options =>
            options.UseMySql("server=localhost;database=StockDb;user=root;password=yourpassword",
        new MySqlServerVersion(new Version(8, 0, 36))));
            services.AddScoped<Domain.SeedWork.IUnitOfWork>(srv => srv.GetRequiredService<StockContext>());

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
