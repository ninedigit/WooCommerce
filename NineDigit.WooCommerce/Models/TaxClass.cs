using System.ComponentModel;

namespace NineDigit.WooCommerce;

public enum TaxClass
{
    [Description("standard")]
    Standard,
    [Description("reduced-rate")]
    ReducedRate,
    [Description("zero-rate")]
    ZeroRate,
}
