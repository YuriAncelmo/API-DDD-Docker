dotnet restore DDDWebAPI.Presentation\DDDWebAPI.Presentation.csproj
dotnet build "DDDWebAPI.Presentation\DDDWebAPI.Presentation.csproj" -c Release -o ./build
dotnet publish "DDDWebAPI.Presentation\DDDWebAPI.Presentation.csproj" -c Release -o ./publish
docker pull mcr.microsoft.com/dotnet/aspnet:6.0
docker pull mcr.microsoft.com/dotnet/sdk:6.0
docker-compose  -f .\docker-compose.yml up
pause