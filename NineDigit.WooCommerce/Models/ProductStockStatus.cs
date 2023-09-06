namespace NineDigit.WooCommerce;

/// <summary>
/// The stock status of the product.
/// </summary>
public sealed class ProductStockStatus
{
    readonly string value;

    private ProductStockStatus(string value)
    {
        this.value = value;
    }
    
    /// <summary>
    /// In stock.
    /// </summary>
    public static ProductStockStatus InStock { get; } = new ProductStockStatus("instock");
    /// <summary>
    /// Out of stock.
    /// </summary>
    public static ProductStockStatus OutOfStock { get; } = new ProductStockStatus("outofstock");
    /// <summary>
    /// On back order
    /// </summary>
    public static ProductStockStatus OnBackOrder { get; } = new ProductStockStatus("onbackorder");

    public static implicit operator string(ProductStockStatus status)
        => status.value;

    public override string ToString()
        => this.value;
}