using System;
using System.Collections.Generic;
using System.Text;

namespace MvcCore.Data.Entities
{
    public class TransactionCategory
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTitle { get; set; }
        public int LanguageID { get; set; }
        public string SeoAlias { get; set; }

        public List<Languages> Languages { get; set; }
    }
}
