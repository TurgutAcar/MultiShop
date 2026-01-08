using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiShop.Order.Domain.SeedWork;
using MultiShop.Order.Persistence.Context;
using Scrutor;
using System.Reflection;

namespace MultiShop.Order.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });
            services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<OrderContext>());
            services.AddHealthChecks()
    .AddSqlServer(
        connectionString: configuration.GetConnectionString("SqlServer")!,
        name: "sqlserver",
        timeout: TimeSpan.FromSeconds(5),
        tags: new[] { "db", "sql", "sqlserver" }
    ).AddRabbitMQ(
        "rabbitmq:5672",
        name: "rabbitmq",
        tags: new[] { "cache", "rabbitmq" }
    ); ;

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
