using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvcCore.Data.Entities
{
    public class AppRoles : IdentityRole<Guid>
    {
        //public int ID { get; set; }
        //public string Title { get; set; }
        public string Description { get; set; }
    }
}
