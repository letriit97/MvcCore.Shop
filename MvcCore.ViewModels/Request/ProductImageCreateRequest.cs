using Microsoft.AspNetCore.Http;

namespace MvcCore.ViewModels.Request
{
    public class ProductImageCreateRequest
    {
        public string Captions { get; set; }
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}