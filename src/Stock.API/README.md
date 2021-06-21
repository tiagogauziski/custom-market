# Product API

## Docker commands to start MongoDB and MongoExpress
1. Create network
```
docker network create mongonetwork
```

1. MongoDB
```
docker run --network mongonetwork --restart unless-stopped --name mongo -p 27017:27017 -d mongo:latest 
```

1. Mongo Express
```
docker run --network mongonetwork --restart unless-stopped --name mongo-express -e ME_CONFIG_MONGODB_SERVER=mongo -p 8081:8081 mongo-express
```

1. Run integration test docker compose
```
docker-compose --file docker-compose-integration.yml up --exit-code-from stock-integration-tests
```
