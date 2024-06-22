FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY /Park20.Backoffice.Api/sql/db.sql /App/sql/db.sql
COPY /Park20.Backoffice.Api/certificate.pfx /App/
COPY --from=build-env /App/out .

ENTRYPOINT ["dotnet", "Park20.Backoffice.Api.dll"]