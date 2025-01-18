FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BlazorApp01/BlazorApp01/BlazorApp01.csproj", "BlazorApp01/BlazorApp01/"]
COPY ["BlazorApp01/BlazorApp01.Client/BlazorApp01.Client.csproj", "BlazorApp01/BlazorApp01.Client/"]
RUN dotnet restore "BlazorApp01/BlazorApp01/BlazorApp01.csproj"
COPY . .
WORKDIR "/src/BlazorApp01/BlazorApp01"
RUN dotnet build "BlazorApp01.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BlazorApp01.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorApp01.dll"]
