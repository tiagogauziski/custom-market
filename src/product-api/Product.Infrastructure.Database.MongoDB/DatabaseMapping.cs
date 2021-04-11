using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;

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
                map.MapIdMember(field => field.Id);
            });
        }
    }
}
