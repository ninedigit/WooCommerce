namespace NineDigit.WooCommerce;

public abstract class QueryBase<TFilter, TSortProperty> : IQueryParametersBindable
    where TFilter : FilterBase, new()
    where TSortProperty : struct, Enum
{
    public TFilter Filter { get; set; } = new();
    /// <summary>
    /// If not specified, up to 10 records are returned.
    /// Must not specify range greater than <see cref="Pagination.MaxTakeValue"/> records.
    /// </summary>
    public Pagination? Pagination { get; set; }
    public TSortProperty? SortBy { get; set; }
    /// <summary>
    /// If not specified, <see cref="SortDirection.Ascending"/> is used.
    /// </summary>
    public SortDirection? SortDirection { get; set; }

    public void BindTo(QueryParameterBuilder builder)
        => builder
            .SetPagination(this?.Pagination)
            .SetOrderBy(this?.SortBy)
            .SetOrderDirection(this?.SortDirection)
            .Apply(this.Filter);
}
