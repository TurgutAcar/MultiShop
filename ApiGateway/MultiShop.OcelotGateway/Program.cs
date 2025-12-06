using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.AspNetCore.Http;
using MultiShop.OcelotGateway.DelegateHanders;
using System.IdentityModel.Tokens.Jwt;
using MultiShop.OcelotGateway.Extensions;
using Ocelot.Values;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer("OcelotAuthenticationScheme", opt =>
{
    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.Audience = "ResourceOcelot";
    opt.RequireHttpsMetadata = false;
});

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("ocelot.json").Build();
var env = builder.Environment;


builder.Services.AddOcelot(configuration);
builder.Services.AddDefaultCors(env);
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});
var app = builder.Build();
var config = new OcelotPipelineConfiguration
{
    PreAuthorizationMiddleware = async (ctx, next) =>
    {
        var method = ctx.Request.Method;
        var user = ctx.User;
        var scopes = user.FindAll("scope").Select(s => s.Value).ToList();

        var isReadMethod = method == "GET";
        var isWriteMethod = method is "POST" or "PUT" or "DELETE";
        var hasFullPermission = scopes.Contains("CatalogFullPermission");
        var hasReadPermission = scopes.Contains("CatalogReadPermission");


        if (isWriteMethod && !hasFullPermission)
        {
            ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
            await ctx.Response.WriteAsync("You do not have permission to perform this operation.");
            return;
        }
        if (isReadMethod && !(hasFullPermission || hasReadPermission))
        {
            ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
            await ctx.Response.WriteAsync("Okuma iþlemleri için CatalogReadPermission veya CatalogFullPermission gerekli.");
            return;
        }



        await next.Invoke();
    }
};




await app.UseOcelot(config);
app.UseHttpsRedirection();
app.UseResponseCompression();
app.UseCors();
app.UseMiddleware<ClientIdDelegateHandler>();

app.MapGet("/", () => "Hello World!");

app.Run();
