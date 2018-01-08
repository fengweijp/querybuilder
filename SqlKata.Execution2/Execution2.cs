using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace SqlKata.Execution2
{
    public partial class Execution2
    { 
        public static async Task<PaginationResult<T>> PaginateAsync<T>(this IDbConnection cnn, Query query, int page, int perPage = 25,
            QueryBuilderSettings settings=null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (page < 1)
            {
                throw new ArgumentException("Page param should be greater than or equal to 1", nameof(page));
            }

            if (perPage < 1)
            {
                throw new ArgumentException("PerPage param should be greater than or equal to 1", nameof(perPage));
            }

            var count = await cnn.CountAsync<long>(query.Clone().AsCount(), settings, transaction, commandTimeout);
            var pageQuery = query.Clone().ForPage(page, perPage);
            var list = await cnn.QueryAsync<T>(pageQuery, settings, transaction, commandTimeout);

            return new PaginationResult<T>
            {
                CommandTimeout = commandTimeout,
                Connection = cnn,
                Count = count,
                List = list,
                Page = page,
                PerPage = perPage,
                Query = query,
                Transaction = transaction
            };

        }

        public static async Task<PaginationResult<dynamic>> PaginateAsync(this IDbConnection cnn, Query query, int page, int perPage = 25,
            QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await cnn.PaginateAsync<dynamic>(query, page, perPage, settings, transaction, commandTimeout);
        }
    }
}