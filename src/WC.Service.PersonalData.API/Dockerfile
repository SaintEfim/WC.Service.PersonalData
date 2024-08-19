FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/WC.Service.PersonalData.API/WC.Service.PersonalData.API.csproj src/WC.Service.PersonalData.API/
COPY src/WC.Service.PersonalData.Domain/WC.Service.PersonalData.Domain.csproj src/WC.Service.PersonalData.Domain/
COPY src/WC.Service.PersonalData.Data.Abstractions/WC.Service.PersonalData.Data.Abstractions.csproj src/WC.Service.PersonalData.Data.Abstractions/
COPY src/WC.Service.PersonalData.Data.PostgreSql/WC.Service.PersonalData.Data.PostgreSql.csproj src/WC.Service.PersonalData.Data.PostgreSql/
COPY src/WC.Service.PersonalData.Data/WC.Service.PersonalData.Data.csproj src/WC.Service.PersonalData.Data/
COPY src/WC.Service.PersonalData.Domain.Abstractions/WC.Service.PersonalData.Domain.Abstractions.csproj src/WC.Service.PersonalData.Domain.Abstractions/

RUN dotnet restore src/WC.Service.PersonalData.API/WC.Service.PersonalData.API.csproj

COPY src/ .
WORKDIR /src/WC.Service.PersonalData.API
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "WC.Service.PersonalData.API.dll"]
