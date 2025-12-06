using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Comment.DependencyInjection;
using MultiShop.Comment.Middlewares;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Audience = "ResourceComment";
    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.RequireHttpsMetadata = false;
});



// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAntiforgery(options =>
{
    // MVC projenizin formda üreteceði token adý (varsayýlan)
    options.Cookie.Name = ".AspNetCore.Antiforgery";

    // MVC'nin token'ý koyacaðý gizli form alanýnýn varsayýlan adý
    options.FormFieldName = "__RequestVerificationToken";

    // Güvenlik: Token'ýn gönderileceði HTTP baþlýk adý (Bu, API'ye özel iþlemlerde faydalýdýr)
    options.HeaderName = "X-CSRF-TOKEN";
});

builder.Services.AddRegistiration(builder.Configuration);
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();
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
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
