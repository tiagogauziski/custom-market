namespace Stock.Application.Settings
{
    /// <summary>
    /// Defines the product service settings.
    /// </summary>
    public interface IProductServiceSettings
    {
        /// <summary>
        /// Gets or sets Product API base address.
        /// </summary>
        public string BaseUrl { get; set; }
    }
}
