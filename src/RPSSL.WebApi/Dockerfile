#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/RPSSL.WebApi/RPSSL.WebApi.csproj", "src/RPSSL.WebApi/"]
COPY ["src/RPSSL.Application/RPSSL.Application.csproj", "src/RPSSL.Application/"]
COPY ["src/RPSSL.Domain/RPSSL.Domain.csproj", "src/RPSSL.Domain/"]
COPY ["src/RPSSL.Infrastructure/RPSSL.Infrastructure.csproj", "src/RPSSL.Infrastructure/"]
COPY ["src/RPSSL.Presentation/RPSSL.Presentation.csproj", "src/RPSSL.Presentation/"]
RUN dotnet restore "src/RPSSL.WebApi/RPSSL.WebApi.csproj"
COPY . .
WORKDIR "/src/src/RPSSL.WebApi"
RUN dotnet build "RPSSL.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RPSSL.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RPSSL.WebApi.dll"]
