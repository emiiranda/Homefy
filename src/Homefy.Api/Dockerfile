# --- Stage 1: Build ------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia apenas o .csproj primeiro para aproveitar o cache de camadas
COPY ["Homefy.Api/Homefy.Api.csproj", "Homefy.Api/"]
RUN dotnet restore "Homefy.Api/Homefy.Api.csproj"

# Copia o restante e publica
COPY . .
WORKDIR "/src/Homefy.Api"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# --- Stage 2: Runtime ----------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Homefy.Api.dll"]