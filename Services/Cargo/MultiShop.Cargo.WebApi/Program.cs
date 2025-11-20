using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Cargo.BusinessLayer.DependencyInjection;
using MultiShop.Cargo.DataAccessLayer.DependencyInjection;
using MultiShop.Cargo.WebApi.Middlewares;
using MultiShop.Cargo.WebApi.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDataAccessRegistiration(builder.Configuration);
builder.Services.AddBusinessRegistiration();
builder.Services.AddAutoMapRegistiration();

builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Audience = "ResourceCargo";
    opt.RequireHttpsMetadata = false;
    opt.Authority = builder.Configuration["IdentityServerUrl"];
});

// Add services to the container.

builder.Services.AddControllers();
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

app.MapControllers();

app.Run();
