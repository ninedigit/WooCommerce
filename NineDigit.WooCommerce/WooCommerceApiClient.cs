using CommunityToolkit.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Net;
using WooCommerceNET;
using WooCommerceNET.WooCommerce.v3;

namespace NineDigit.WooCommerce;

public sealed class WooCommerceApiClient : IDisposable
{
    HttpWebResponse? lastResponse;
    
    readonly SemaphoreSlim clientLock = new(1, 1);
    readonly ILogger logger;

    public WooCommerceApiClient(
        WooCommerceApiClientOptions options,
        ILoggerFactory loggerFactory)
    {
        Guard.IsNotNull(options);
        Guard.IsNotNull(loggerFactory);
        
        this.logger = loggerFactory.CreateLogger<WooCommerceApiClient>();

        var restApi = new RestAPI(
            url: options.Url.ToString(),
            key: options.PublicKey,
            secret: options.PrivateKey,
            requestFilter: this.OnWooCommerceRequest,
            responseFilter: this.OnWooCommerceResponse);

        this.Client = new WCObject(restApi);
    }

    /// <summary>
    /// The wrapped client
    /// </summary>
    public WCObject Client { get; }

    #region Event handlers
    private void OnWooCommerceRequest(HttpWebRequest request)
        => this.logger.LogDebug("Sending {httpMethod} request to {uri}", request.Method, request.RequestUri);

    private void OnWooCommerceResponse(HttpWebResponse response)
    {
        this.logger.LogDebug("Received response with http status {statusCode}", response.StatusCode);
        this.lastResponse = response;
    }
    #endregion

    /// <summary>
    /// Gets products based on query.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Task<PaginatedResult<Product>> GetProductsAsync(ProductsQuery? query, CancellationToken cancellationToken)
        => ExecuteAsync(async () =>
        {
            var queryParameters = new QueryParameterBuilder()
                .Apply(query)
                .Build();

            var items = await this.Client.Product.GetAll(queryParameters);

            if (this.lastResponse is null)
                throw new InvalidOperationException("Last response expected not to be null.");

            var totalCount = this.lastResponse.GetXWpTotalHeaderValue();

            return new PaginatedResult<Product>(Items: items, TotalCount: totalCount);
        }, cancellationToken);

    /// <summary>
    /// Gets product by ID.
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Product> GetProductAsync(ulong id, CancellationToken cancellationToken)
        => ExecuteAsync(() => this.Client.Product.Get(id), cancellationToken);

    /// <summary>
    /// Updates all properties of product.
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="product">The product to update.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task UpdateProductAsync(ulong id, Product product, CancellationToken cancellationToken)
        => ExecuteAsync(() => this.Client.Product.Update(id, product), cancellationToken);

    /// <summary>
    /// Updates only non-null properties of product.
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="product">Partially initialized product.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task PatchProductAsync(ulong id, Product product, CancellationToken cancellationToken)
        => ExecuteAsync(() => this.Client.Product.UpdateWithNull(id, product), cancellationToken);

    /// <summary>
    /// Gets orders based on query.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Task<PaginatedResult<Order>> GetOrdersAsync(OrdersQuery? query, CancellationToken cancellationToken)
        => ExecuteAsync(async () =>
        {
            var queryParameters = new QueryParameterBuilder()
                .Apply(query)
                .Build();

            var items = await this.Client.Order.GetAll(queryParameters);

            if (this.lastResponse is null)
                throw new InvalidOperationException("Last response expected not to be null.");

            var totalCount = this.lastResponse.GetXWpTotalHeaderValue();

            return new PaginatedResult<Order>(Items: items, TotalCount: totalCount);
        }, cancellationToken);

    #region Helpers
    private async Task<T> ExecuteAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken)
    {
        await this.clientLock.WaitAsync(cancellationToken);

        try
        {
            return await action();
        }
        finally
        {
            this.clientLock.Release();
        }
    }
    #endregion
    
    #region IDisposable
    private bool disposedValue;

    private void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                this.clientLock.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
