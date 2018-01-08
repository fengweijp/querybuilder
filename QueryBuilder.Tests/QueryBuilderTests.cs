using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using SqlKata.Compilers;

namespace SqlKata.Tests
{
    public class TestConnectionString
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }

    public class QueryBuilderTestSettings
    {
        public static readonly IConfigurationRoot CurrentConfigurationRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("testSettings.json")
            .Build();

        public static IEnumerable<TestConnectionString> ConnectionStrings
        {
            get
            {
                var sett = new List<TestConnectionString>();
                CurrentConfigurationRoot.GetSection("ConnectionStrings").Bind(sett);
                return sett;
            }
        }

    }


    public class QueryBuilderTests
    {
        public static readonly QueryBuilderSettings DefSqlServerSettings = new QueryBuilderSettings()
        {
            Compiler = new SqlServerCompiler()
        };

        public static readonly QueryBuilderSettings DefPostgresSettings = new QueryBuilderSettings()
        {
            Compiler = new PostgresCompiler()
        };

        public static readonly QueryBuilderSettings DefMySqlSettings = new QueryBuilderSettings()
        {
            Compiler = new MySqlCompiler()
        };
    }
}