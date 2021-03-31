using Microsoft.AspNetCore.Http;

namespace MvcCore.ViewModels.Request
{
    public class ProductImageUpdateRequest
    {
        public int ID { get; set; }
        public string Captions { get; set; }
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}