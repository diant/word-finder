#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["WordFinder.Api/WordFinder.Api.csproj", "WordFinder.Api/"]
COPY ["WordFinder.Core/WordFinder.Core.csproj", "WordFinder.Core/"]
RUN dotnet restore "WordFinder.Api/WordFinder.Api.csproj"
COPY . .
WORKDIR "/src/WordFinder.Api"
RUN dotnet build "WordFinder.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WordFinder.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WordFinder.Api.dll"]