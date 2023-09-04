namespace NineDigit.WooCommerce;

public sealed class TaxRatesFilter : FilterBase
{
    /// <summary>
    /// Retrieve only tax rates of this Tax class.
    /// </summary>
    public string? Class { get; set; }

    public override void BindTo(QueryParameterBuilder builder)
        => builder
            .SetIfNotNull("class", this.Class);
}
