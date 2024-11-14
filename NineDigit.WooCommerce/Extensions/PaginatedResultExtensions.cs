using CommunityToolkit.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace NineDigit.WooCommerce;

public static class PaginationExtensions
{
    public static bool IsLastPage<T>(this Pagination pagination, PaginatedResult<T> paginatedResult)
    {
        Guard.IsNotNull(pagination);
        Guard.IsNotNull(paginatedResult);

        return pagination.Skip + paginatedResult.Count == paginatedResult.TotalCount;
    }

    public static Pagination NextPage(this Pagination pagination)
    {
        Guard.IsNotNull(pagination);

        int? skip = (!pagination.Skip.HasValue && !pagination.Take.HasValue)
            ? null
            : (pagination.Skip ?? 0) + (pagination.Take ?? 0);

        return new Pagination
        {
            Skip = skip,
            Take = pagination.Take
        };
    }

    public static bool TryGetNextPage<T>(
        this Pagination self,
        PaginatedResult<T> paginatedResult,
        [NotNullWhen(true)] out Pagination? pagination)
    {
        Guard.IsNotNull(self);
        Guard.IsNotNull(paginatedResult);

        if (IsLastPage(self, paginatedResult))
            pagination = null;
        else
            pagination = new Pagination
            {
                Skip = (self.Skip ?? 0) + (self.Take ?? paginatedResult.Count),
                Take = self.Take
            };

        return pagination is not null;
    }
}