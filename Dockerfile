FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder
WORKDIR /src

COPY CryoVigil.API/CryoVigil.API.csproj CryoVigil.API/
RUN dotnet restore CryoVigil.API/CryoVigil.API.csproj

COPY . .
WORKDIR /src/CryoVigil.API
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_HTTP_PORTS=10000
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true

COPY --from=builder /app/publish .

EXPOSE 10000

ENTRYPOINT ["dotnet", "CryoVigil.API.dll"]
