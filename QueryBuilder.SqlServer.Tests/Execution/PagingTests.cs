using System.Threading.Tasks;
using SqlKata;
using Xunit;

namespace QueryBuilder.SqlServer.Tests.Execution
{
    [Collection("PagingTests")]
    public class PagingTests : SqlServerDatabaseTest
    {
        private const string tablename = "TABLE";

        public PagingTests(SqlServerDatabaseFixture database) : base(database)
        {
            SetUp().GetAwaiter().GetResult();
        }

        async Task SetUp()
        {
//            await Connection.ExecuteAsync(@"
//if object_id('"+tablename+"') is not null drop table ["+tablename+@"]
//CREATE TABLE ["+tablename+"] ([num] int)");

//            string sql = string.Empty;
//            for (int i = 0; i < numRows; i++)
//            {
//                sql += $"INSERT INTO [{tablename}] (num) VALUES ({i}){Environment.NewLine}";
//            }

//            await Connection.ExecuteAsync(sql);
        }

        [Fact]
        public async Task CanPageDynamic()
        {
            var query = new Query(tablename)
                .Select("Id");

            const int pageSz = 5;
            var page = await Connection.PaginateAsync(query, 1,pageSz);

            int counter = 0;
            while (true)
            {
                ++counter;
                if (page.IsLast) { //from prev iteration
                    break;
                }
                page = await page.NextAsync();
            }

            Assert.Equal(page.TotalPages, counter);
        }
    }
}