using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MultiShop.Catalog.DependencyInjection;
using MultiShop.Catalog.Middlewares;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CatalogReadPermission", policy =>
    {
        policy.RequireClaim("scope", "CatalogReadPermission");
    });
    options.AddPolicy("CatalogFullPermission", policy =>
    {
        policy.RequireClaim("scope", "CatalogFullPermission");
    });
    options.AddPolicy("CatalogReadOrFullPermission", policy =>
            policy.RequireAssertion(context =>
                context.User.HasClaim("scope", "CatalogReadPermission") ||
                context.User.HasClaim("scope", "CatalogFullPermission")));
   
});
// Elasticsearch Ayar�
var esSettings = new ElasticsearchClientSettings(new Uri("http://elasticsearch:9200"))
                    .DefaultIndex("products");

var esClient = new ElasticsearchClient(esSettings);

// DI Container kayd�
builder.Services.AddSingleton(esClient);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt=>
{
    
    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.Audience = "ResourceCatalog";
    opt.RequireHttpsMetadata = false;
});
builder.Services.AddApplication(builder);



// Add services to the container.
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter());
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
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

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
app.MapHealthChecks("/health-check", new HealthCheckOptions
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
