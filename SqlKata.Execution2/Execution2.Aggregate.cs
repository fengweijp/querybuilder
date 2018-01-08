using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace SqlKata.Execution2
{
    public static partial class Execution2
    {
        public static async Task<T> AggregateAsync<T>(this IDbConnection cnn, Query query,
            string AggregateAsyncOp,IEnumerable<string> columns = null, QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var result = query
                .AsAggregate(AggregateAsyncOp, columns.ToArray())
                .Build(settings);

            return await cnn.ExecuteScalarAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout);
        }

        public static async Task<T> CountAsync<T>(this IDbConnection cnn, Query query,
            QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var result = query.AsCount().Build(settings);
            return await cnn.ExecuteScalarAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout);
        }

        public static async Task<T> AverageAsync<T>(this IDbConnection cnn, Query query, string column,
            QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await cnn.AggregateAsync<T>(query,"avg", new []{column},settings,transaction,commandTimeout);
        }

        public static async Task<T> SumAsync<T>(this IDbConnection cnn, Query query, string column,
            QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await cnn.AggregateAsync<T>(query, "sum", new[] { column }, settings, transaction, commandTimeout);
        }

        public static async Task<T> MinAsync<T>(this IDbConnection cnn, Query query, string column,
            QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await cnn.AggregateAsync<T>(query, "min", new[] { column }, settings, transaction, commandTimeout);
        }

        public static async Task<T> MaxAsync<T>(this IDbConnection cnn, Query query, string column,
            QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await cnn.AggregateAsync<T>(query, "max", new[] { column }, settings, transaction, commandTimeout);
        }
    }
}