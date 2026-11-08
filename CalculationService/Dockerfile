FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

RUN dotnet restore FinSight.Calculos.API/FinSight.Calculos.API.csproj
RUN dotnet publish FinSight.Calculos.API/FinSight.Calculos.API.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "FinSight.Calculos.API.dll"]