FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/NewsAPI/NewsAPI.csproj", "NewsAPI/"]
COPY ["src/App.Application/App.Application.csproj", "App.Application/"]
COPY ["src/App.Domain/App.Domain.csproj", "App.Domain/"]
COPY ["src/App.Infrastructure/App.Infrastructure.csproj", "App.Infrastructure/"]
COPY ["src/Services/NewsMapper/Mapper.csproj", "NewsMapper/"]
RUN dotnet restore "src/NewsAPI/NewsAPI.csproj"
COPY . .
WORKDIR "/src/Services/NewsAPI"
RUN dotnet build "NewsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewsAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsAPI.dll"]