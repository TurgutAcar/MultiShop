// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MultiShop.IdentityServer.Data;
using MultiShop.IdentityServer.Models;
using MultiShop.IdentityServer.Seeds;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.IdentityServer
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                // uncomment to write to Azure diagnostics stream
                //.WriteTo.File(
                //    @"D:\home\LogFiles\Application\identityserver.txt",
                //    fileSizeLimitBytes: 1_000_000,
                //    rollOnFileSizeLimit: true,
                //    shared: true,
                //    flushToDiskInterval: TimeSpan.FromSeconds(1))
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
                .CreateLogger();
            try
            {


                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                   
                     var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                     await RoleSeed.SeedRolesAsync(roleManager);


                    var serviceProvider = scope.ServiceProvider;
                    var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

                    applicationDbContext.Database.Migrate();
                    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                    if (!userManager.Users.Any())
                    {
                        await RoleSeed.SeedAdminAsync(userManager);

                        //userManager.CreateAsync(new ApplicationUser
                        //{
                        //    Name="Turgut",
                        //    Surname="Acar",
                        //    UserName = "turgutacar05",
                        //    Email = "turgut@gmail.com",
                        //}, "11111aA*").Wait();

                    }
                }


                Log.Information("Starting host...");
                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }

            //try
            //{
            //    var seed = args.Contains("/seed");
            //    if (seed)
            //    {
            //        args = args.Except(new[] { "/seed" }).ToArray();
            //    }

            //    var host = CreateHostBuilder(args).Build();

            //    if (seed)
            //    {
            //        Log.Information("Seeding database...");
            //        var config = host.Services.GetRequiredService<IConfiguration>();
            //        var connectionString = config.GetConnectionString("DefaultConnection");
            //        SeedData.EnsureSeedData(connectionString);
            //        Log.Information("Done seeding database.");
            //        return 0;
            //    }

            //    Log.Information("Starting host...");
            //    host.Run();
            //    return 0;
            //}
            //catch (Exception ex)
            //{
            //    Log.Fatal(ex, "Host terminated unexpectedly.");
            //    return 1;
            //}
            //finally
            //{
            //    Log.CloseAndFlush();
            //}
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}