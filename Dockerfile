# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo .csproj (usando o nome exato)
COPY NovaCommerce.API.csproj ./

# Restaura os pacotes
RUN dotnet restore NovaCommerce.API.csproj

# Copia todo o resto do código fonte
COPY . ./

# Publica a aplicação em modo Release
RUN dotnet publish NovaCommerce.API.csproj -c Release -o /app/publish --no-restore

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia os arquivos publicados da etapa anterior
COPY --from=build /app/publish ./

# Expoe a porta padrão do ASP.NET Core em containers
EXPOSE 8080

# Define a URL que a app vai escutar (boa prática em containers)
ENV ASPNETCORE_URLS=http://+:8080

# Executa a aplicação
ENTRYPOINT ["dotnet", "NovaCommerce.API.dll"]