@echo off

echo Iniciando o build do projeto...
cd /d %~dp0
docker build -f Finan.Application/Dockerfile -t finan-app --build-arg ENVIRONMENT=Production --build-arg BUILD_CONFIGURATION=Release .

echo Executando o container...

docker run -p 8080:8080 finan-app
pause