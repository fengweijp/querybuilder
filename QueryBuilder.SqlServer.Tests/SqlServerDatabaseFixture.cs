 public class SqlServerDatabaseFixture : IDisposable
    {
        public SqlServerDatabaseFixture()
        {
            var connectionString =
                QueryBuilderTestSettings.Current.ConnectionString.Single(s=>s.Name=="sqlserver").ConnectionString;
            var connection = new SqlConnection(connectionString);

        }

        public void Dispose()
        {

        }
    }