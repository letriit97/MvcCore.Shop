using MvcCore.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvcCore.Data.Entities
{
    public class Promotion
    {
        public int ID { set; get; }
        public DateTime FromDate { set; get; }
        public DateTime ToDate { set; get; }
        public bool ApplyForAll { set; get; }
        public int? DiscountPercent { set; get; }
        public decimal? DiscountAmount { set; get; }
        public string ProductIds { set; get; }
        public string ProductCategoryIDs { set; get; }
        public Status Status { set; get; }
        public string Name { set; get; }

    }
}
