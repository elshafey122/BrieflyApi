using System.Net;

namespace Briefly.Core.Response
{
    public class Response<T> 
    {
        public Response() 
        {
            
        }   
        public Response(T data , HttpStatusCode statusCode = HttpStatusCode.OK , string message="", bool succeeded = true)
        {
            Succeeded = succeeded;
            Data = data;
            Message = message;
            StatusCode = statusCode;
        }

        public bool Succeeded { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
