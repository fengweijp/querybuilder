using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace SqlKata.Execution2
{
    public static partial class SqlMapper
    {
        public static async Task<long> CountAsync(this IDbConnection cnn, Query query,
            QueryBuilderSettings settings = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var result = query.AsCount().Build(settings);
            return await cnn.ExecuteScalarAsync<long>(result.Sql, result.Bindings, transaction, commandTimeout,
                commandType);
        }
    }
}