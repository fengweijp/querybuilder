using System;
using SqlKata;
using SqlKata.Tests;
using Xunit;

namespace QueryBuilder.SqlServer.Tests
{
    public partial class QueryTests
    {
        private QueryBuilderSettings settings = QueryBuilderTests.DefSqlServerSettings;

        [Fact]
        public void CanCompileLimit()
        {
            var query = new Query("TABLE")
                .Select("Id")
                .Limit(5);

            var r = query.Build(settings);
            Assert.Equal("SELECT TOP (5) [Id] FROM [TABLE]", r.ToString());
        }

        [Fact]
        public void CanCompileOffset()
        {
            var query = new Query("TABLE")
                .Select("Id")
                .Offset(20);

            var r = query.Build(settings);
            Assert.Equal(
                "SELECT * FROM (SELECT [Id], ROW_NUMBER() OVER (ORDER BY (SELECT 0)) AS [row_num] FROM [TABLE]) AS [subquery] WHERE [row_num] >= 20",
                r.RawSql);
        }

        [Fact]
        public void CanCompileLimitOffset()
        {
            var query = new Query("TABLE")
                .Select("Id")
                .Limit(5)
                .Offset(20);

            var r = query.Build(settings);
            Assert.Equal("SELECT * FROM (SELECT [Id], ROW_NUMBER() OVER (ORDER BY (SELECT 0)) AS [row_num] FROM [TABLE]) AS [subquery] WHERE [row_num] BETWEEN 21 AND 25",r.ToString());
        }

        [Fact]
        public void CanSkipTake()
        {
            var query = new Query("TABLE")
                .Select("Id")
                .Skip(20)
                .Take(5);

            var r = query.Build(settings);
            Assert.Equal("SELECT * FROM (SELECT [Id], ROW_NUMBER() OVER (ORDER BY (SELECT 0)) AS [row_num] FROM [TABLE]) AS [subquery] WHERE [row_num] BETWEEN 21 AND 25", r.ToString());
        }

        [Fact]
        public void CanForPage()
        {
            var query = new Query("TABLE")
                .Select("Id")
                .ForPage(5, 5);

            var r = query.Build(settings);
            Assert.Equal("SELECT * FROM (SELECT [Id], ROW_NUMBER() OVER (ORDER BY (SELECT 0)) AS [row_num] FROM [TABLE]) AS [subquery] WHERE [row_num] BETWEEN 21 AND 25", r.ToString());
        }
    }
}