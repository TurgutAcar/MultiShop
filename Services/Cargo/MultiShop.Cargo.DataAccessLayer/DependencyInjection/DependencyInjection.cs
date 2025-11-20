using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using Scrutor;
using System.Reflection;


namespace MultiShop.Cargo.DataAccessLayer.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessRegistiration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CargoContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.Scan(action =>
            {
                action
                .FromAssemblies(Assembly.GetExecutingAssembly())
                .AddClasses(publicOnly: false)
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .AsImplementedInterfaces()
                .WithScopedLifetime();
            });
            return services;
        }
    }
}
