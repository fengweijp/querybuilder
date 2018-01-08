using System.Data.SqlClient;
using SqlKata.Tests.Execution;
using Xunit;

namespace QueryBuilder.SqlServer.Tests
{
    public abstract class SqlServerDatabaseTest : IClassFixture<SqlServerDatabaseFixture>
    {
        private readonly SqlServerDatabaseFixture _database;

        protected SqlServerDatabaseTest(SqlServerDatabaseFixture database)
        {
            _database = database;
        }

        protected IExecutionDatabaseFixture Database { get { return _database; } }

        protected SqlConnection Connection { get { return (SqlConnection)_database.Connection; } }

        protected SqlServerDatabaseFixture SqlDatabase { get { return _database; } }
    }
}