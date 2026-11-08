FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore FinSight.GoalService.sln

RUN dotnet publish FinSight.Metas.API/FinSight.Metas.API.csproj -c Release -o /app/publish --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

COPY --from=build src/FinSight.Metas.Infrastructure/ /FinSight.Metas.Infrastructure

ENTRYPOINT ["dotnet", "FinSight.Metas.API.dll"]