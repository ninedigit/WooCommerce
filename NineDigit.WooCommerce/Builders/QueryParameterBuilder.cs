using CommunityToolkit.Diagnostics;
using System.Globalization;

namespace NineDigit.WooCommerce;

public sealed class QueryParameterBuilder
{
    readonly Dictionary<string, string> data = new();

    public QueryParameterBuilder SetPagination(Pagination? pagination)
    {
        if (pagination?.Take is not null)
            Guard.IsLessThanOrEqualTo(pagination.Take.Value, Pagination.MaxTakeValue);

        return this
            .SetIfNotNull("offset", pagination?.Skip?.ToString())
            .SetIfNotNull("per_page", pagination?.Take?.ToString());
    }

    public QueryParameterBuilder Apply(IQueryParametersBindable? bindable)
    { 
        bindable?.BindTo(this);
        return this;
    }

    public QueryParameterBuilder SetOrderDirection(SortDirection? orderDirection)
        => SetIfNotNull("order", orderDirection?.GetDescription());

    public QueryParameterBuilder SetOrderBy<TEnum>(TEnum? value) where TEnum : struct, Enum
        => SetOrderBy(value?.GetDescription());

    public QueryParameterBuilder SetOrderBy(string? propertyName)
        => SetIfNotNull("order_by", propertyName);

    public QueryParameterBuilder SetIfNotNull(string key, DateTime? value)
        => SetIfNotNull(key, value?.ToIso8601String());

    public QueryParameterBuilder SetIfNotNull(string key, int? value)
        => SetIfNotNull(key, value?.ToString());

    public QueryParameterBuilder SetIfNotNull(string key, decimal? value)
        => SetIfNotNull(key, value?.ToString(CultureInfo.InvariantCulture));

    public QueryParameterBuilder SetIfNotNull(string key, bool? value)
        => SetIfNotNull(key, value?.ToString());

    public QueryParameterBuilder SetIfNotNull<TEnum>(string key, TEnum? value) where TEnum : struct, Enum
        => SetIfNotNull(key, value?.GetDescription());

    public QueryParameterBuilder SetIfNotNull(string key, string? value)
    {
        if(value is not null)
            this.data[key] = value;
        return this;
    }

    public Dictionary<string, string> Build()
        => new(this.data);
}
