using Xunit;

namespace QueryBuilder.SqlServer.Tests.Execution
{
    [CollectionDefinition(nameof(SqlTestCollection))]
    public class SqlTestCollection : ICollectionFixture<SqlServerDatabaseFixture>
    {
        
    }
}