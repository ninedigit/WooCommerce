﻿using CommunityToolkit.Diagnostics;
using WooCommerceNET.WooCommerce.v3;

namespace NineDigit.WooCommerce;

public static class WooCommerceApiClientExtensions
{
    /// <summary>
    /// Gets all products (executes multiple request and fetches all pages).
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task<IList<Product>> GetAllProductsAsync(this WooCommerceApiClient self, ProductFilter filter, CancellationToken cancellationToken)
    {
        Guard.IsNotNull(filter);

        var query = new ProductsQuery
        {
            SortBy = ProductSortingProperty.Id,
            SortDirection = SortDirection.Ascending,
            Filter = filter,
        };

        var result = new List<Product>();
        var fetchedProductIds = new HashSet<ulong>();
        var skip = 0;
        var pageSize = Pagination.MaxTakeValue;

        while (true)
        {
            query.Pagination = new Pagination() { Skip = skip, Take = pageSize };

            var currentPageProducts = await self.GetProductsAsync(query, cancellationToken);

            if (currentPageProducts.Count == 0)
                break; // No more products to fetch

            foreach (var product in currentPageProducts.Items)
            {
                var productId = product.id ?? throw new InvalidOperationException("Non-null product identifier expected");

                if (!fetchedProductIds.Contains(productId))
                {
                    result.Add(product);
                    fetchedProductIds.Add(productId);
                }
            }

            skip += pageSize;
        }

        return result;
    }

    public static async Task<PaginatedResult<Order>> GetAllOrdersAsync(
        this WooCommerceApiClient self,
        OrdersQuery query,
        CancellationToken cancellationToken = default)
    {
        var originalPagination = query.Pagination ?? new Pagination { Skip = 0, Take = null };

        if (!originalPagination.Take.HasValue)
        {
            originalPagination = new Pagination
            {
                Skip = originalPagination.Skip,
                Take = Pagination.MaxTakeValue
            };
        }

        query = new OrdersQuery
        {
            Pagination = originalPagination,
            SortDirection = query.SortDirection,
            SortBy = query.SortBy,
            Filter = query.Filter
        };

        var paginatedResults = new List<PaginatedResult<Order>>();

        for (var i = 0; ; i++)
        {
            var paginatedResult = await self.GetOrdersAsync(query, cancellationToken).ConfigureAwait(false);

            paginatedResults.Add(paginatedResult);

            if (!query.Pagination.TryGetNextPage(paginatedResult, out var nextPage))
                break;

            query.Pagination = nextPage;
        }

        var mergedItems = paginatedResults.SelectMany(i => i.Items).ToList();
        var result = new PaginatedResult<Order>(mergedItems, paginatedResults.Last().TotalCount);

        return result;
    }

    /// <summary>
    /// Gets all orders (executes multiple request and fetches all pages).
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task<IList<Order>> GetAllOrdersAsync(this WooCommerceApiClient self, OrdersFilter filter, CancellationToken cancellationToken)
    {
        Guard.IsNotNull(filter);

        var query = new OrdersQuery
        {
            SortBy = OrderSortingProperty.Id,
            SortDirection = SortDirection.Ascending,
            Filter = filter,
        };

        var result = new List<Order>();
        var fetchedOrderIds = new HashSet<ulong>();
        var skip = 0;
        var pageSize = Pagination.MaxTakeValue;

        while (true)
        {
            query.Pagination = new Pagination() { Skip = skip, Take = pageSize };

            var currentPageOrders = await self.GetOrdersAsync(query, cancellationToken);

            if (currentPageOrders.Count == 0)
                break; // No more orders to fetch

            foreach (var order in currentPageOrders.Items)
            {
                var orderId = order.id ?? throw new InvalidOperationException("Non-null order identifier expected");

                if (!fetchedOrderIds.Contains(orderId))
                {
                    result.Add(order);
                    fetchedOrderIds.Add(orderId);
                }
            }

            skip += pageSize;
        }

        return result;
    }
}
