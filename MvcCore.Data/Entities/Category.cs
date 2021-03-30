using MvcCore.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvcCore.Data.Entities
{
    public class Category
    {
        public int ID { get; set; }
        public int SortOrder { get; set; }
        public int IsShowOnHome { get; set; }
        public int? ParentID { get; set; }
        public Status Status { get; set; }

        public List<ProductCategories> ProductCategories { get; set; }

        public List<CategoryTranslation> TranslationCategories { get; set; }
    }
}
