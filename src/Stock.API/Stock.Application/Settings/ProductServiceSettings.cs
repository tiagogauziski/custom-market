using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace Stock.Application.Settings
{
    /// <summary>
    /// Default implementation of <see cref="IProductServiceSettings"/>.
    /// </summary>
    public class ProductServiceSettings : IProductServiceSettings,
        IOptions<IProductServiceSettings>
    {
        /// <inheritdoc />
        [Required]
        public string BaseUrl { get; set; }

        /// <inheritdoc />
        public IProductServiceSettings Value => this;
    }
}
