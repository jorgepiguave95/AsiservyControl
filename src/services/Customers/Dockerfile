# ===== Build =====
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Contracts/Contracts.csproj Contracts/
COPY Accounts/Accounts.csproj Accounts/

RUN dotnet restore Accounts/Accounts.csproj

COPY . .
RUN dotnet publish Accounts/Accounts.csproj -c Release -o /app/publish

# ===== Runtime =====
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet","Accounts.dll"]
