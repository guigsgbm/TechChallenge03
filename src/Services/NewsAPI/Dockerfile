FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/src/Services/NewsAPI/NewsAPI.csproj", "Services/NewsAPI/"]
COPY ["src/App.Application/App.Application.csproj", "App.Application/"]
COPY ["src/App.Domain/App.Domain.csproj", "App.Domain/"]
COPY ["src/Services/NewsMapper/Mapper.csproj", "Services/NewsMapper/"]
COPY ["src/App.Infrastructure/App.Infrastructure.csproj", "App.Infrastructure/"]
RUN dotnet restore "Services/NewsAPI/NewsAPI.csproj"
WORKDIR tests
COPY ["tests/", "."]
RUN dotnet restore "TestsNewsAPI/TestsNewsAPI.csproj"
RUN dotnet build "TestsNewsAPI/TestsNewsAPI.csproj" -c Release -o /app
RUN dotnet test "TestsNewsAPI.csproj" --logger "trx;LogFileName=TestsNewsAPI.trx" 


WORKDIR "/src/Services/NewsAPI"
COPY ["src", "."]
RUN dotnet build "NewsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewsAPI.csproj" -c Release -o /app/publish

FROM base AS final
RUN groupadd --gid 10001 nonroot \
    && useradd --gid 10001 --uid 10001 nonroot
WORKDIR /app
COPY --from=publish /app/publish .
USER nonroot

ENTRYPOINT ["dotnet", "NewsAPI.dll"]