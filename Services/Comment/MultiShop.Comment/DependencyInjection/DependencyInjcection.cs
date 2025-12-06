using Microsoft.EntityFrameworkCore;
using MultiShop.Comment.DataAccessLayer.Context;
using Scrutor;
using System.Reflection;

namespace MultiShop.Comment.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRegistiration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<CommentContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });


            services.Scan(action =>
            {
                action
                .FromAssemblies(Assembly.GetExecutingAssembly())

                .AddClasses(classes =>
                {
                    classes.Where(type => type != typeof(MultiShop.Comment.Middlewares.ExceptionHandler));
                })

                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .AsImplementedInterfaces()
                .WithScopedLifetime();
            });
            return services;
        }
    }
}
