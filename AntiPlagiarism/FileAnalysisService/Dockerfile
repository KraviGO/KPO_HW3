FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY FileAnalysisService/FileAnalysisService.Host/FileAnalysisService.Host.csproj \
     FileAnalysisService/FileAnalysisService.Host/

RUN dotnet restore FileAnalysisService/FileAnalysisService.Host/FileAnalysisService.Host.csproj

COPY FileAnalysisService/ FileAnalysisService/

RUN dotnet publish FileAnalysisService/FileAnalysisService.Host/FileAnalysisService.Host.csproj \
    -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "FileAnalysisService.Host.dll"]