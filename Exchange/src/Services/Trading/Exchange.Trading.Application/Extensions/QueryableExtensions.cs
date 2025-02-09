using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Exchange.Trading.Application.Extensions;

public static class QueryableExtensions
{
    public static async Task<List<T>> ToPaginatedListAsync<T>(
        this IQueryable<T> query,
        int pageIndex,
        int pageSize,
        Expression<Func<T, object>>? orderByDescending = null,
        Expression<Func<T, object>>? orderByAscending = null
        )
    {
        if (orderByDescending != null)
        {
            query = query
                .OrderByDescending(orderByDescending);
        }

        if (orderByAscending != null)
        {
            query = query
                .OrderBy(orderByAscending);
        }

        return await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}
