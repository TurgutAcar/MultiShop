using FluentValidation;
using Microsoft.Extensions.Options;
using MultiShop.Catalog.settings;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Scrutor;

namespace MultiShop.Catalog.DependencyInjection
{
    public static class DependencyInjection
    {
        public static  IServiceCollection AddApplication(this IServiceCollection services,WebApplicationBuilder builder)
        {
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
            services.AddScoped<IDatabaseSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
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
