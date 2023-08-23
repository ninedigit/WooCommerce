using System.ComponentModel;

namespace NineDigit.WooCommerce;

public enum ProductSortingProperty
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
    [Description("price")]
    Price,
    [Description("popularity")]
    Popularity,
    [Description("rating")]
    Rating
}
