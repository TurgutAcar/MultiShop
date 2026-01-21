using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Razor;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.AboutServices;
using MultiShop.WebUI.Services.BasketService;
using MultiShop.WebUI.Services.BrandServices;
using MultiShop.WebUI.Services.CargoServices.CargoCompanyServices;
using MultiShop.WebUI.Services.CargoServices.CargoCustomerServices;
using MultiShop.WebUI.Services.CatologService.CategoryService;
using MultiShop.WebUI.Services.CatologService.FeatureSliderServices;
using MultiShop.WebUI.Services.CatologService.ProductService;
using MultiShop.WebUI.Services.CatologService.SpecialOfferServices;
using MultiShop.WebUI.Services.CommentServices;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.DiscountServices;
using MultiShop.WebUI.Services.FeatureService;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.MessageService;
using MultiShop.WebUI.Services.OfferDiscountServices;
using MultiShop.WebUI.Services.OrderServices.OrderAddressServices;
using MultiShop.WebUI.Services.OrderServices.OrderOrderingServices;
using MultiShop.WebUI.Services.ProductDetailServices;
using MultiShop.WebUI.Services.ProductImageServices;
using MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.CommentStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.DiscountStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.MessageStatisticService;
using MultiShop.WebUI.Services.StatisticServices.UserStatisticServices;
using MultiShop.WebUI.Services.UserIdentityService;
using MultiShop.WebUI.Settings;
using FluentValidation;
using System;
using MultiShop.WebUI.Validators;
using FluentValidation.AspNetCore;
using MultiShop.DtoLayer.IdentityDtos.RegisterDtos;
using System.Globalization;
using MultiShop.WebUI.Filters;
using MultiShop.WebUI.GlobalException;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.NotifierServices;
using Microsoft.AspNetCore.Authentication;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAntiforgery(options =>
{
    // Bu isimler, MVC'nin formda ve cookie'de kullanacağı varsayılan isimlerdir.
    // Explicit olarak tanımlamak karışıklığı önler.
    options.Cookie.Name = ".AspNetCore.Antiforgery";
    options.FormFieldName = "__RequestVerificationToken";

    // HeaderName ayarı, bu senaryoda (geleneksel formlar) doğrudan kullanılmasa da, 
    // iyi bir uygulama olarak tutulabilir.
    options.HeaderName = "X-CSRF-TOKEN";
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
    AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Login/Index/";
        opt.LogoutPath = "/Login/Logout";
        opt.AccessDeniedPath = "/Login/AccessDenied";
        opt.ExpireTimeSpan = TimeSpan.FromDays(5);
        opt.Cookie.Name = "MultiShopCookie";
        opt.ExpireTimeSpan = TimeSpan.FromHours(2);
        opt.SlidingExpiration = true;
    });
builder.Services.AddAccessTokenManagement();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILoginService,LoginService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();

// Add services to the container.
builder.Services.AddHttpClient();
//builder.Services.AddControllersWithViews();
builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));
builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddScoped<ClientCredentialTokenHandler>();
builder.Services.AddScoped<UiAwareHttpHandler>();

builder.Services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();

var values = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
builder.Services.AddScoped<UiHealthState>();
builder.Services.AddScoped<IUiNotifierService, UiNotifierService>();

builder.Services.AddHttpClient<IUserService, UserService>(opt =>
{
    opt.BaseAddress = new Uri(values.IdentityServerUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>()
 .AddHttpMessageHandler<UiAwareHttpHandler>();

builder.Services.AddHttpClient<IUserIdentityService, UserIdentityService>(opt =>
{
    opt.BaseAddress = new Uri(values.IdentityServerUrl);
});

builder.Services.AddHttpClient<IUserStatisticService, UserStatisticService>(opt =>
{
    opt.BaseAddress = new Uri(values.IdentityServerUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<ICategoryService, CategoryService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IProductService, ProductService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IBrandService, BrandService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IFeatureService, FeatureService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();
builder.Services.AddHttpClient<IOfferDiscountService, OfferDiscountService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();
builder.Services.AddHttpClient<IAboutService, AboutService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();
builder.Services.AddHttpClient<ISpecialOfferService, SpecialOfferService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();
builder.Services.AddHttpClient<IFeatureSliderService, FeatureSliderService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>()
 .AddHttpMessageHandler<UiAwareHttpHandler>();
builder.Services.AddHttpClient<IProductImageService, ProductImageService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IProductDetailService, ProductDetailService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();
builder.Services.AddHttpClient<ICommentService,CommentService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Comment.Path}/");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();
builder.Services.AddHttpClient<ICommentStatisticService, CommentStatisticService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Comment.Path}/");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();
builder.Services.AddHttpClient<IBasketService, BasketService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Basket.Path}/");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IDiscountService, DiscountService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Discount.Path}/");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IOrderAddressService, OrderAddressService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Order.Path}/");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddHttpClient<ICatalogStatisticService, CatalogStatisticService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IOrderOrderingService, OrderOrderingService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Order.Path}/");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddHttpClient<ICargoComanyService, CargoComanyService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Cargo.Path}/");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddHttpClient<ICargoCustomerService, CargoCustomerService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Cargo.Path}/");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IMessageService, MessageService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Message.Path}/");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddHttpClient<IDiscountStatisticService, DiscountStatisticService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Discount.Path}/");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddHttpClient<IMessageStatisticService, MessageStatisticService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Message.Path}/");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
//builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<AuthRedirectFilter>();
});

builder.Services.AddFluentValidationAutoValidation(options =>
{
    options.DisableDataAnnotationsValidation = true;
});

builder.Services.AddValidatorsFromAssemblyContaining<CreateRegisterValidators>();
ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");


// FluentValidation ekleniyor
//builder.Services.AddValidatorsFromAssemblyContaining<CreateRegisterValidators>();



//builder.Services.AddFluentValidationClientsideAdapters(); // İsteğe bağlı

//builder.Services.AddControllersWithViews()
//.AddFluentValidation(opt =>
//{
//   opt.RegisterValidatorsFromAssemblyContaining<CreateRegisterValidators>;
//  opt.DisableDataAnnotationsValidation = true;
//   opt.ValidatorOptions.LanguageManager.Culture = new System.Globalization.CultureInfo("tr");
//});
builder.Services.AddValidatorsFromAssemblyContaining<CreateRegisterValidators>();

builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "Resources";
});
builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
var app = builder.Build();
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (UiCriticalException ex)
    {
        await context.SignOutAsync();
        context.Response.Redirect("/Login/Index");
        //context.Response.Redirect("/Error/Critical");
    }
});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
var supportedCultures = new[] { "en", "fr", "de", "tr" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[3])
    .AddSupportedCultures(supportedCultures).AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.Run();
