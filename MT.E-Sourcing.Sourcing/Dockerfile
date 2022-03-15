#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MT.E-Sourcing.Sourcing/MT.E-Sourcing.Sourcing.API.csproj", "MT.E-Sourcing.Sourcing/"]
COPY ["MT.E-Sourcing.Sourcing.Data/MT.E-Sourcing.Sourcing.Data.csproj", "MT.E-Sourcing.Sourcing.Data/"]
COPY ["MT.E-Sourcing.Sourcing.Core/MT.E-Sourcing.Sourcing.Core.csproj", "MT.E-Sourcing.Sourcing.Core/"]
COPY ["MT.E-Sourcing.Sourcing.Service/MT.E-Sourcing.Sourcing.Service.csproj", "MT.E-Sourcing.Sourcing.Service/"]
RUN dotnet restore "MT.E-Sourcing.Sourcing/MT.E-Sourcing.Sourcing.API.csproj"
COPY . .
WORKDIR "/src/MT.E-Sourcing.Sourcing"
RUN dotnet build "MT.E-Sourcing.Sourcing.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MT.E-Sourcing.Sourcing.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MT.E-Sourcing.Sourcing.API.dll"]
