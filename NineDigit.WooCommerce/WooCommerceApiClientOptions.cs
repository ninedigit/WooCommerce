using CommunityToolkit.Diagnostics;

namespace NineDigit.WooCommerce;

public sealed class WooCommerceApiClientOptions
{
    public WooCommerceApiClientOptions(Uri url, string publicKey, string privateKey)
    {
        Guard.IsNotNull(url);

        if (!url.IsAbsoluteUri)
            throw new ArgumentException("URL must be an absolute URI.", nameof(url));

        if (url.Scheme != Uri.UriSchemeHttp && url.Scheme != Uri.UriSchemeHttps)
            throw new ArgumentException("URL must have HTTP or HTTPS scheme.", nameof(url));

        if (string.IsNullOrWhiteSpace(publicKey))
            throw new ArgumentException($"'{nameof(publicKey)}' cannot be null or whitespace.", nameof(publicKey));

        if (string.IsNullOrWhiteSpace(privateKey))
            throw new ArgumentException($"'{nameof(privateKey)}' cannot be null or whitespace.", nameof(privateKey));

        Url = url;
        PublicKey = publicKey;
        PrivateKey = privateKey;
    }

    public Uri Url { get; }
    public string PrivateKey { get; }
    public string PublicKey { get; }
}
