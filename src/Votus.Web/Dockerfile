#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Votus.Web/Votus.Web.csproj", "Votus.Web/"]
RUN dotnet restore "Votus.Web/Votus.Web.csproj"
COPY . .
WORKDIR "/src/Votus.Web"
RUN dotnet build "Votus.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Votus.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Votus.Web.dll"]