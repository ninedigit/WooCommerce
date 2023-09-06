using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using NineDigit.WooCommerce;
using WooCommerceNET.WooCommerce.v3;

// Load the sensitive data from the 'appsettings.json' configuration file.
var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var url = configuration["url"];
var publicKey = configuration["apiKeys:publicKey"];
var privateKey = configuration["apiKeys:privateKey"];

// Create client options
var clientOpts = new WooCommerceApiClientOptions(new Uri(url), publicKey, privateKey);

// Instantiate client
var client = new WooCommerceApiClient(clientOpts, NullLoggerFactory.Instance);

// Get tax rates
var taxRatesQuery = new TaxRatesQuery
{
    Pagination = Pagination.TakeFirst(10),
};
var taxRates = await client.GetTaxRatesAsync(taxRatesQuery, CancellationToken.None);
Console.Write($"{taxRates.Count}/{taxRates.TotalCount} tax rate(s) received.");

// Get orders
var ordersQuery = new OrdersQuery
{
    Pagination = Pagination.TakeFirst(5),
    SortBy = OrderSortingProperty.Date,
    SortDirection = SortDirection.Descending,
    Filter = new()
    {
        PublishedAfter = DateTime.Today.AddDays(-3)
    }
};
var orders = await client.GetOrdersAsync(ordersQuery, CancellationToken.None);
Console.Write($"{orders.Count}/{orders.TotalCount} orders(s) received.");

// Get products
var productsQuery = new ProductsQuery
{
    Pagination = Pagination.TakeFirst(10),
    SortBy = ProductSortingProperty.Id,
    SortDirection = SortDirection.Ascending,
    Filter = new()
    {
        Status = ProductStatus.Private
    }
};
var products = await client.GetProductsAsync(productsQuery, CancellationToken.None);
Console.Write($"{products.Count}/{products.TotalCount} product(s) received.");

// Get single product by ID
var productId = 12660u;
var product = await client.GetProductAsync(productId, CancellationToken.None);
Console.Write($"Product stock quantity: {product.stock_quantity}");

// Patch product
var partialProduct = new Product()
{
    manage_stock = true,
    stock_quantity = product.stock_quantity + 1,
    stock_status = ProductStockStatus.InStock
};

var productAfter = await client.UpdateProductAsync(productId, partialProduct, CancellationToken.None);
Console.Write($"New product stock quantity: {productAfter.stock_quantity}");
