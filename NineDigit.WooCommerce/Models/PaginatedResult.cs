namespace NineDigit.WooCommerce;

public record PaginatedResult<T>(IReadOnlyList<T> Items, int TotalCount)
{
    public int Count => Items.Count;
}
