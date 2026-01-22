using WizCo.Application.Shared.Results;
using WizCo.Domain.Shared;
using X.PagedList.Extensions;

namespace WizCo.Application.Shared.Factories
{
    public static class PagedResultFactory<T>
    {
        public static PagedResult<T> Create(QueryBase query, IQueryable<T> queryableData)
        {
            var paged = queryableData.ToPagedList(query.Page, query.PageSize);

            if (paged.Any() is false)
            {
                return PagedResult<T>.Empty();
            }

            return new PagedResult<T>
            {
                Data = paged,
                Page = paged.PageNumber,
                TotalItemsPage = paged.Count,
                TotalItems = paged.TotalItemCount,
                TotalPages = paged.PageCount
            };
        }
    }
}
