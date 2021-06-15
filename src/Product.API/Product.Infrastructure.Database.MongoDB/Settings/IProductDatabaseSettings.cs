namespace Product.Infrastructure.Database.MongoDB.Settings
{
    /// <summary>
    /// Defines the product infrastructure database settings.
    /// </summary>
    public interface IProductDatabaseSettings
    {
        /// <summary>
        /// Gets or sets the database connection string.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        string DatabaseName { get; set; }
    }
}
