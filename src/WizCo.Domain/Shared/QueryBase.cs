using AspNetCore.IQueryable.Extensions;

namespace WizCo.Domain.Shared
{
    public abstract class QueryBase : ICustomQueryable
    {
        protected string _sort;

        private int _pageSize;

        public int Page { get; set; }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value == 0)
                    _pageSize = 50;
                else
                    _pageSize = value;
            }
        }

        protected abstract string DefaultSort { get; }

        public string Sort { get { return _sort ?? DefaultSort; } set { _sort = value; } }

        public QueryBase()
        {
            Page = 1;
            PageSize = 0;
        }

        /// <summary>
        /// Search term used to filter query results
        /// </summary>
        public string Search { get; set; }
    }
}
