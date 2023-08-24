using CommunityToolkit.Diagnostics;

namespace NineDigit.WooCommerce;

public sealed class WooCommerceApiClientOptions
{
    const string WooCommerceApiV3Postfix = "/wp-json/wc/v3";

    /// <summary>
    /// </summary>
    /// <param name="url">The WooCommerce webpage URL. E.g. <c>https://foo.bar</c> or <c>https://foo.bar/wp-json/wc/v3</c></param>
    /// <param name="publicKey">The public key</param>
    /// <param name="privateKey">The private key (secret)</param>
    /// <exception cref="ArgumentException"></exception>
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

        Url = url.EnsurePostfix(WooCommerceApiV3Postfix);
        PublicKey = publicKey;
        PrivateKey = privateKey;
    }

    public Uri Url { get; }
    public string PrivateKey { get; }
    public string PublicKey { get; }
}
