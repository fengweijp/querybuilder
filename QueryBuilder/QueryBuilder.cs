using System;
using SqlKata.Compilers;

namespace SqlKata
{
    public sealed class QueryBuilder
    {
        public static Func<QueryBuilderSettings> DefaultSettings { get; set; }

        private static QueryBuilderSettings GlobalSettings()
        {
            var settings = DefaultSettings == null
                ? new QueryBuilderSettings()
                : DefaultSettings();

            if (settings.Compiler == null)
            {
                if (DefaultSettings != null)
                {
                    throw new QueryBuilderException($"{nameof(settings.Compiler)} is null");
                }

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
                settings = GlobalSettings();
            }

            return settings.Compiler.Compile(query);
        }
    }
}
