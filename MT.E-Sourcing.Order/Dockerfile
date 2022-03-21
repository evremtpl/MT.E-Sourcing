#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MT.E-Sourcing.Order/MT.E-Sourcing.Order.csproj", "MT.E-Sourcing.Order/"]
COPY ["MT.E-Sourcing.Order.Application/MT.E-Sourcing.Order.Application.csproj", "MT.E-Sourcing.Order.Application/"]
COPY ["MT.E-Sourcing.Order.Domain/MT.E-Sourcing.Order.Domain.csproj", "MT.E-Sourcing.Order.Domain/"]
COPY ["MT.E-Sourcing.Common/MT.E-Sourcing.Common.csproj", "MT.E-Sourcing.Common/"]
COPY ["MT.E-Sourcing.Order.Infrastructure/MT.E-Sourcing.Order.Infrastructure.csproj", "MT.E-Sourcing.Order.Infrastructure/"]
RUN dotnet restore "MT.E-Sourcing.Order/MT.E-Sourcing.Order.csproj"
COPY . .
WORKDIR "/src/MT.E-Sourcing.Order"
RUN dotnet build "MT.E-Sourcing.Order.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MT.E-Sourcing.Order.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MT.E-Sourcing.Order.dll"]
