namespace Stock.Infrastructure.Database.MongoDb.Settings
{
    /// <summary>
    /// Defines the stock infrastructure database settings.
    /// </summary>
    public interface IStockDatabaseSettings
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
