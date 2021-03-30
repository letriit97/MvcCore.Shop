using MvcCore.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvcCore.Data.Entities
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime CreateTime { get; set; }
        public Guid UserID { get; set; }

        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipEmail { get; set; }
        public string ShipMobile { get; set; }
        public OrderStatus Status { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public AppUsers AppUsers { get; set; }

    }
}
