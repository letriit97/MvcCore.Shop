using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvcCore.Data.EF
{
    public class ShopOnlineDBContext : DbContext
    {
        public ShopOnlineDBContext(DbContextOptions options) : base(options)
        {
           
        }
        
        ///public DbSet<>
    }
}
