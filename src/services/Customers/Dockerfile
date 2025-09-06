# ===== Build =====
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Contracts/Contracts.csproj Contracts/
COPY services/Customers/Customers.Domain/Customers.Domain.csproj services/Customers/Customers.Domain/
COPY services/Customers/Customers.Aplication/Customers.Aplication.csproj services/Customers/Customers.Aplication/
COPY services/Customers/Customers.Infraestructure/Customers.Infraestructure.csproj services/Customers/Customers.Infraestructure/
COPY services/Customers/Customers.Api/Customers.Api.csproj services/Customers/Customers.Api/

RUN dotnet restore services/Customers/Customers.Api/Customers.Api.csproj

COPY . .
RUN dotnet publish services/Customers/Customers.Api/Customers.Api.csproj -c Release -o /app/publish

# ===== Runtime =====
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet","Customers.Api.dll"]
