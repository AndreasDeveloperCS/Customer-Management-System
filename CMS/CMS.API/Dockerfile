#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CMS.API/CMS.API.csproj", "CMS.API/"]
RUN dotnet restore "CMS.API/CMS.API.csproj"
COPY . .
WORKDIR "/src/CMS.API"
RUN dotnet build "CMS.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CMS.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CMS.API.dll"]