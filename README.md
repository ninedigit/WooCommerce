# NineDigit WooCommerce

[![NuGet version (NineDigit.WooCommerce)](https://img.shields.io/nuget/v/NineDigit.WooCommerce)](https://www.nuget.org/packages/NineDigit.WooCommerce/)

Wrapper for [WooCommerceNET](https://github.com/XiaoFaye/WooCommerce.NET) library.

## Quick start

```csharp
// Create client options
WooCommerceApiClientOptions clientOpts = new WooCommerceApiClientOptions(
    new Uri("https://your-woocommerce-page.sk/wp-json/wc/v3"),
    "your_public_key_here",
    "your_private_key_here");

// Instantiate client
WooCommerceApiClient client = new WooCommerceApiClient(clientOpts, NullLoggerFactory.Instance);

// Get orders
OrdersQuery ordersQuery = new OrdersQuery
{
    Pagination = Pagination.TakeFirst(5),
    SortBy = OrderSortingProperty.Date,
    SortDirection = SortDirection.Descending,
    Filter = new()
    {
        PublishedAfter = DateTime.Today.AddDays(-3)
    }
};

PaginatedResult<Order> orders = await client.GetOrdersAsync(ordersQuery, CancellationToken.None);

// Get products
ProductsQuery productsQuery = new ProductsQuery
{
    Pagination = Pagination.TakeFirst(10),
    SortBy = ProductSortingProperty.Id,
    SortDirection = SortDirection.Ascending,
    Filter = new()
    {
        Status = ProductStatus.Private
    }
};

PaginatedResult<Product> products = await client.GetProductsAsync(productsQuery, CancellationToken.None);
```

