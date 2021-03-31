using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcCore.Data.Entities;
using MvcCore.Data.Enum;
using System;

namespace MvcCore.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //AppConfigration
            modelBuilder.Entity<AppConfigration>().HasData(
                new AppConfigration() { Key = "Key1", Value = "Data Value 1" },
                new AppConfigration() { Key = "Key2", Value = "Data Value 2" },
                new AppConfigration() { Key = "Key3", Value = "Data Value 3" }

                );

            //Languages
            modelBuilder.Entity<Languages>().HasData(
                    new Languages() { ID = 1, Title = "VietName", IsDefault = true },
                    new Languages() { ID = 2, Title = "US", IsDefault = false }
                );

            //CategoryTranslation
            modelBuilder.Entity<CategoryTranslation>().HasData(
                new CategoryTranslation()
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "Áo Nam",
                    LanguageId = 1,
                    SeoAlias = "áo nam",
                    SeoTitle = "Sản phẩm áo thời trang Nam",
                    SeoDescription = "Sản phẩm áo thời trang Nam"
                },
                new CategoryTranslation()
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Men-Shirt",
                    LanguageId = 2,
                    SeoAlias = "Men-Shirt",
                    SeoTitle = "Product Shirt for men",
                    SeoDescription = "Product Shirt for men"
                },
                new CategoryTranslation()
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "Áo Nữ",
                    LanguageId = 1,
                    SeoAlias = "áo Nữ",
                    SeoTitle = "Sản phẩm áo thời trang Nữ",
                    SeoDescription = "Sản phẩm áo thời trang Nữ"
                },
                new CategoryTranslation()
                {
                    Id = 4,
                    CategoryId = 2,
                    Name = "Women-Shirt",
                    LanguageId = 2,
                    SeoAlias = "Women-Shirt",
                    SeoTitle = "Product Shirt for Women",
                    SeoDescription = "Product Shirt for Women"
                }
            );


            //Category
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    ID = 1,
                    IsShowOnHome = 1,
                    ParentID = 1,
                    SortOrder = 1,
                    Status = Status.Active,
                },
                new Category()
                {
                    ID = 2,
                    IsShowOnHome = 1,
                    ParentID = 2,
                    SortOrder = 2,
                    Status = Status.Active,
                }
            );



            //Product
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    ID = 1,
                    CreateTime = DateTime.Now,
                    OriginalPrice = 100000,
                    Price = 200000,
                    Stock = 0,
                    ViewCount = 0,
                });

            //ProductTranslation
            modelBuilder.Entity<ProductTranslation>().HasData(
                new ProductTranslation()
                {
                    ID = 1,
                    ProductID = 1,
                    Name = "Áo sơ mi Nam",
                    LanguageId = 1,
                    SeoAlias = "áo  sơ mi nam",
                    SeoTitle = "Sản phẩm áo  sơ mi thời trang Nam",
                    SeoDescription = "Sản phẩm áo  sơ mi thời trang Nam",
                    Details = "mô tả sản phẩm áo sơ mi",
                    Description = ""
                },
                new ProductTranslation()
                {
                    ID = 2,
                    ProductID = 1,
                    Name = "Men-Shirt",
                    LanguageId = 2,
                    SeoAlias = "Men-Shirt",
                    SeoTitle = "Product Shirt for men",
                    SeoDescription = "Product Shirt for men",
                    Details = "Description of products",
                    Description = ""
                }
            );


            //ProductCategories
            modelBuilder.Entity<ProductCategories>().HasData(
                new ProductCategories()
                {
                    ProductID = 1,
                    ProductCategoryID = 1
                }
            );




            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRoles>().HasData(new AppRoles
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUsers>();
            modelBuilder.Entity<AppUsers>().HasData(new AppUsers
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "letri4612931997@gmail.com",
                NormalizedEmail = "letri4612931997@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abcd1234$"),
                SecurityStamp = string.Empty,
                FirstName = "Tri",
                LastName = "Le",
                Dob = new DateTime(2020, 01, 31)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });

        }
    }
}
