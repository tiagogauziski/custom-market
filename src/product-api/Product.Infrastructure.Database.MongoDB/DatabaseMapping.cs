using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

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
        }
    }
}
