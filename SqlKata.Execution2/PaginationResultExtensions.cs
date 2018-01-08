using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlKata.Execution2
{
    public static class PaginationResultExtensions
    {
        public static Task<PaginationResult<T>> NextAsync<T>(this PaginationResult<T> result)
        {
            return result.Connection.PaginateAsync<T>(result.Query, result.Page + 1, result.PerPage, result.Settings, result.Transaction, result.CommandTimeout);
        }
        public static Task<PaginationResult<T>> PreviousAsync<T>(this PaginationResult<T> result)
        {
            return result.Connection.PaginateAsync<T>(result.Query, result.Page + 1, result.PerPage, result.Settings, result.Transaction, result.CommandTimeout);
        }

        public static Task<PaginationResult<T>> SeekAsync<T>(this PaginationResult<T> result, int desiredPage)
        {
            return result.Connection.PaginateAsync<T>(result.Query, desiredPage, result.PerPage, result.Settings, result.Transaction, result.CommandTimeout);
        }

        public static IEnumerable<PaginationResult<T>> ForEach<T>(this PaginationResult<T> result)
        {
            return result.ForEachExplicit();
        }

        public static IEnumerable<PaginationResult<T>> ForEachExplicit<T>(this PaginationResult<T> result, int startingPage = 1)
        {
            var seek = result.SeekAsync(1).GetAwaiter().GetResult();
            var iterator = new PaginationIterator<T>()
            {
                CurrentPage = seek,
                FirstPage = seek
            };

            return iterator;
        }

        public static T FirstOrDefault<T>(this PaginationResult<T> result,
            Predicate<T> predicate)
        {
            foreach (var paginationResult in result.ForEachExplicit())
            {
                foreach (var row in paginationResult.List)
                {
                    if (predicate.Invoke(row))
                    {
                        return row;
                    }
                }
            }

            return default(T);
        }
    }
}