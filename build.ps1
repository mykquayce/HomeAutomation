# update the base image
docker pull mcr.microsoft.com/dotnet/aspnet:8.0 || exit 1
docker pull mcr.microsoft.com/dotnet/sdk:8.0 || exit 1

# build
$secret = 'id=ca_crt,src={0}\.aspnet\https\ca.crt' -f ${env:userprofile}
docker build `
	--build-arg "NuGetServerApiKey=${env:NuGetServerApiKey}" `
	--file .\HomeAutomation.Website\Dockerfile `
	--no-cache `
	--secret $secret `
	--tag eassbhhtgu/homeautomation-website:latest `
	.

# build
$secret = 'id=ca_crt,src={0}\.aspnet\https\ca.crt' -f ${env:userprofile}
docker build `
	--build-arg "NuGetServerApiKey=${env:NuGetServerApiKey}" `
	--file .\HomeAutomation.WebApplication\Dockerfile `
	--no-cache `
	--secret $secret `
	--tag eassbhhtgu/homeautomation-api:latest `
	.

