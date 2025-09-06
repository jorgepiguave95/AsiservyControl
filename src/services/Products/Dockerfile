# ===== Build =====
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Contracts/Contracts.csproj Contracts/
COPY services/Products/Products.Domain/Products.Domain.csproj services/Products/Products.Domain/
COPY services/Products/Products.Aplication/Products.Aplication.csproj services/Products/Products.Aplication/
COPY services/Products/Products.Infraestructure/Products.Infraestructure.csproj services/Products/Products.Infraestructure/
COPY services/Products/Products.Api/Products.Api.csproj services/Products/Products.Api/

RUN dotnet restore services/Products/Products.Api/Products.Api.csproj

COPY . .
RUN dotnet publish services/Products/Products.Api/Products.Api.csproj -c Release -o /app/publish

# ===== Runtime =====
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet","Products.Api.dll"]
