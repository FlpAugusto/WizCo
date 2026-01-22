namespace WizCo.Infrastructure.Data
{
    public class BaseQueryBuilder<T>
    {
        protected IQueryable<T> query;

        public BaseQueryBuilder(IQueryable<T> initialQuery)
        {
            query = initialQuery;
        }

        public IQueryable<T> Build() => query;
    }
}
