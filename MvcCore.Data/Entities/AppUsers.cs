using Microsoft.AspNetCore.Identity;
using MvcCore.Data.Entities;
using System;
using System.Collections.Generic;

namespace MvcCore.Data.Entities
{
    public class AppUsers : IdentityUser<Guid>
    {
        //[Key]
        //public int AppUserID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }

        public List<Cart> Carts { get; set; }
        public List<Order> Orders { get; set; }
        public List<Transaction> Transactions { get; set; }

        //public List<Notification> Notifications { get; set; }
    }
}
