# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS base
WORKDIR /code


#RUN dotnet tool install --global -dotnet-ef
FROM base AS build

# copy csproj and restore as distinct layers
COPY src/Sophia.Api.Api/*.csproj ./src/Sophia.Api/
RUN dotnet restore src/Sophia.Api/Sophia.Api.csproj

# copy everything else and build appp
COPY src/Sophia.Api/. ./src/Sophia.Api/
WORKDIR /code/src/Sophia.Api
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
ENV ASPNETCORE_URLS="http://+:80"
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Sophia.Api.dll"]