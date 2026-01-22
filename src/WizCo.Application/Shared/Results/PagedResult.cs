namespace WizCo.Application.Shared.Results
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Data { get; set; }

        public int TotalItemsPage { get; set; } = 0;

        public int TotalItems { get; set; } = 0;

        public int Page { get; set; } = 0;

        public int TotalPages { get; set; } = 0;

        public static PagedResult<T> Empty()
        {
            var result = new PagedResult<T>
            {
                Data = []
            };

            return result;
        }
    }
}
