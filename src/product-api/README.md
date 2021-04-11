# Product API

## Docker commands to start MongoDB and MongoExpress
MongoDB
```
docker run --network mongonetwork --name mongo -p 27017:27017 -d mongo:latest 
```

Mongo Express
```
docker run --network mongonetwork --name mongo-express -e ME_CONFIG_MONGODB_SERVER=mongo -p 8081:8081 mongo-express
```
