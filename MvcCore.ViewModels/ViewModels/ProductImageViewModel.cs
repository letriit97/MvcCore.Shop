using System;

namespace MvcCore.ViewModels.ViewModels
{
    public class ProductImageViewModel
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string Url { get; set; }
        public string Captions { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreateTime { get; set; }
        public int SortOrder { get; set; }
        public long FileSize { get; set; }
    }
}