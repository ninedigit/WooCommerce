using System.ComponentModel;

namespace NineDigit.WooCommerce;

public enum ProductStatus
{
    [Description("any")]
    Any,
    [Description("draft")]
    Draft,
    [Description("pending")]
    Pending,
    [Description("private")]
    Private,
    [Description("publish")]
    Publish
}
