FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

RUN dotnet restore FinSight.Dados.API/FinSight.Dados.API.csproj
RUN dotnet publish FinSight.Dados.API/FinSight.Dados.API.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "FinSight.Dados.API.dll"]