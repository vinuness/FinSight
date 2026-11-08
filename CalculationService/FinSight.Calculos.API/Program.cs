using System.Net;
using System.Net.Sockets;
using FinSight.Calculos.API.Interface;
using FinSight.Calculos.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<ICalculos, Calculos>()
    //Configura a criacao e gerenciamento das conexoes HTTP
    
    .ConfigurePrimaryHttpMessageHandler(() =>  
        //Cria manipulador com socket
        new SocketsHttpHandler
        {
            //funcao que será chamada quando estabelecer conexao com um servidor
            ConnectCallback = async(context, cancellationToken) =>
            {
                //Faz consulta DNS para descobrir o endereco IP do dominio
                var enderecos = await Dns.GetHostAddressesAsync(
                    //obtem o dominio que o HttpClient quer acessar
                    context.DnsEndPoint.Host,

                    //define que será procurado apenas enderecos IPv4
                    AddressFamily.InterNetwork,

                    //Permite cancelar a operação caso a requisicao seja cancelada
                    cancellationToken
                );

                var socket = new Socket
                (
                    //Define que o Socket utilizará IPv4.
                    AddressFamily.InterNetwork,

                    //Define uma conexão baseada em fluxo contínuo de dados.
                    SocketType.Stream,

                    //Define que a comunicação utilizará o protocolo TCP.
                    ProtocolType.Tcp
                );

                //conecta o socket ao servidor encontrado pelo DNS
                await socket.ConnectAsync(
                    //cria o endereco de destino utilizando o IP e porta
                    new IPEndPoint(
                        //pega o primeiro endereco IPv4 encontrado pelo DNS
                        enderecos[0],
                        //pega a porta que será utilizada na conexao
                        context.DnsEndPoint.Port
                    ),
                    //permite cancelar a tentativa de conexao
                    cancellationToken
                );

                return new NetworkStream(
                    //passa o socket conectado 
                    socket,

                    //quando o NetworkStream fechar o socket também deve fechar automaticamente
                    ownsSocket: true
                );
            }  
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
    options.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
