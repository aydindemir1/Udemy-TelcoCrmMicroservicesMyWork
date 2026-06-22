# TEMEL (BASE) İMAJ — Uygulama çalışırken kullanılan minimal imaj (ASP.NET 9.0 runtime)
# -----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base

# Uygulama konteyner içindeki çalışma dizini
WORKDIR /app

EXPOSE 5003

# -----------------------------
# BUILD STAGE — Uygulamanın derlendiği imaj (SDK içerir)
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

# Build konfigürasyonunu parametre olarak al (Release / Debug)
ARG BUILD_CONFIGURATION=Release

# Kaynak kodlar burada tutulacak
WORKDIR /src

# Copy local NuGet packages
COPY ./Packages /Packages
RUN dotnet nuget add source /Packages --name LocalPackages

# .csproj dosyasını kopyala (restore işlemi için sadece projeyi alıyoruz, tüm dosyaları değil)
COPY CustomerService/WebApi/WebApi.csproj CatalogService/WebApi/

# NuGet paketlerini indir (cache etkili olsun diye erken yapılıyor)
RUN dotnet restore "CatalogService/WebApi/WebApi.csproj"

# Geriye kalan tüm dosyaları kopyala (çünkü artık restore cache edildi)
COPY . .

# Build klasörüne geçiş yap
WORKDIR /src/CustomerService/WebApi

# Uygulamayı derle (sadece build işlemi, publish değil)
RUN dotnet build "WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build


# -----------------------------
# PUBLISH STAGE — Yayınlama çıktısı alınır (tek dosya gibi bağımlılıklar toplanır)
# -----------------------------
FROM build AS publish

ARG BUILD_CONFIGURATION=Release

# Uygulamayı yayınla (/p:UseAppHost=false → daha küçük image için .exe vs. üretme)
RUN dotnet publish "WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


# -----------------------------
# FINAL STAGE — Çalışacak son imaj (ASP.NET runtime üzerinde app kopyalanır)
# -----------------------------
FROM base AS final

# Uygulama çalıştırma klasörü
WORKDIR /app

# Yayınlanmış çıktıyı son aşamadaki runtime imajına kopyala
COPY --from=publish /app/publish .

# Uygulamanın başlatılacağı komut (entrypoint)
ENTRYPOINT ["dotnet", "WebApi.dll"]