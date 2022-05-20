FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["API_FeirasLivresSP.csproj", "./"]
RUN dotnet restore "API_FeirasLivresSP.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "API_FeirasLivresSP.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API_FeirasLivresSP.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API_FeirasLivresSP.dll"]