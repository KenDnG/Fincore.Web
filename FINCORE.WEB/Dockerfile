#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

ARG BUILD_COMPANY_ID_ENVIRONMENT
ARG BUILD_COMPANY_NAME_ENVIRONMENT
ENV COMPANY_ID_ENVIRONMENT=$BUILD_COMPANY_ID_ENVIRONMENT
ENV COMPANY_NAME_ENVIRONMENT=$BUILD_COMPANY_NAME_ENVIRONMENT

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FINCORE.WEB/FINCORE.WEB.csproj", "FINCORE.WEB/"]
RUN dotnet restore "FINCORE.WEB/FINCORE.WEB.csproj"
COPY . .
WORKDIR "/src/FINCORE.WEB"
RUN dotnet build "FINCORE.WEB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FINCORE.WEB.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FINCORE.WEB.dll"]

