using Microsoft.AspNetCore.Http;

namespace MvcCore.ViewModels.Request
{
    public class ProductCreateRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public int Stock { get; set; }
        public int ViewCount { get; set; }
        public string SeoAlias { get; set; }
        public int LanguageID { get; set; }
        public IFormFile FileUpload { get; set; }
    }
}