# update the base image
docker pull mcr.microsoft.com/dotnet/aspnet:8.0 || exit 1
docker pull mcr.microsoft.com/dotnet/sdk:8.0 || exit 1

# build
docker build `
	--file '.\HomeAutomation.Website\Dockerfile' `
	--secret "id=ca_crt,src=${env:userprofile}\.aspnet\https\ca.crt" `
	--tag 'eassbhhtgu/homeautomation-website:latest' `
	.

# build
docker build `
	--file '.\HomeAutomation.WebApplication\Dockerfile' `
	--secret "id=ca_crt,src=${env:userprofile}\.aspnet\https\ca.crt" `
	--tag 'eassbhhtgu/homeautomation-api:latest' `
	.
