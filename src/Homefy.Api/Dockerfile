# --- Stage 1: Build ------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia apenas os .csproj para aproveitar cache
COPY ["src/Homefy.Api/Homefy.Api.csproj", "src/Homefy.Api/"]
COPY ["src/Homefy.Application/Homefy.Application.csproj", "src/Homefy.Application/"]
COPY ["src/Homefy.Domain/Homefy.Domain.csproj", "src/Homefy.Domain/"]
COPY ["src/Homefy.Infrastructure/Homefy.Infrastructure.csproj", "src/Homefy.Infrastructure/"]

RUN dotnet restore "src/Homefy.Api/Homefy.Api.csproj"

# Copia o restante da solução
COPY . .

# Publica a API
WORKDIR "/src/src/Homefy.Api"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# --- Stage 2: Runtime ----------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Homefy.Api.dll"]