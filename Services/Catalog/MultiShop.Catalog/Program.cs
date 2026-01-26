using Elastic.Clients.Elasticsearch;
using HealthChecks.UI.Client;
using IdentityModel;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using MultiShop.Catalog.Infrastructure.DependencyInjection;
using MultiShop.Catalog.Infrastructure.Middlewares;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CatalogWritePolicy", policy =>
    {
        policy.RequireRole("Admin");
        policy.RequireClaim("scope", "CatalogFullPermission");
    });

    options.AddPolicy("CatalogReadPolicy", policy =>
    {
        policy.RequireClaim("scope",
            "CatalogReadPermission",
            "CatalogFullPermission");
    });
    //options.AddPolicy("CatalogReadPermission", policy =>
    //{
    //    policy.RequireClaim("scope", "CatalogReadPermission");
    //});
    //options.AddPolicy("CatalogFullPermission", policy =>
    //{
    //    policy.RequireClaim("scope", "CatalogFullPermission");
    //});
    //options.AddPolicy("CatalogReadOrFullPermission", policy =>
    //        policy.RequireAssertion(context =>
    //            context.User.HasClaim("scope", "CatalogReadPermission") ||
    //            context.User.HasClaim("scope", "CatalogFullPermission")));

});
builder.Services.AddMassTransit(x =>
{

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", 5673, "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ConfigureEndpoints(context);
    });

    //// MONGO OUTBOX
    //x.AddMongoDbOutbox(o =>
    //{
    //    o.Connection = "mongodb://catalogdb:27017/?replicaSet=rs0";
    //    o.DatabaseName = "MultiShopCatalogDb";

    //    o.UseBusOutbox();
    //});
});

// Elasticsearch Ayar
var esSettings = new ElasticsearchClientSettings(new Uri("http://elasticsearch:9200"))
                    .DefaultIndex("products");

var esClient = new ElasticsearchClient(esSettings);

builder.Services.AddSingleton(esClient);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt=>
{
    
    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.Audience = "ResourceCatalog";
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        RoleClaimType = JwtClaimTypes.Role
    };
});
builder.Services.AddApplication(builder);



// Add services to the container.
builder.Services.AddControllers(opt =>
{
    opt.Conventions.Add(new AuthorizeByMethodAttribute()); // Sınıf adın neyse o   
    //opt.Filters.Add(new AuthorizeFilter());
});
builder.Host.UseSerilog((context, services, configuration) => configuration
    // ?? Konfigürasyon dosyasından tüm Serilog bloğunu okur
    .ReadFrom.Configuration(context.Configuration)
    .Enrich.FromLogContext()
// Not: Artık .WriteTo.Console() veya .MinimumLevel.Warning() gibi 
// ayarları burada tutmanıza gerek yok, hepsi appsettings.json'da!
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddExceptionHandler<ExceptionHandler>();
//builder.Services.AddProblemDetails();

var app = builder.Build();
app.UseSerilogRequestLogging(); // Gelen her isteği loglar ve log context'ine request bilgilerini ekler.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health-check", new HealthCheckOptions //ACILACAK
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    ResultStatusCodes =
    {
        [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Healthy] = StatusCodes.Status200OK,
        [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded] = StatusCodes.Status200OK,
        [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
    }
});
//app.MapHealthChecksUI(options =>
//{
//    options.UIPath = "/health-ui";      
//    options.ApiPath = "/health-ui-api"; 
//});


app.Run();
