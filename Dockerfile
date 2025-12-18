# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo .csproj da raiz
COPY NovaCommerce.API.csproj .

# Restaura as dependências
RUN dotnet restore

# Copia todo o código fonte
COPY . .

# Publica a aplicação
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia os arquivos publicados
COPY --from=build /app/publish .

# Configurações para container
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Executa a API
ENTRYPOINT ["dotnet", "NovaCommerce.API.dll"]