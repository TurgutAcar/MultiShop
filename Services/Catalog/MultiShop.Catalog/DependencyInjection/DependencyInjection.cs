using FluentValidation;
using Microsoft.Extensions.Options;
using MultiShop.Catalog.settings;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Scrutor;
using Microsoft.Extensions.Diagnostics.HealthChecks;

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

                .AddClasses(classes =>
                {
                    classes.Where(type => type != typeof(MultiShop.Catalog.Middlewares.ExceptionHandler));
                })

                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .AsImplementedInterfaces()
                .WithScopedLifetime();
            });
            builder.Services.AddHealthChecks()
     .AddMongoDb(
         mongodbConnectionString: builder.Configuration
            .GetSection("DatabaseSettings")
            .Get<DatabaseSettings>()!.ConnectionString,
         name: "mongodb",
         timeout: TimeSpan.FromSeconds(5),
         tags: new[] { "db", "nosql", "mongo" }
     );
            builder.Services.AddHealthChecksUI(setup =>
            {
                setup.SetEvaluationTimeInSeconds(30); // 30 saniyede bir kontrol
                setup.MaximumHistoryEntriesPerEndpoint(50);
                setup.AddHealthCheckEndpoint("API Health", "/health-check"); // UI bu endpointi izleyecek
            })
.AddInMemoryStorage();





            return services;

        }
    }
}
