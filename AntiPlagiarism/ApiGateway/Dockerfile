FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ApiGateway/ApiGateway.Host/ApiGateway.Host.csproj ApiGateway/ApiGateway.Host/
RUN dotnet restore ApiGateway/ApiGateway.Host/ApiGateway.Host.csproj
COPY ApiGateway/ ApiGateway/
RUN dotnet publish ApiGateway/ApiGateway.Host/ApiGateway.Host.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.Host.dll"]