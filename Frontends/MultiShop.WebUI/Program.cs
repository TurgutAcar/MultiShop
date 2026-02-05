using Microsoft.AspNetCore.Authentication.Cookies;
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
using System.Globalization;
using MultiShop.WebUI.Filters;
using MultiShop.WebUI.GlobalException;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.NotifierServices;
using Microsoft.AspNetCore.Authentication;
using MultiShop.Shared.Enums;
using Polly;
using MultiShop.WebUI.Helper;
using MultiShop.WebUI.Validators.Catalog.Brand;
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
       // opt.ExpireTimeSpan = TimeSpan.FromDays(5);
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
builder.Services.AddTransient<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddTransient<ClientCredentialTokenHandler>();
builder.Services.AddTransient<UiAwareHttpHandler>();
builder.Services.AddTransient<TooManyRequestsRetryHandler>();

builder.Services.AddScoped<IApiClientFactory,ApiClientFactory>();

builder.Services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();

var values = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
builder.Services.AddScoped<UiHealthState>();
builder.Services.AddScoped<IUiNotifierService, UiNotifierService>();


// Program.cs



foreach (var service in values.Services)
{
    builder.Services.AddHttpClient(service.Key + "Visitor", opt =>
    {
        opt.BaseAddress = new Uri($"{values.OcelotUrl}/{service.Value.Path}/");
    })
     .AddHttpMessageHandler<ClientCredentialTokenHandler>()

    // 1️⃣ Polly Retry
    .AddPolicyHandler(ResiliencePolicies.GetRetryPolicy())

    // 2️⃣ Circuit Breaker
    .AddPolicyHandler(ResiliencePolicies.GetCircuitBreakerPolicy())

    // 3️⃣ Token

    // 4️⃣ UI Exception Mapping
    .AddHttpMessageHandler<UiAwareHttpHandler>();
    //        .AddHttpMessageHandler<ClientCredentialTokenHandler>()
    //    // POLLY FIRST
    //.AddTransientHttpErrorPolicy(p =>
    //    p.WaitAndRetryAsync(3, retry =>
    //        TimeSpan.FromSeconds(Math.Pow(2, retry))))
    //.AddTransientHttpErrorPolicy(p =>
    //    p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)))

    //// UI Exception Mapping LAST
    //.AddHttpMessageHandler<UiAwareHttpHandler>();
    //  .AddHttpMessageHandler<TooManyRequestsRetryHandler>()
    //.AddHttpMessageHandler<UiAwareHttpHandler>();

    builder.Services.AddHttpClient(service.Key + "Authorized", opt =>
    {
        opt.BaseAddress = new Uri($"{values.OcelotUrl}/{service.Value.Path}/");
    })
            .AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>()

         .AddPolicyHandler(ResiliencePolicies.GetRetryPolicy())
    .AddPolicyHandler(ResiliencePolicies.GetCircuitBreakerPolicy())
    .AddHttpMessageHandler<UiAwareHttpHandler>();
    //.AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>()
    // .AddHttpMessageHandler<TooManyRequestsRetryHandler>()
    //  .AddHttpMessageHandler<UiAwareHttpHandler>();
    // builder.Services.AddHttpClient(service.Key + "Manager", opt =>
    // {
    //     opt.BaseAddress = new Uri($"{values.OcelotUrl}/{service.Value.Path}/");
    // }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>()
    //.AddHttpMessageHandler<UiAwareHttpHandler>();
}



