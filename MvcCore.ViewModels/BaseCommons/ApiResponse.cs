namespace MvcCore.ViewModels.BaseCommons
{
    public class ApiResponse<T>
    {
        public bool Staus { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}