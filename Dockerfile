FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/NewsAPI/NewsAPI.csproj", "NewsAPI/"]
COPY ["App.Application/App.Application.csproj", "App.Application/"]
COPY ["App.Domain/App.Domain.csproj", "App.Domain/"]
COPY ["App.Infrastructure/App.Infrastructure.csproj", "App.Infrastructure/"]
COPY ["Services/NewsMapper/Mapper.csproj", "NewsMapper/"]
RUN dotnet restore "NewsAPI/NewsAPI.csproj"
COPY . .
WORKDIR "/src/Services/NewsAPI"
RUN dotnet build "NewsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewsAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsAPI.dll"]