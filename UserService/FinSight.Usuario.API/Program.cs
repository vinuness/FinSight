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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var key = Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"] ?? "");
var configPath = builder.Configuration["connection:ConfigPath"] ?? "";

builder.Services.AddAuthentication((options) =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer((options) =>
{
    options.MapInboundClaims = false;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };

    options.IncludeErrorDetails = true;
});

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IEnderecoService, EnderecoService>();

builder.Services.AddScoped<JWTService>();

Constants constants = new();
constants.ConfigPath = configPath;

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(Constants.Connection, ServerVersion.AutoDetect(Constants.Connection));
});

builder.Services.AddCors();

var app = builder.Build();

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
