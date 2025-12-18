# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln .
COPY NovaCommerce.API.csproj ./
RUN dotnet restore "NovaCommerce.API.csproj"

COPY . .
WORKDIR /app/NovaCommerce.API
RUN dotnet publish "NovaCommerce.API.csproj" -c Release -o out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/NovaCommerce.API/out .

EXPOSE 8080
ENTRYPOINT ["dotnet", "NovaCommerce.API.dll"]
