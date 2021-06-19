using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using Stock.Application.Product.Contracts.V1;
using Stock.Application.Product.Errors;
using Stock.Application.Settings;

namespace Stock.Application.Product.Service
{
    /// <summary>
    /// Default implementation for <see cref="IProductService"/> responsible to communicate with Product API endpoints.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="httpClient">HTTP client.</param>
        /// <param name="productServiceSettings">Product service settings.</param>
        public ProductService(
            HttpClient httpClient,
            IProductServiceSettings productServiceSettings)
        {
            if (productServiceSettings is null)
            {
                throw new ArgumentNullException(nameof(productServiceSettings));
            }

            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            _httpClient.BaseAddress = new Uri(productServiceSettings.BaseUrl);
        }

        /// <inheritdoc />
        public async Task<Result<ProductResponse>> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;
            try
            {
                response = await _httpClient.GetAsync($"/api/v1/product/{productId}", cancellationToken).ConfigureAwait(false);
            }
            catch (HttpRequestException)
            {
                // TODO: handle the exception better. maybe log it and return error.
                throw;
            }

            if (response.IsSuccessStatusCode)
            {
                return Result.Ok(await response.Content.ReadFromJsonAsync<ProductResponse>(cancellationToken: cancellationToken).ConfigureAwait(false));
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return Result.Fail(new ProductNotFoundError());
            }
            else
            {
                return Result.Fail(new ProductUnexpectedResponseError(response.StatusCode));
            }
        }
    }
}
