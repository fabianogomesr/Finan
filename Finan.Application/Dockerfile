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

COPY ./Finan.Application/*.csproj ./Finan.Application/
COPY ./Finan.Domain/*.csproj ./Finan.Domain/
COPY ./Finan.Infra.Data/*.csproj ./Finan.Infra.Data/

RUN dotnet restore ./Finan.Application/Finan.Application.csproj

COPY . .

WORKDIR /src/Finan.Application
RUN dotnet build -c Release -o /app/build

# ============================================================
# PUBLISH
# ============================================================
FROM build AS publish
RUN dotnet publish "Finan.Application.csproj" -c Release -o /app/publish /p:UseAppHost=false


# ============================================================
# FINAL
# ============================================================
FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Finan.Application.dll"]
