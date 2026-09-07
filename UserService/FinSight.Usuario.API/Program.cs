using FinSight.Usuario.Application.Services;
using FinSight.Usuario.Domain.Interfaces.IRepositories;
using FinSight.Usuario.Domain.Interfaces.IServices;
using FinSight.Usuario.Infrastructure.Data;
using FinSight.Usuario.Infrastructure.Repositories;
using FinSight.Usuario.Infrastructure.Utilidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration
    .SetBasePath(
        Path.Combine(
            Directory.GetCurrentDirectory(),
            "..",
            "FinSight.Usuario.Infrastructure"))
        .AddJsonFile("appsettings.json").Build();

var key = Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"] ?? "");
var configPath = builder.Configuration["connection:ConfigPath"] ?? "";

builder.Services.AddAuthentication((options) =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer((options) =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,

        ValidAudience = builder.Configuration["jwt:audience"],
        ValidIssuer = builder.Configuration["jwt:issuer"]
    };

    options.IncludeErrorDetails = true;
});

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<JWTService>();

Constants constants = new();
constants.ConfigPath = configPath;

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(Constants.Connection, ServerVersion.AutoDetect(Constants.Connection));
});

builder.Services.AddCors();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseCors((options) =>
{
    options.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

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
