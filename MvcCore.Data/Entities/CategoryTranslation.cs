using System;
using System.Collections.Generic;
using System.Text;

namespace MvcCore.Data.Entities
{
    public class CategoryTranslation
    {
        public int Id { set; get; }
        public int CategoryId { set; get; }
        public string Name { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public int LanguageId { set; get; }
        public string SeoAlias { set; get; }

        public Category Category { get; set; }

        public Languages Language { get; set; }

    }
}
