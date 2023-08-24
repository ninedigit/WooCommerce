namespace System;

internal static class UriExtensions
{
    public static Uri EnsurePostfix(this Uri self, string postfix)
    {
        if (!self.AbsolutePath.EndsWith(postfix, StringComparison.OrdinalIgnoreCase))
        {
            var newPath = self.AbsolutePath.TrimEnd('/') + postfix;
            return new UriBuilder(self) { Path = newPath }.Uri;
        }

        return self;
    }
}
