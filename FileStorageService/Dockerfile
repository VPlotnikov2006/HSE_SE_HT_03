FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY "./FileStorage.API/*.csproj" "./FileStorage.API/"
COPY "./FileStorage.Application/*.csproj" "./FileStorage.Application/"
COPY "./FileStorage.Infrastructure/*.csproj" "./FileStorage.Infrastructure/"
COPY "./FileStorage.Domain/*.csproj" "./FileStorage.Domain/"

RUN dotnet restore "./FileStorage.API/FileStorage.API.csproj"

COPY . .

RUN dotnet publish "FileStorage.API/FileStorage.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FileStorage.API.dll"]
