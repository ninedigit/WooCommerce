namespace NineDigit.WooCommerce;

public abstract class FilterBase : IQueryParametersBindable
{
    public abstract void BindTo(QueryParameterBuilder builder);
}
