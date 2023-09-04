using System.ComponentModel;

namespace NineDigit.WooCommerce;

public enum TaxRatesSortingProperty
{
    [Description("order")]
    Order,
    [Description("id")]
    Id,
    [Description("priority")]
    Priority,
}