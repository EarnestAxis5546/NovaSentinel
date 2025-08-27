# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY src/NovaSentinel.sln .
COPY src/NovaSentinel/NovaSentinel.csproj src/NovaSentinel/
COPY src/NovaSentinel.Tests/NovaSentinel.Tests.csproj src/NovaSentinel.Tests/
RUN dotnet restore
COPY src/ src/
RUN dotnet publish src/NovaSentinel -c Release -o out --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
COPY config/envoy.yaml /etc/envoy/
COPY config/redis.conf /etc/redis/
COPY lua/ /app/lua/
RUN apt-get update && apt-get install -y redis-server envoy bpfcc-tools
EXPOSE 8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "NovaSentinel.dll"]