using System;
using System.Collections.Generic;
using System.Text;

namespace MvcCore.Data.Entities
{
    public class ProductImage
    {
        public int ID { get; set; }
        public int ProductID { get; set; }

        public string Url { get; set; }

        public string Captions { get; set; }

        public bool IsDefault { get; set; }

        public DateTime CreateTime { get; set; }
        public int SortOrder { get; set; }

        public long FileSize { get; set; }

        public Product Products { get; set; }
    }
}
