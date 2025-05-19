# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
EXPOSE 8080
EXPOSE 8081

# copy csproj files and restore as distinct layers
COPY "OdysseyPortfolio-BE/*.csproj" "OdysseyPortfolio-BE/"
COPY "OdysseyPortfolio-Libraries/*.csproj" "OdysseyPortfolio-Libraries/"
RUN dotnet restore "OdysseyPortfolio-BE/OdysseyPortfolio-BE.csproj"

# copy and build app and libraries
COPY "OdysseyPortfolio-BE/" "OdysseyPortfolio-BE/"
COPY "OdysseyPortfolio-Libraries/" "OdysseyPortfolio-Libraries/"
WORKDIR "/source/OdysseyPortfolio-BE"
RUN dotnet publish -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/nightly/aspnet:8.0-jammy-chiseled-composite
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["./OdysseyPortfolio-BE"]

