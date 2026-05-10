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

COPY ./Finan.Web/*.csproj ./Finan.Web/
COPY ./Finan.Domain/*.csproj ./Finan.Domain/
COPY ./Finan.Service/*.csproj ./Finan.Service/
COPY ./Finan.Web.Clients/*.csproj ./Finan.Web.Clients/

RUN dotnet restore ./Finan.Web/Finan.Web.csproj

COPY . .

WORKDIR /src/Finan.Web

RUN apt-get update && apt-get install -y nodejs npm

RUN npm install
RUN npm run build

RUN dotnet build -c Release -o /app/build

# ============================================================
# PUBLISH
# ============================================================
FROM build AS publish
RUN dotnet publish "./Finan.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ============================================================
# FINAL
# ============================================================
FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Finan.Web.dll"]