using System;
using System.Collections.Generic;
using System.Data;

namespace SqlKata.Execution2
{
    public class PaginationResult<T>
    {
        internal IDbConnection Connection { get; set; }
        internal IDbTransaction Transaction { get; set; }
        public int? CommandTimeout { get; set; }
        internal QueryBuilderSettings Settings { get; set; }
        public Query Query { get; set; }
        public long Count { get; set; }
        public IEnumerable<T> List { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalPages
        {
            get
            {

                if (PerPage < 1)
                {
                    return 0;
                }

                var div = (float)Count / PerPage;

                return (int)Math.Ceiling(div);

            }
        }

        public bool IsFirst
        {
            get
            {
                return Page == 1;
            }
        }

        public bool IsLast
        {
            get
            {
                return Page == TotalPages;
            }
        }

        public bool HasNext
        {
            get
            {
                return Page < TotalPages;
            }
        }

        public bool HasPrevious
        {
            get
            {
                return Page > 1;
            }
        }

        public Query NextQuery()
        {
            return this.Query.ForPage(Page + 1, PerPage);
        }

        public Query PreviousQuery()
        {
            return this.Query.ForPage(Page - 1, PerPage);
        }

        public Query SeekQuery(int page)
        {
            return this.Query.ForPage(page, PerPage);
        }
    }
}