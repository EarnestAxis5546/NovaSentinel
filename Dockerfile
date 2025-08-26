# Use official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY src/NovaSentinel.sln .
COPY src/NovaSentinel/NovaSentinel.csproj src/NovaSentinel/
RUN dotnet restore
COPY src/NovaSentinel/ src/NovaSentinel/
RUN dotnet publish src/NovaSentinel -c Release -o out

# Use runtime image for deployment
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
COPY config/envoy.yaml /etc/envoy/
COPY config/redis.conf /etc/redis/
RUN apt-get update && apt-get install -y redis-server envoy
EXPOSE 8080
ENTRYPOINT ["dotnet", "NovaSentinel.dll"]