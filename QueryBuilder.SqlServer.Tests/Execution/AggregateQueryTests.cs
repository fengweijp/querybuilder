using System.Threading.Tasks;
using Dapper;
using SqlKata;
using Xunit;
using SqlKata.Execution2;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly,DisableTestParallelization = true, MaxParallelThreads = 1)]

namespace QueryBuilder.SqlServer.Tests.Execution
{
    [Collection(nameof(SqlTestCollection))]
    public class AggregateQueryTests : SqlServerDatabaseTest
    {
        void SetUp()
        {
            Connection.ExecuteAsync(@"
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
            SetUp();
        }

        [Fact]
        public async Task CanCountData()
        {
            var query = new Query("agg");
            var count = await Connection.CountAsync<int>(query);
            Assert.Equal(5, count);
        }

        [Fact]
        public async Task CanSumData()
        {
            var query = new Query("agg");
            var sum = await Connection.SumAsync<int>(query, "num");
            Assert.Equal(15, sum);
        }

        [Fact]
        public async Task CanMaxData()
        {
            var query = new Query("agg");
            var count = await Connection.MaxAsync<int>(query,"num");
            Assert.Equal(5, count);
        }

        [Fact]
        public async Task CanMinData()
        {
            var query = new Query("agg");
            var count = await Connection.MinAsync<int>(query,"num");
            Assert.Equal(1, count);
        }


    }
}