using System;

namespace SqlKata
{
    public class QueryBuilderException : Exception
    {
        //QuerySyntaxException
        //maybe CompilationException or ..
        public QueryBuilderException()
        {
        }

        public QueryBuilderException(string message) : base(message)
        {
        }

        public QueryBuilderException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}