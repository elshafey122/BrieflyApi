namespace Briefly.Core.Pagination
{
    public static class QuerablePagiExtensions
    {
        public static async Task<PaginationResponse<T>> PaginateList<T>(this IList<T>data ,int pageNumber,int PageSize)
            where T : class
        {
            if (data == null)
            {
                return null;
            }
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            PageSize = PageSize <= 0 ? 10 : PageSize;
            int count = data.Count();
            if (count == 0)
            {
                return PaginationResponse<T>.Fail("There is no data");
            }
            var finalData = data.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();
            return PaginationResponse<T>.Success(finalData, "Get data successfully", count, pageNumber, PageSize);   
        }
        
    }
}