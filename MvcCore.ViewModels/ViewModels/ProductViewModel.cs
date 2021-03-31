using System;

namespace MvcCore.ViewModels.ViewModels
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public Decimal OriginalPrice { get; set; }
        public int Stock { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreateTime { get; set; }
        public string SeoAlias { get; set; }
        public string Name { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }

        public int LanguageId { set; get; }
    }
}