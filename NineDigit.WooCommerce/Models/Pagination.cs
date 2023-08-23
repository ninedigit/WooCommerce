namespace NineDigit.WooCommerce;

public record Pagination
{
    public const int MaxTakeValue = 100;

    /// <summary>
    /// Number of elements to skip
    /// </summary>
    public int? Skip { get; init; }
    /// <summary>
    /// Number of elements to take
    /// </summary>
    public int? Take { get; init; }

    /// <summary>
    /// Creates pagination that skips no elements and takes specified number of elements.
    /// </summary>
    /// <param name="take">Elements to take</param>
    /// <returns></returns>
    public static Pagination TakeFirst(int? take = 1) => new() { Skip = 0, Take = take };
}