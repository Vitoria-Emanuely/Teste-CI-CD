# Etapa 1: Base para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Etapa 2: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["User-api/User-api.csproj", "User-api/"]
RUN dotnet restore "User-api/User-api.csproj"
COPY . .
WORKDIR "/src/User-api"
RUN dotnet build "User-api.csproj" -c Release -o /app/build

# Etapa 3: Publicação
FROM build AS publish
RUN dotnet publish "User-api.csproj" -c Release -o /app/publish

# Etapa 4: Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "User-api.dll"]
