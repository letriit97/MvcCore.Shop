using System;
using System.Collections.Generic;
using System.Text;

namespace MvcCore.Data.Entities
{
    public class SystemActivities
    {
        public int ID { get; set; }
        public string ActionName { get; set; }
        public DateTime ActionDate { get; set; }
        public int FuntionID { get; set; }
        public int UserID { get; set; }
        public string ClientIP { get; set; }
    }
}
