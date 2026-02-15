# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS base
WORKDIR /code


RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

FROM base AS build

# copy csproj and restore as distinct layers
COPY Sophia.sln ./
COPY src/Sophia.Api/*.csproj ./src/Sophia.Api/
COPY src/Sophia.Domain/*.csproj ./src/Sophia.Domain/
COPY src/Sophia.Infrastructure/*.csproj ./src/Sophia.Infrastructure/
COPY src/Sophia.Db/*.csproj ./src/Sophia.Db/
RUN dotnet restore Sophia.sln

# copy everything else and build app
COPY src/. ./src/
WORKDIR /code/src/Sophia.Api
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
ENV ASPNETCORE_URLS="http://+:80"
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Sophia.Api.dll"]
