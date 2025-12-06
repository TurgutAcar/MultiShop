using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Authorization;
using MultiShop.Catalog.DependencyInjection;
using MultiShop.Catalog.Middlewares;


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
// Elasticsearch Ayarý
var esSettings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
                    .DefaultIndex("products"); // varsayýlan index

var esClient = new ElasticsearchClient(esSettings);

// DI Container kaydý
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

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
