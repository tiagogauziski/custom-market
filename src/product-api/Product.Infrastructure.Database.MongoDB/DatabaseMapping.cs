using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Product.Models;

namespace Product.Infrastructure.Database.MongoDB
{
    /// <summary>
    /// Defines database models mapping.
    /// </summary>
    public static class DatabaseMapping
    {
        /// <summary>
        /// Register mappings into <see cref="BsonClassMap"/>.
        /// </summary>
        public static void RegisterMapping()
        {
            BsonClassMap.RegisterClassMap<Models.Product>(map =>
            {
                map.AutoMap();
                map.MapIdMember(field => field.Id).SetSerializer(new GuidSerializer(BsonType.String));
            });

            BsonClassMap.RegisterClassMap<ProductHistory>(map =>
            {
                map.AutoMap();
                map.MapIdMember(field => field.Id).SetSerializer(new GuidSerializer(BsonType.String));
                map.MapField(field => field.ProductId).SetSerializer(new GuidSerializer(BsonType.String));
                map.MapField(field => field.DateTimeUtc).SetSerializer(new DateTimeOffsetSerializer(BsonType.String));
            });
        }
    }
}
