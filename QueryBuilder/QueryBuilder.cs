using System;
using SqlKata.Compilers;

namespace SqlKata
{
    public sealed class QueryBuilder
    {
        private static Func<QueryBuilderSettings> _defaultSettings;

        public static Func<QueryBuilderSettings> DefaultSettings
        {
            get { return _defaultSettings; }
            set
            {
                _defaultSettings = value;   
            }
        }

        private static QueryBuilderSettings GlobalSettings()
        {
            var settings = DefaultSettings == null
                ? new QueryBuilderSettings()
                : DefaultSettings();

            if (settings.Compiler == null)
            {
                settings.Compiler = new SqlServerCompiler();
            }
            return settings;
        }

        public static SqlResult Build(Query query)
        {
            var settings = GlobalSettings();
            return settings.Compiler.Compile(query);
        }

        public static SqlResult Build(Query query, QueryBuilderSettings settings)
        {
            if (settings == null)
            {
                throw new Exception("Settings parameter is null");
            }

            return settings.Compiler.Compile(query);
        }
    }
}
