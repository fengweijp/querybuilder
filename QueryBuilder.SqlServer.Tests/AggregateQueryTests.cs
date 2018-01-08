using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using SqlKata;
using SqlKata.Tests.Execution;
using Xunit;

namespace QueryBuilder.SqlServer.Tests
{
    public abstract class SqlDatabaseTest
    {
        private readonly SqlServerDatabaseFixture _database;

        protected SqlDatabaseTest(SqlServerDatabaseFixture database)
        {
            _database = database;
        }

        protected IExecutionDatabaseFixture Database { get { return _database; } }

        protected SqlConnection Connection { get { return (SqlConnection)_database.Connection; } }

        protected SqlServerDatabaseFixture SqlDatabase { get { return _database; } }
    }

    [CollectionDefinition("AggregateTests")]
    public class AggregateTestsCollection : ICollectionFixture<SqlServerDatabaseFixture>,
        IClassFixture<SqlServerDatabaseFixture>
    {

    }

    [Collection("AggregateTests")]
    public class AggregateQueryTests : SqlDatabaseTest
    {
        //TODO: Store test data in manifest resource files
        async Task SetUp()
        {
            await Connection.ExecuteAsync(@"
if object_id('agg') is not null drop table agg
CREATE TABLE agg ([num] int)
INSERT INTO agg VALUES (1);
INSERT INTO agg VALUES (2);
INSERT INTO agg VALUES (3);
INSERT INTO agg VALUES (4);
INSERT INTO agg VALUES (5);");

        }

        public AggregateQueryTests(SqlServerDatabaseFixture database) : base(database)
        {
            SetUp().GetAwaiter().GetResult();
        }

        [Fact]
        public async Task CanSumData()
        {
            var query = new Query("agg");

        }
    }
}