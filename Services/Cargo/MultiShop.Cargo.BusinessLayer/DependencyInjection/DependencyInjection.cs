using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using Scrutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessRegistiration(this IServiceCollection services)
        {
           ;

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
