using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Application.Services;
using MultiShop.Order.Persistence.Context;
using MultiShop.Order.Persistence.DependencyInjection;
using MultiShop.Order.Persistence.Repositories;
using MultiShop.Order.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.Audience = "ResourceOrder";
    options.Authority = builder.Configuration["IdentityServerUrl"];
});
builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();
// Add services to the container.
//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
//builder.Services.AddScoped(typeof(IOrderingRepository), typeof(OrderingRepository));

#region
//builder.Services.AddScoped<GetAddressByIdQueryHandler>();
//builder.Services.AddScoped<GetAddressQueryHandler>();
//builder.Services.AddScoped<CreateAddressCommandHandler>();
//builder.Services.AddScoped<UpdateAddressCommandHandler>();
//builder.Services.AddScoped<RemoveAddressCommandHandler>();

//builder.Services.AddScoped<GetOrderDetailQueryByIdHandler>();
//builder.Services.AddScoped<GetOrderDetailQueryHandler>();
//builder.Services.AddScoped<CreateOrderDetailCommandHandler>();
//builder.Services.AddScoped<UpdateOrderDetailCommandHandler>();
//builder.Services.AddScoped<RemoveOrderDetailCommandHandler>();
#endregion
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
app.UseExceptionHandler();

app.MapControllers();

app.Run();
