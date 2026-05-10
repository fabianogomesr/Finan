# ============================================================
# BASE: Runtime para produção
# ============================================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

ARG ENVIRONMENT=Production
ENV ASPNETCORE_ENVIRONMENT=$ENVIRONMENT

EXPOSE 8080

# ============================================================
# BUILD
# ============================================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./Finan.Api/*.csproj ./Finan.Api/
COPY ./Finan.Domain/*.csproj ./Finan.Domain/
COPY ./Finan.Infra.Data/*.csproj ./Finan.Infra.Data/

RUN dotnet restore ./Finan.Api/Finan.Api.csproj

COPY . .

WORKDIR /src/Finan.Api
RUN dotnet build -c Release -o /app/build

# ============================================================
# PUBLISH
# ============================================================
FROM build AS publish
RUN dotnet publish "Finan.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false


# ============================================================
# FINAL
# ============================================================
FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Finan.Api.dll"]
