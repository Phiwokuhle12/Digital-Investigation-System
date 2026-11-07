
# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["DigitalInvestigationAPI.csproj", "./"]

RUN dotnet restore "DigitalInvestigationAPI.csproj"

COPY . .

RUN dotnet publish "DigitalInvestigationAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_HTTP_PORTS=10000

EXPOSE 10000

ENTRYPOINT ["dotnet", "DigitalInvestigationAPI.dll"]
