# ===== Build =====
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Contracts/Contracts.csproj Contracts/
COPY ClientGateway/ClientGateway.csproj ClientGateway/

RUN dotnet restore ClientGateway/ClientGateway.csproj

COPY . .
RUN dotnet publish ClientGateway/ClientGateway.csproj -c Release -o /app/publish

# ===== Runtime =====
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet","ClientGateway.dll"]
