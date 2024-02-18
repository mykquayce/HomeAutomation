# update the base image
docker pull eassbhhtgu/homeautomation-api:latest || exit 1
docker pull eassbhhtgu/homeautomation-website:latest || exit 1

# deploy
docker stack deploy --compose-file .\docker-compose.yml homeautomation
