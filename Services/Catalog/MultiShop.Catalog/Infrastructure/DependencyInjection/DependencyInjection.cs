using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Scrutor;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MultiShop.Catalog.Infrastructure.Middlewares;
using MultiShop.Catalog.Infrastructure.Settings;

namespace MultiShop.Catalog.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static  IServiceCollection AddApplication(this IServiceCollection services,WebApplicationBuilder builder)
        {
          //  builder.Services.AddHostedService<OutboxPublisherWorker>();

            //services.AddSingleton<IEventBus, RabbitMqEventBus>();

            services.AddAutoMapper(typeof(DependencyInjection).Assembly);
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
            services.AddSingleton<IDatabaseSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            });

            services.Scan(action =>
            {
                action
                .FromAssemblies(Assembly.GetExecutingAssembly())

                .AddClasses(classes =>
                {
                    classes.Where(type => type != typeof(ExceptionHandler));
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
         tags: new[] { "db", "mongo", "sqlserver" });
    // ).AddRabbitMQ(
    //    "rabbitmq:5672",
    //    name: "rabbitmq",
    //    tags: new[] { "cache", "rabbitmq" }
    //); 
            //            builder.Services.AddHealthChecksUI(setup =>
            //            {
            //                setup.SetEvaluationTimeInSeconds(30); // 30 saniyede bir kontrol
            //                setup.MaximumHistoryEntriesPerEndpoint(50);
            //                setup.AddHealthCheckEndpoint("API Health", "http://localhost/health-check"); // UI bu endpointi izleyecek
            //            })
            //.AddInMemoryStorage();





            return services;

        }
    }
}
