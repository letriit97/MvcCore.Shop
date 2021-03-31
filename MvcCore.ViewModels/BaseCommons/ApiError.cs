namespace MvcCore.ViewModels.BaseCommons
{
    public class ApiError<T> : ApiResponse<T>
    {
        public ApiError()
        {
            StatusCode = 404;
        }

        public ApiError(string message)
        {
            StatusCode = 404;
            Message = message;
        }

        public ApiError(T result, string message)
        {
            StatusCode = 404;
            Result = result;
            Message = message;
        }
    }
}