using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace SqlKata.Execution2
{
    public static partial class SqlMapper
    {
        public static Task<IEnumerable<dynamic>> QueryAsync(this IDbConnection cnn, Query query, QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var result = query.Build(settings);
            return cnn.QueryAsync<dynamic>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection cnn, Query query, QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var result = query.Build(settings);
            return cnn.QueryAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public static Task<T> QueryFirstAsync<T>(this IDbConnection cnn, Query query, QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var result = query.Build(settings);
            return cnn.QueryFirstAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public static Task<T> QueryFirstOrDefaultAsync<T>(this IDbConnection cnn, Query query, QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var result = query.Build(settings);
            return cnn.QueryFirstOrDefaultAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public static Task<T> QuerySingleAsync<T>(this IDbConnection cnn, Query query, QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var result = query.Build(settings);
            return cnn.QuerySingleAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public static Task<T> QuerySingleOrDefaultAsync<T>(this IDbConnection cnn, Query query, QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var result = query.Build(settings);
            return cnn.QuerySingleOrDefaultAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public static Task<dynamic> QueryFirstAsync(this IDbConnection cnn, Query query, QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var result = query.Build(settings);
            return cnn.QueryFirstAsync<dynamic>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public static Task<dynamic> QueryFirstOrDefaultAsync(this IDbConnection cnn, Query query, QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var result = query.Build(settings);
            return cnn.QueryFirstOrDefaultAsync<dynamic>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public static Task<dynamic> QuerySingleAsync(this IDbConnection cnn, Query query, QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var result = query.Build(settings);
            return cnn.QuerySingleAsync<dynamic>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public static Task<dynamic> QuerySingleOrDefaultAsync(this IDbConnection cnn, Query query, QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var result = query.Build(settings);
            return cnn.QuerySingleOrDefaultAsync<dynamic>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public static Task<object> ExecuteScalarAsync(this IDbConnection cnn, Query query, QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var result = query.Build(settings);
            return cnn.ExecuteScalarAsync<object>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public static Task<T> ExecuteScalarAsync<T>(this IDbConnection cnn, Query query, QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var result = query.Build(settings);
            return cnn.ExecuteScalarAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }
    }
}