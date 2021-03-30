using System;
using System.Collections.Generic;
using System.Text;

namespace MvcCore.Data.Entities
{
    public class ProductCategories
    {
        public int ProductID { get; set; }

        public int ProductCategoryID { get; set; }
        public Product Product { get; set; }
        public Category Category { get; set; }


    }
}
