# -----------------------------
# Stage 1: Build
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Solution ve projeleri kopyala
COPY *.sln .
COPY YazOkulu.GEN.API/*.csproj ./YazOkulu.GEN.API/
COPY YazOkulu.Data/*.csproj ./YazOkulu.Data/
COPY YazOkulu.Core/*.csproj ./YazOkulu.Core/
COPY YazOkulu.GENAppService/*.csproj ./YazOkulu.GENAppService/
COPY YazOkulu.Extensions/*.csproj ./YazOkulu.Extensions/
COPY YazOkuluAppServiceModels/*.csproj ./YazOkuluAppServiceModels/
COPY YazOkuluBaseAppService/*.csproj ./YazOkuluBaseAppService/

# NuGet paketlerini restore et
RUN dotnet restore

# Tüm projeleri kopyala
COPY . .

# Publish işlemi
RUN dotnet publish YazOkulu.GEN.API/YazOkulu.GEN.API.csproj -c Release -o /app/publish

# -----------------------------
# Stage 2: Runtime
# -----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Başlatma komutu
ENTRYPOINT ["dotnet", "YazOkulu.GEN.API.dll"]
