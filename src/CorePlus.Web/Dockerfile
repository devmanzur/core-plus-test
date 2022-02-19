FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CorePlus.Web/CorePlus.Web.csproj", "CorePlus.Web/"]
COPY ["CorePlus.Modules/CorePlus.Modules.csproj", "CorePlus.Modules/"]
RUN dotnet restore "CorePlus.Web/CorePlus.Web.csproj"
COPY . .
WORKDIR "/src/CorePlus.Web"
RUN dotnet build "CorePlus.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CorePlus.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CorePlus.Web.dll"]
