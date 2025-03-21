# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Finan.Application/Finan.Application.csproj", "Finan.Application/"]
COPY ["Finan.Domain/Finan.Domain.csproj", "Finan.Domain/"]
COPY ["Finan.Infra.CrossCutting.DTO/Finan.Infra.CrossCutting.DTO.csproj", "Finan.Infra.CrossCutting.DTO/"]
COPY ["Finan.Infra.CrossCutting.IoC/Finan.Infra.CrossCutting.IoC.csproj", "Finan.Infra.CrossCutting.IoC/"]
COPY ["Finan.Infra.Data/Finan.Infra.Data.csproj", "Finan.Infra.Data/"]
COPY ["Finan.Service/Finan.Service.csproj", "Finan.Service/"]
RUN dotnet restore "./Finan.Application/Finan.Application.csproj"
COPY . .
WORKDIR "/src/Finan.Application"
RUN dotnet build "./Finan.Application.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Finan.Application.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Finan.Application.dll"]