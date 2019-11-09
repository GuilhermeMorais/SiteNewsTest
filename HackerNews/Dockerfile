FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["HackerNews.csproj", ""]
RUN dotnet restore "./HackerNews.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "HackerNews.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HackerNews.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HackerNews.dll"]