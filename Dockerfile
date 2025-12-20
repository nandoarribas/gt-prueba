# STAGE 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy everything (filtered by the new .dockerignore)
COPY . .

RUN dotnet clean "src/GtMotive.Estimate.Microservice.Host/GtMotive.Estimate.Microservice.Host.csproj"

# Run publish - using absolute paths to avoid confusion
RUN dotnet publish "src/GtMotive.Estimate.Microservice.Host/GtMotive.Estimate.Microservice.Host.csproj" \
    -c Release \
    -o /app/out \
    /p:UseAppHost=false
	
# --- Runtime Stage ---
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copy the output
COPY --from=build /app/out/ ./


EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "GtMotive.Estimate.Microservice.Host.dll"]