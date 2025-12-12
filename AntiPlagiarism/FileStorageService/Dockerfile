FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY FileStorageService/FileStorageService.Host/FileStorageService.Host.csproj \
     FileStorageService/FileStorageService.Host/

RUN dotnet restore FileStorageService/FileStorageService.Host/FileStorageService.Host.csproj

COPY FileStorageService/ FileStorageService/

RUN dotnet publish FileStorageService/FileStorageService.Host/FileStorageService.Host.csproj \
    -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "FileStorageService.Host.dll"]