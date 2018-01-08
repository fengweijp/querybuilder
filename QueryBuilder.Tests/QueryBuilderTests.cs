using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public static QueryBuilderTestSettings Current
        {
            get
            {
                var sett = new QueryBuilderTestSettings();
                CurrentConfigurationRoot.Bind(sett);
                return sett;
            }
        }

        public List<TestConnectionString> ConnectionString { get; set; }
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