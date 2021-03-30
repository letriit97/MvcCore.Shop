using MvcCore.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvcCore.Data.Entities
{
    public class Funtions
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int ParentID { get; set; }
        public string SortOrder { get; set; }
        public Status Status { get; set; }
    }
}
