using System.ComponentModel;

namespace NineDigit.WooCommerce;

public enum OrderSortingProperty
{
    [Description("date")]
    Date,
    [Description("id")]
    Id,
    [Description("include")]
    Include,
    [Description("title")]
    Title,
    [Description("slug")]
    Slug,
}
