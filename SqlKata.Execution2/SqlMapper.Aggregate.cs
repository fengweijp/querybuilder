using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace SqlKata.Execution2
{
    public static partial class SqlMapper
    {
        public static async Task<T> Aggregate<T>(this IDbConnection cnn, Query query,
            string aggregateOp,IEnumerable<string> columns = null, QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var result = query
                .AsAggregate(aggregateOp, columns.ToArray())
                .Build(settings);

            return await cnn.ExecuteScalarAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout);
        }

        public static async Task<T> CountAsync<T>(this IDbConnection cnn, Query query,
            QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var result = query.AsCount().Build(settings);
            return await cnn.ExecuteScalarAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout);
        }

        public static async Task<T> Average<T>(this IDbConnection cnn, Query query, string column,
            QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await cnn.Aggregate<T>(query,"avg", new []{column},settings,transaction,commandTimeout);
        }

        public static async Task<T> Sum<T>(this IDbConnection cnn, Query query, string column,
            QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await cnn.Aggregate<T>(query, "sum", new[] { column }, settings, transaction, commandTimeout);
        }

        public static async Task<T> Min<T>(this IDbConnection cnn, Query query, string column,
            QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await cnn.Aggregate<T>(query, "min", new[] { column }, settings, transaction, commandTimeout);
        }

        public static async Task<T> Max<T>(this IDbConnection cnn, Query query, string column,
            QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await cnn.Aggregate<T>(query, "max", new[] { column }, settings, transaction, commandTimeout);
        }
    }
}