#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RPSSL.WebApi/RPSSL.WebApi.csproj", "RPSSL.WebApi/"]
COPY ["RPSSL.Application/RPSSL.Application.csproj", "RPSSL.Application/"]
COPY ["RPSSL.Domain/RPSSL.Domain.csproj", "RPSSL.Domain/"]
COPY ["RPSSL.Infrastructure/RPSSL.Infrastructure.csproj", "RPSSL.Infrastructure/"]
COPY ["RPSSL.Presentation/RPSSL.Presentation.csproj", "RPSSL.Presentation/"]
RUN dotnet restore "./RPSSL.WebApi/./RPSSL.WebApi.csproj"
COPY . .
WORKDIR "/src/RPSSL.WebApi"
RUN dotnet build "./RPSSL.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RPSSL.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RPSSL.WebApi.dll"]