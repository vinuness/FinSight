FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore FinSight.UserService.sln

RUN dotnet publish FinSight.Usuario.API/FinSight.Usuario.API.csproj -c Release -o /app/publish --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

COPY --from=build src/FinSight.Usuario.Infrastructure/ /FinSight.Usuario.Infrastructure        

ENTRYPOINT ["dotnet", "FinSight.Usuario.API.dll"]