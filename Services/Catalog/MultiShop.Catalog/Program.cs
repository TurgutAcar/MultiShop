using Microsoft.AspNetCore.Authentication.JwtBearer;
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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt=>
{
    
    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.Audience = "ResourceCatalog";
    opt.RequireHttpsMetadata = false;
});
builder.Services.AddApplication(builder);
builder.Services.AddExceptionHandler<ExceptionHandler>();



// Add services to the container.
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler();

app.MapControllers();

app.Run();
