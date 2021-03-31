namespace MvcCore.ViewModels.BaseCommons
{
    public class ApiAuthorize<T> : ApiResponse<T>
    {
        public ApiAuthorize()
        {
            StatusCode = 401;
        }

        public ApiAuthorize(string message)
        {
            StatusCode = 401;
            Message = message;
        }

        public ApiAuthorize(T result, string message)
        {
            StatusCode = 401;
            Result = result;
            Message = message;
        }
    }
}