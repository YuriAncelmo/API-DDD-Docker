#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DDDWebAPI.Presentation/DDDWebAPI.Presentation.csproj", "DDDWebAPI.Presentation/"]
COPY ["DDDWebAPI.Application.DTO/DDDWebAPI.Application.DTO.csproj", "DDDWebAPI.Application.DTO/"]
COPY ["DDDWebAPI.Domain.Core/DDDWebAPI.Domain.Core.csproj", "DDDWebAPI.Domain.Core/"]
COPY ["DDDWebAPI.Domain/DDDWebAPI.Domain.csproj", "DDDWebAPI.Domain/"]
COPY ["DDDWebAPI.Infrastruture.CrossCutting.IOC/DDDWebAPI.Infrastruture.CrossCutting.IOC.csproj", "DDDWebAPI.Infrastruture.CrossCutting.IOC/"]
COPY ["DDDWebAPI.Domain.Services/DDDWebAPI.Domain.Services.csproj", "DDDWebAPI.Domain.Services/"]
COPY ["DDDWebAPI.Infrastruture.Repository/DDDWebAPI.Infrastruture.Repository.csproj", "DDDWebAPI.Infrastruture.Repository/"]
COPY ["DDDWebAPI.Infrastructure.Data/DDDWebAPI.Infrastructure.Data.csproj", "DDDWebAPI.Infrastructure.Data/"]
COPY ["DDDWebAPI.Application/DDDWebAPI.Application.csproj", "DDDWebAPI.Application/"]
COPY ["DDDWebAPI.Infrastruture.CrossCutting.Adapter/DDDWebAPI.Infrastruture.CrossCutting.Adapter.csproj", "DDDWebAPI.Infrastruture.CrossCutting.Adapter/"]
RUN dotnet restore "DDDWebAPI.Presentation/DDDWebAPI.Presentation.csproj"
COPY . .
WORKDIR "/src/DDDWebAPI.Presentation"
RUN dotnet build "DDDWebAPI.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DDDWebAPI.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DDDWebAPI.Presentation.dll"]