using System;
using System.Collections.Generic;
using System.Text;

namespace MvcCore.Data.Entities
{
    public class ProductTranslation
    {
        public int ID { set; get; }
        public int ProductID { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }

        public string SeoAlias { get; set; }
        public int LanguageId { set; get; }

        public Product Product { get; set; }

        public Languages Languages { get; set; }

    }
}
