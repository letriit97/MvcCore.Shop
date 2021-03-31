using Microsoft.AspNetCore.Http;

namespace MvcCore.ViewModels.Request
{
    public class ProductUpdateRequest
    {
        public int ID { get; set; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string SeoAlias { get; set; }
        public int LanguageId { set; get; }

        public IFormFile FileUpload { get; set; }
    }
}