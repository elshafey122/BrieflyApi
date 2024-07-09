using System.Net;

namespace Briefly.Core.Pagination
{
    public class PaginationResponse<T>
    {
        public PaginationResponse(bool succeed, List<T> data, string message, HttpStatusCode statusCode, int count, int pagenumber, int pagesize = 10)
        {
            Succeeded = succeed;
            Data = data;
            Message = message;
            PageNumber = pagenumber;
            PageSize = pagesize;
            TotalPages = (int)Math.Ceiling(count / (double)pagesize);
            TotalCount = count;
            StatusCode = statusCode;
        }
        public PaginationResponse(bool succeed, string message)
        {
            Succeeded = succeed;
            Message = message;
            StatusCode = HttpStatusCode.BadRequest;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public bool Succeeded { get; set; }
        public List<T> Data { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public static PaginationResponse<T> Success(List<T> data, string message , int count, int pageNumber, int pageSize)
        {
            return new(true, data, message , HttpStatusCode.OK, count, pageNumber, pageSize);
        }
        public static PaginationResponse<T> Fail(string message)
        {
            return new(false, null ,message , HttpStatusCode.BadRequest, 0, 1 , 10);
        }
    }
}
