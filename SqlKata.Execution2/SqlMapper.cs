using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using SqlKata.Execution;

namespace SqlKata.Execution2
{
    public partial class SqlMapper
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

            var count = await cnn.CountAsync<long>(query.AsCount(), settings, transaction, commandTimeout);

            var list = await cnn.QueryAsync<T>(query.ForPage(page, perPage), settings, transaction, commandTimeout);

            return new PaginationResult<T>
            {
                Query = query.Clone(),
                Page = page,
                PerPage = perPage,
                Count = count,
                List = list
            };

        }

        public static async Task<PaginationResult<dynamic>> PaginateAsync(this IDbConnection cnn, Query query, int page, int perPage = 25,
            QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await cnn.PaginateAsync<dynamic>(query, page, perPage, settings, transaction, commandTimeout);
        }
    }
}