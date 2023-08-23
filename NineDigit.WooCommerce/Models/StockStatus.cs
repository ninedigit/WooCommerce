using System.ComponentModel;

namespace NineDigit.WooCommerce;

public enum StockStatus
{
    [Description("instock")]
    InStock,
    [Description("outofstock")]
    OutOfStock,
    [Description("onbackorder")]
    OnBackOrder
}