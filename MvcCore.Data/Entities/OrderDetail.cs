using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MvcCore.Data.Entities
{
    public class OrderDetail
    {
        [Key]
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
