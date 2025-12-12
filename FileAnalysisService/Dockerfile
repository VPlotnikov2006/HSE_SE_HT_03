FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY "./FileAnalysis.API/*.csproj" "./FileAnalysis.API/"
COPY "./FileAnalysis.Application/*.csproj" "./FileAnalysis.Application/"
COPY "./FileAnalysis.Infrastructure/*.csproj" "./FileAnalysis.Infrastructure/"
COPY "./FileAnalysis.Domain/*.csproj" "./FileAnalysis.Domain/"

RUN dotnet restore "./FileAnalysis.API/FileAnalysis.API.csproj"

COPY . .

RUN dotnet publish "FileAnalysis.API/FileAnalysis.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FileAnalysis.API.dll"]
