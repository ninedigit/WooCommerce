using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace NineDigit.WooCommerce;

internal static class HttpWebResponseExtensions
{
    const string XWpTotalHeaderName = "X-WP-Total";
    public static int GetXWpTotalHeaderValue(this HttpWebResponse response)
    {
        if (!response.TryGetXWpTotalHeaderValue(out int? total))
            throw new InvalidOperationException($"The '{XWpTotalHeaderName}' header cannot be processed.");

        return total.Value;
    }

    public static bool TryGetXWpTotalHeaderValue(this HttpWebResponse response, [NotNullWhen(true)] out int? total)
    {
        total = default;

        if (response.Headers.AllKeys.Contains(XWpTotalHeaderName))
        {
            var xWpTotalHeaderValue = response.Headers[XWpTotalHeaderName];
            if (int.TryParse(xWpTotalHeaderValue, out int xWpTotal))
            {
                total = xWpTotal;
                return true;
            }
        }

        return false;
    }
}
