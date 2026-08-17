using FinSight.Dados.API.Controllers;
using FinSight.Dados.API.Service.AwesomeAPIService;
using FinSight.Dados.API.Service.Interface;
using System.Net;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAPIData, APIData>();

builder.Services.AddHttpClient<IAPIData, APIData>()
    .ConfigurePrimaryHttpMessageHandler(() =>
        new SocketsHttpHandler
        {
            ConnectCallback = async (context, cancellationToken) =>
            {
                var addresses = await Dns.GetHostAddressesAsync(
                    context.DnsEndPoint.Host,
                    AddressFamily.InterNetwork,
                    cancellationToken
                );

                var socket = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp
                );

                await socket.ConnectAsync(
                    new IPEndPoint(addresses[0], context.DnsEndPoint.Port),
                    cancellationToken
                );

                return new NetworkStream(socket, ownsSocket: true);
            }
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors((options) =>
{
    options.AllowAnyMethod()
        .AllowAnyOrigin()
        .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
