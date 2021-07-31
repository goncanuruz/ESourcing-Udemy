#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ESourcing.Products/E-SourcingProducts.csproj", "ESourcing.Products/"]
RUN dotnet restore "ESourcing.Products/E-SourcingProducts.csproj"
COPY . .
WORKDIR "/src/ESourcing.Products"
RUN dotnet build "E-SourcingProducts.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "E-SourcingProducts.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-SourcingProducts.dll"]
