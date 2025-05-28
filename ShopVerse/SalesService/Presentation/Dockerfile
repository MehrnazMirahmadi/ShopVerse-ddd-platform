# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SalesService/Presentation/Presentation.csproj", "SalesService/Presentation/"]
COPY ["ShopVerse.BuildingBlocks.Messaging/ShopVerse.BuildingBlocks.Messaging/ShopVerse.BuildingBlocks.Messaging.csproj", "ShopVerse.BuildingBlocks.Messaging/ShopVerse.BuildingBlocks.Messaging/"]
COPY ["ShopVerse.BuildingBlocks/ShopVerse.BuildingBlocks.csproj", "ShopVerse.BuildingBlocks/"]
COPY ["SalesService/Application/Application.csproj", "SalesService/Application/"]
COPY ["SalesService/Domain/Domain.csproj", "SalesService/Domain/"]
COPY ["SalesService/Infrastructure/Infrastructure.csproj", "SalesService/Infrastructure/"]
RUN dotnet restore "./SalesService/Presentation/Presentation.csproj"
COPY . .
WORKDIR "/src/SalesService/Presentation"
RUN dotnet build "./Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.dll"]