using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Stock.Domain.Models;

namespace Stock.Infrastructure.Database.MongoDb
{
    /// <summary>
    /// Defines MongoDB database models mapping.
    /// </summary>
    internal class DatabaseMapping
    {
        /// <summary>
        /// Register mappings into <see cref="BsonClassMap"/>.
        /// </summary>
        public static void RegisterMapping()
        {
            if (BsonClassMap.GetRegisteredClassMaps().Count() > 0)
            {
                return;
            }

            BsonClassMap.RegisterClassMap<StockModel>(map =>
            {
                map.AutoMap();
                map.MapIdMember(field => field.ProductId).SetSerializer(new GuidSerializer(BsonType.String));
            });

            BsonClassMap.RegisterClassMap<StockHistoryModel>(map =>
            {
                map.AutoMap();
                map.MapIdMember(field => field.Id).SetSerializer(new GuidSerializer(BsonType.String));
                map.MapField(field => field.ProductId).SetSerializer(new GuidSerializer(BsonType.String));
                map.MapField(field => field.DateTimeUtc).SetSerializer(new DateTimeOffsetSerializer(BsonType.String));
            });
        }
    }
}
