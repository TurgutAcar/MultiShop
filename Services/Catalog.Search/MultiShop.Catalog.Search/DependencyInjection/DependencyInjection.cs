using FluentValidation;
using System.Reflection;
using Scrutor;
using Microsoft.AspNetCore.Diagnostics;
using MultiShop.Catalog.Search.Middlewares;

namespace MultiShop.Catalog.Search.DependencyInjection
{
    public static class DependencyInjection
    {
        public static  IServiceCollection AddApplication(this IServiceCollection services,WebApplicationBuilder builder)
        {
          //  builder.Services.AddHostedService<OutboxPublisherWorker>();

            //services.AddSingleton<IEventBus, RabbitMqEventBus>();

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            services.AddExceptionHandler<ExceptionHandler>();
            builder.Services.AddProblemDetails();
         

            services.Scan(action =>
            {
                action
                .FromAssemblies(Assembly.GetExecutingAssembly())
                            .AddClasses(classes => classes
                .Where(type =>
                    //type != typeof(ExceptionHandler)&&
                    !typeof(IExceptionHandler).IsAssignableFrom(type) &&
                    type.Name.EndsWith("Service") ||
                    type.Name.EndsWith("Repository") ||
                    type.Name.EndsWith("Handler")))

                //.AddClasses(classes =>
                //{
                //    classes.Where(type => type != typeof(ExceptionHandler));
                //})

                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .AsImplementedInterfaces()
                .WithScopedLifetime();
            });

         
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
