#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MT.E-Sourcing.Products/MT.E-Sourcing.Products.csproj", "MT.E-Sourcing.Products/"]
COPY ["MT.E-Sourcing.Data/MT.E-Sourcing.Data.csproj", "MT.E-Sourcing.Data/"]
COPY ["MT.E-Sourcing.Core/MT.E-Sourcing.Core.csproj", "MT.E-Sourcing.Core/"]
RUN dotnet restore "MT.E-Sourcing.Products/MT.E-Sourcing.Products.csproj"
COPY . .
WORKDIR "/src/MT.E-Sourcing.Products"
RUN dotnet build "MT.E-Sourcing.Products.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MT.E-Sourcing.Products.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MT.E-Sourcing.Products.dll"]
