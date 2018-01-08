using System.Threading.Tasks;
using Dapper;
using SqlKata;
using Xunit;

namespace QueryBuilder.SqlServer.Tests.Execution
{
    [Collection("AggregateSimpleTests")]
    public class AggregateQueryTests : SqlServerDatabaseTest
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
        public async Task CanCountData()
        {
            var query = new Query("agg");
            var count = await Execution2.CountAsync<int>(Connection, query);
            Assert.Equal(5, count);
        }

        [Fact]
        public async Task CanSumData()
        {
            var query = new Query("agg");
            var sum = await Execution2.SumAsync<int>(Connection, query, "num");
            Assert.Equal(15, sum);
        }

        [Fact]
        public async Task CanMaxData()
        {
            var query = new Query("agg");
            var count = await Execution2.MaxAsync<int>(Connection, query,"num");
            Assert.Equal(5, count);
        }

        [Fact]
        public async Task CanMinData()
        {
            var query = new Query("agg");
            var count = await Execution2.MinAsync<int>(Connection, query,"num");
            Assert.Equal(1, count);
        }


    }
}