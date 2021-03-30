using Microsoft.EntityFrameworkCore;
using MvcCore.Data.Entities;
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

        public DbSet<Actions> Actions { get; set; }
        public DbSet<AppConfigration> Configrations { get; set; }
        public DbSet<AppRoles> AppRoles { get; set; }
        public DbSet<AppUsers> AppUsers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> TranslationCategories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Funtions> Funtions { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductCategories> ProductCategories { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<SystemActivities> SystemActivities { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
    }
}
