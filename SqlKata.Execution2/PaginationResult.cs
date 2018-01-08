using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SqlKata.Execution2;

namespace SqlKata.Execution
{
    public class PaginationResult<T>
    {
        internal IDbConnection Connection { get; set; }
        internal IDbTransaction Transaction { get; set; }
        public int? CommandTimeout { get; set; }
        internal QueryBuilderSettings Settings { get; set; }
        public Query Query { get; set; }
        public long Count { get; set; }
        public IEnumerable<T> List { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalPages
        {
            get
            {

                if (PerPage < 1)
                {
                    return 0;
                }

                var div = (float)Count / PerPage;

                return (int)Math.Ceiling(div);

            }
        }

        public bool IsFirst
        {
            get
            {
                return Page == 1;
            }
        }

        public bool IsLast
        {
            get
            {
                return Page == TotalPages;
            }
        }

        public bool HasNext
        {
            get
            {
                return Page < TotalPages;
            }
        }

        public bool HasPrevious
        {
            get
            {
                return Page > 1;
            }
        }

        public Query NextQuery()
        {
            return this.Query.ForPage(Page + 1, PerPage);
        }

        public Query PreviousQuery()
        {
            return this.Query.ForPage(Page - 1, PerPage);
        }

        public Query SeekQuery(int page)
        {
            return this.Query.ForPage(page, PerPage);
        }
    }

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