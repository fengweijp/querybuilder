namespace SqlKata
{
    public static class QueryBuilderExtensions
    {
        public static SqlResult Build(this Query query)
        {
            return QueryBuilder.Build(query);
        }

        public static SqlResult Build(this Query query, QueryBuilderSettings settings)
        {
            return QueryBuilder.Build(query, settings);
        }
    }
}
