namespace NineDigit.WooCommerce;

public static class DateTimeExtensions
{
    public static string ToIso8601String(this DateTime dateTime)
        => dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
}