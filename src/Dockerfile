FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY "WordFinder.Web/WordFinder.Web.csproj" "WordFinder.Web/"
COPY "WordFinder.Core/WordFinder.Core.csproj" "WordFinder.Core/"
RUN dotnet restore "WordFinder.Web/WordFinder.Web.csproj"
COPY . .
WORKDIR "/src/WordFinder.Web"
RUN dotnet build "WordFinder.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WordFinder.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WordFinder.Web.dll"]