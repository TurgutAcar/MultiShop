using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MultiShop.Catalog.Search.DependencyInjection;
using MultiShop.Catalog.Search.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization(options =>
{
  

    options.AddPolicy("CatalogSearchPolicy", policy =>
    {
        policy.RequireClaim("scope",
            "CatalogSearchReadPermission"
           );
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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Elasticsearch Ayar
var esSettings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
                    .DefaultIndex("products");

var esClient = new ElasticsearchClient(esSettings);

builder.Services.AddSingleton(esClient);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{

    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.Audience = "ResourceCatalogSearch";
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        //RoleClaimType = JwtClaimTypes.Role,
        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
        // Eðer scope'larda da sorun yaþarsan þunu da ekleyebilirsin:
        // NameClaimType = "name"
    };
});
builder.Services.AddApplication(builder);



// Add services to the container.
builder.Services.AddControllers(opt =>
{
    opt.Conventions.Add(new AuthorizeByMethodAttribute()); // Sýnýf adýn neyse o   
    //opt.Filters.Add(new AuthorizeFilter());
});
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
app.UseHttpsRedirection();


app.MapControllers();

app.Run();
