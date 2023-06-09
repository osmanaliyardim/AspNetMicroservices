#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Identity/IdentityServer/IdentityServer.csproj", "Services/Identity/IdentityServer/"]
COPY ["BuildingBlocks/Common.Logging/Common.Logging.csproj", "BuildingBlocks/Common.Logging/"]
RUN dotnet restore "Services/Identity/IdentityServer/IdentityServer.csproj"
COPY . .
WORKDIR "/src/Services/Identity/IdentityServer"
RUN dotnet build "IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityServer.dll"]