#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Votus.WebApi/Votus.WebApi.csproj", "Votus.WebApi/"]
RUN dotnet restore "Votus.WebApi/Votus.WebApi.csproj"
COPY . .
WORKDIR "/src/Votus.WebApi"
RUN dotnet build "Votus.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Votus.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Votus.WebApi.dll"]