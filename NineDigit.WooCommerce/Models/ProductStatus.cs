using System.ComponentModel;

namespace NineDigit.WooCommerce;

/// <summary>
/// Product status is a way of indicating whether a product is available for purchase or not.
/// </summary>
public enum ProductStatus
{
    [Description("any")]
    Any,
    /// <summary>
    /// The product is not yet available for purchase, but it is in development.
    /// </summary>
    [Description("draft")]
    Draft,
    /// <summary>
    /// This means the product is awaiting approval from a WooCommerce administrator.
    /// </summary>
    [Description("pending")]
    Pending,
    /// <summary>
    /// This means the product is only available for purchase by certain users (e.g., members of a private club).
    /// </summary>
    [Description("private")]
    Private,
    /// <summary>
    /// The product is available for purchase.
    /// </summary>
    [Description("publish")]
    Published
}
