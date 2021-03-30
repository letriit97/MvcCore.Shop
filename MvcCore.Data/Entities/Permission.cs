using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MvcCore.Data.Entities
{
    public class Permission
    {
        [Key]
        public int RoleID { get; set; }
        public int FuntionID { get; set; }
        public int ActionID { get; set; }
    }
}
