FROM mcr.microsoft.com/dotnet/sdk:8.0 as build-env

RUN --mount=type=secret,id=ca_crt,dst=/usr/local/share/ca-certificates/ca.crt \
	/usr/sbin/update-ca-certificates

WORKDIR /app
COPY . .
RUN dotnet restore --nologo --source https://api.nuget.org/v3/index.json --source https://nuget/v3/index.json --verbosity minimal
RUN dotnet publish ./HomeAutomation.WebApplication/HomeAutomation.WebApplication.csproj --configuration Release --nologo --no-restore --output /app/publish --verbosity minimal

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 443/tcp
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "./HomeAutomation.WebApplication.dll"]
