namespace MvcCore.ViewModels.BaseCommons
{
    public class ApiSuccess<T> : ApiResponse<T>
    {
        public ApiSuccess()
        {
            StatusCode = 200;
        }

        public ApiSuccess(string message)
        {
            StatusCode = 200;
            Message = message;
        }

        public ApiSuccess(T result)
        {
            StatusCode = 200;
            Result = result;
        }

        public ApiSuccess(T result, string message)
        {
            StatusCode = 200;
            Result = result;
            Message = message;
        }
    }
}