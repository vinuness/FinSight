using System.Text;
using FinSight.Metas.Application.Services;
using FinSight.Metas.Domain.Entities;
using FinSight.Metas.Domain.Interfaces;
using FinSight.Metas.Infrastructure.Data;
using FinSight.Metas.Infrastructure.Repositories;
using FinSight.Metas.Infrastructure.Utilidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");

var jwt = Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"]);
var issuer = builder.Configuration["jwt:issuer"];
var audience = builder.Configuration["jwt:audience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateLifetime = true,

        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(jwt)
    };
});

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMetaRepository, MetaRepository>();
builder.Services.AddScoped<IMetaService, MetaService>();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(Constants.Connection, ServerVersion.AutoDetect(Constants.Connection));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
