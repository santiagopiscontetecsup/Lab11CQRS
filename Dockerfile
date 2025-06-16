# Usa la imagen oficial de .NET 8 como base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

# Cambia esto si tu archivo .csproj se encuentra en otra ruta
RUN dotnet restore Lab11SantiagoPisconte.Api/Lab11SantiagoPisconte.Api.csproj
RUN dotnet publish Lab11SantiagoPisconte.Api/Lab11SantiagoPisconte.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Lab11SantiagoPisconte.Api.dll"]
