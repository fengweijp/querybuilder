using System;
using System.Threading.Tasks;
using Dapper;
using SqlKata;
using Xunit;
using SqlKata.Execution2;

namespace QueryBuilder.SqlServer.Tests.Execution
{
    [Collection(nameof(SqlTestCollection))]
    public class PagingTests : SqlServerDatabaseTest
    {
        private const string tablename = "TABLE";

        public PagingTests(SqlServerDatabaseFixture database) : base(database)
        {
            SetUp();
        }

        void SetUp()
        {
            Connection.Execute(@"
if object_id('" + tablename + "') is not null drop table [" + tablename + @"]
CREATE TABLE [" + tablename + "] ([Id] int)");

            string sql = string.Empty;
            for (int i = 0; i < 44; i++)
            {
                sql += $"INSERT INTO [{tablename}] (Id) VALUES ({i}){Environment.NewLine}";
            }

            Connection.Execute(sql);
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