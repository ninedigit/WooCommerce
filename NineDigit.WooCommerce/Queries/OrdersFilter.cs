namespace NineDigit.WooCommerce;

public sealed class OrdersFilter : FilterBase
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
    public OrderStatus? Status { get; set; }
    /// <summary>
    /// Limit result set to orders assigned a specific customer.
    /// </summary>
    public int? CustomerId { get; set; }
    /// <summary>
    /// Limit result set to orders assigned a specific product.
    /// </summary>
    public int? ProductId { get; set; }
    /// <summary>
    /// Number of decimal points to use in each resource. Default is 2.
    /// </summary>
    public int? DecimalPoints { get; set; }

    public override void BindTo(QueryParameterBuilder builder)
        => builder
            .SetIfNotNull("search", this.Search)
            .SetIfNotNull("after", this.PublishedAfter)
            .SetIfNotNull("before", this.PublishedBefore)
            .SetIfNotNull("modified_after", this.ModifiedAfter)
            .SetIfNotNull("modified_before", this.ModifiedBefore)
            .SetIfNotNull("dates_are_gmt", this.DatesAreGmt)
            .SetIfNotNull("status", this.Status)
            .SetIfNotNull("customer", this.CustomerId)
            .SetIfNotNull("product", this.ProductId)
            .SetIfNotNull("dp", this.DecimalPoints);
}