builder.Services.AddHttpClient<IUserService, UserService>(opt =>
{
    opt.BaseAddress = new Uri(values.IdentityServerUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>()
 .AddHttpMessageHandler<UiAwareHttpHandler>();

builder.Services.AddHttpClient<IUserIdentityService, UserIdentityService>(opt =>
{
    opt.BaseAddress = new Uri(values.IdentityServerUrl);
});
//builder.Services.AddTransient<IUserStatisticService, UserStatisticService>();

builder.Services.AddHttpClient<IUserStatisticService, UserStatisticService>(opt =>
{
    opt.BaseAddress = new Uri(values.IdentityServerUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

//builder.Services.AddHttpClient<ICategoryService, CategoryService>("Visitor",opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
//}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

//builder.Services.AddHttpClient<ICategoryService, CategoryService>("Authorized", opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
//}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddTransient<IProductService, ProductService>();


//builder.Services.AddHttpClient<IProductService, ProductService>("Visitor",opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
//}).AddHttpMessageHandler<ClientCredentialTokenHandler>();
//builder.Services.AddHttpClient<IProductService, ProductService>("Authorized",opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
//}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddTransient<IBrandService, BrandService>();

//builder.Services.AddHttpClient<IBrandService, BrandService>("Visitor", opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
//}).AddHttpMessageHandler<ClientCredentialTokenHandler>()
// .AddHttpMessageHandler<UiAwareHttpHandler>();
//builder.Services.AddHttpClient<IBrandService, BrandService>("Authorized", opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
//}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>()
// .AddHttpMessageHandler<UiAwareHttpHandler>();
builder.Services.AddTransient<ISpecialOfferService, SpecialOfferService>();

//builder.Services.AddHttpClient("Visitor",opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
//}).AddHttpMessageHandler<ClientCredentialTokenHandler>();
builder.Services.AddTransient<IFeatureService, FeatureService>();

//builder.Services.AddHttpClient<IFeatureService, FeatureService>("Authorized", opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
//}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddTransient<IOfferDiscountService, OfferDiscountService>();

//builder.Services.AddHttpClient<IOfferDiscountService, OfferDiscountService>("Visitor",opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
//}).AddHttpMessageHandler<ClientCredentialTokenHandler>();
//builder.Services.AddHttpClient<IOfferDiscountService, OfferDiscountService>("Authorized", opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
//}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddTransient<IAboutService, AboutService>();

//builder.Services.AddHttpClient<IAboutService, AboutService>(opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
//}).AddHttpMessageHandler<ClientCredentialTokenHandler>();
builder.Services.AddTransient<ISpecialOfferService, SpecialOfferService>();

//builder.Services.AddHttpClient("Visitor", opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
//}).AddHttpMessageHandler<ClientCredentialTokenHandler>();
//builder.Services.AddHttpClient("Authorized", opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
//}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddTransient<IFeatureSliderService, FeatureSliderService>();
//builder.Services.AddHttpClient("Visitor", opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
//}).AddHttpMessageHandler<ClientCredentialTokenHandler>()
// .AddHttpMessageHandler<UiAwareHttpHandler>();
//builder.Services.AddHttpClient("Authorized", opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
//}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>()
// .AddHttpMessageHandler<UiAwareHttpHandler>();
builder.Services.AddTransient<IProductImageService, ProductImageService>();

//builder.Services.AddHttpClient<IProductImageService, ProductImageService>(opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
//}).AddHttpMessageHandler<ClientCredentialTokenHandler>();
builder.Services.AddTransient<IProductDetailService, ProductDetailService>();

//builder.Services.AddHttpClient<IProductDetailService, ProductDetailService>(opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
//}).AddHttpMessageHandler<ClientCredentialTokenHandler>();
builder.Services.AddTransient<ICommentService, CommentService>();

//builder.Services.AddHttpClient<ICommentService,CommentService>(opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Comment.Path}/");
//}).AddHttpMessageHandler<ClientCredentialTokenHandler>();
builder.Services.AddTransient<ICommentStatisticService, CommentStatisticService>();

//builder.Services.AddHttpClient<ICommentStatisticService, CommentStatisticService>(opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Comment.Path}/");
//}).AddHttpMessageHandler<ClientCredentialTokenHandler>();
builder.Services.AddTransient<IBasketService, BasketService>();

//builder.Services.AddHttpClient<IBasketService, BasketService>(opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Basket.Path}/");
//}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddTransient<IDiscountService, DiscountService>();

//builder.Services.AddHttpClient<IDiscountService, DiscountService>(opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Discount.Path}/");
//}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddTransient<IOrderAddressService, OrderAddressService>();

//builder.Services.AddHttpClient<IOrderAddressService, OrderAddressService>(opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Order.Path}/");
//}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddTransient<ICatalogStatisticService, CatalogStatisticService>();

//builder.Services.AddHttpClient<ICatalogStatisticService, CatalogStatisticService>(opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}/");
//}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddTransient<IOrderOrderingService, OrderOrderingService>();

//builder.Services.AddHttpClient<IOrderOrderingService, OrderOrderingService>(opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Order.Path}/");
//}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddTransient<ICargoComanyService, CargoComanyService>();

//builder.Services.AddHttpClient<ICargoComanyService, CargoComanyService>(opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Cargo.Path}/");
//}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddTransient<ICargoCustomerService, CargoCustomerService>();

//builder.Services.AddHttpClient<ICargoCustomerService, CargoCustomerService>(opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Cargo.Path}/");
//}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddTransient<IMessageService, MessageService>();

//builder.Services.AddHttpClient<IMessageService, MessageService>(opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Message.Path}/");
//}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddTransient<IDiscountStatisticService, DiscountStatisticService>();

//builder.Services.AddHttpClient<IDiscountStatisticService, DiscountStatisticService>(opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Discount.Path}/");
//}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddTransient<IMessageStatisticService, MessageStatisticService>();

//builder.Services.AddHttpClient<IMessageStatisticService, MessageStatisticService>(opt =>
//{
//    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Message.Path}/");
//}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>()
// .AddHttpMessageHandler<UiAwareHttpHandler>();

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
builder.Services.AddValidatorsFromAssemblyContaining<CreateBrandValidators>();

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
//builder.Services.AddValidatorsFromAssemblyContaining<CreateRegisterValidators>();

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
        var criticality = ex.Criticality;
        if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            context.Response.StatusCode =  500;
            await context.Response.WriteAsJsonAsync(new { error = true });
            return;
        }
        if (criticality == UiCriticality.Low)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new
            {
                error = true,
                message = ex.Message
            });
            return;
        }

        if (criticality == UiCriticality.High)
        {
            await context.SignOutAsync();
            context.Response.Redirect("/Login/Index");
            return;
        }

        if (criticality == UiCriticality.Medium)
        {
            context.Response.Redirect("/Error/Critical");
            return;
        }
        //await context.SignOutAsync();
        //context.Response.Redirect("/Login/Index");
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
    pattern: "{controller=Default}/{action=Index}/{id?}");



//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//      name: "areas",
//      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    );
//});
app.Run();
