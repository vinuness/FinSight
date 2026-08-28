using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FinSight.Web.DelegateAuth;
using FinSight.Web.Interfaces;
using FinSight.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<JWTHandler>();

builder.Services.AddHttpClient<IDataService, DataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7162/api/APIData/");
}).AddHttpMessageHandler<JWTHandler>();

builder.Services.AddHttpClient<ICalculoService, CalculoService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7091/api/Calculo/");
}).AddHttpMessageHandler<JWTHandler>();

builder.Services.AddHttpClient<IUserService, UserService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7058/api/Usuario/");
}).AddHttpMessageHandler<JWTHandler>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    var token = context.Request.Cookies["jwt"];
    if (!string.IsNullOrEmpty(token))
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

        identity.AddClaim(new Claim(ClaimTypes.Email, jwt.Claims.First(e => e.Type == "email").Value));
        identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.First(e => e.Type == "role").Value));
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, jwt.Claims.First(e => e.Type == "nameid").Value));
        identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.First(e => e.Type == "unique_name").Value));

        context.User = new ClaimsPrincipal(identity);
    }
    await next();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
