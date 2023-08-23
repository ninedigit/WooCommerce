namespace NineDigit.WooCommerce;

public sealed class ProductFilter : FilterBase
{
    /// <summary>
    /// Limit results to those matching a string.
    /// </summary>
    public string? Search { get; set; }
    /// <summary>
    /// Limit response to resources published after a given date.
    /// </summary>
    public DateTime? PublishedAfter { get; set; }
    /// <summary>
    /// Limit response to resources published before a given date.
    /// </summary>
    public DateTime? PublishedBefore { get; set; }
    /// <summary>
    /// Limit response to resources modified after a given date.
    /// </summary>
    public DateTime? ModifiedAfter { get; set; }
    /// <summary>
    /// Limit response to resources modified before a given date.
    /// </summary>
    public DateTime? ModifiedBefore { get; set; }
    /// <summary>
    /// Whether to consider GMT post dates when limiting response by published or modified date.
    /// </summary>
    public bool? DatesAreGmt { get; set; }
    /// <summary>
    /// Limit result set to products assigned a specific status.
    /// </summary>
    public ProductStatus? Status { get; set; }
    /// <summary>
    /// Limit result set to products with a specific SKU.
    /// </summary>
    public string? Sku { get; set; }
    /// <summary>
    /// Limit result set to featured products.
    /// </summary>
    public bool? IsFeatured { get; set; }
    /// <summary>
    /// Limit result set to products assigned a specific category ID.
    /// </summary>
    public int? CategoryId { get; set; }
    /// <summary>
    /// Limit result set to products assigned a specific tag ID.
    /// </summary>
    public int? TagId { get; set; }
    /// <summary>
    /// Limit result set to products assigned a specific shipping class ID.
    /// </summary>
    public int? ShippingClassId { get; set; }
    /// <summary>
    /// Limit result set to products with a specific tax class.
    /// </summary>
    public TaxClass? TaxClass { get; set; }
    /// <summary>
    /// Limit result set to products on sale.
    /// </summary>
    public bool? IsOnSale { get; set; }
    /// <summary>
    /// Limit result set to products based on a minimum price.
    /// </summary>
    public decimal? MinPrice { get; set; }
    /// <summary>
    /// Limit result set to products based on a maximum price.
    /// </summary>
    public decimal? MaxPrice { get; set; }
    /// <summary>
    /// Limit result set to products with specified stock status.
    /// </summary>
    public StockStatus? StockStatus { get; set; }

    public override void BindTo(QueryParameterBuilder builder)
        => builder
            .SetIfNotNull("search", this.Search)
            .SetIfNotNull("after", this.PublishedAfter)
            .SetIfNotNull("before", this.PublishedBefore)
            .SetIfNotNull("modified_after", this.ModifiedAfter)
            .SetIfNotNull("modified_before", this.ModifiedBefore)
            .SetIfNotNull("dates_are_gmt", this.DatesAreGmt)
            .SetIfNotNull("status", this.Status)
            .SetIfNotNull("sku", this.Sku)
            .SetIfNotNull("featured", this.IsFeatured)
            .SetIfNotNull("category", this.CategoryId)
            .SetIfNotNull("tag", this.TagId)
            .SetIfNotNull("shipping_class", this.ShippingClassId)
            .SetIfNotNull("tax_class", this.TaxClass)
            .SetIfNotNull("on_sale", this.IsOnSale)
            .SetIfNotNull("min_price", this.MinPrice)
            .SetIfNotNull("max_price", this.MaxPrice)
            .SetIfNotNull("stock_status", this.StockStatus);
}