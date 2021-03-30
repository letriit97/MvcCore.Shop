using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MvcCore.Data.Entities;

namespace MvcCore.Data.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategories>
    {
        public void Configure(EntityTypeBuilder<ProductCategories> builder)
        {
            builder.HasKey(t => new { t.ProductCategoryID, t.ProductID });
            //Tên bảng
            builder.ToTable("ProductCategories");

            //Thiết lập Khóa Chính
            builder.HasKey(x => new { x.ProductCategoryID, x.ProductID });

            //Liên kết khóa ngoại
            builder.HasOne(x => x.Product).WithMany(p => p.ProductCategories).HasForeignKey(x => x.ProductID);
            builder.HasOne(x => x.Category).WithMany(p => p.ProductCategories).HasForeignKey(x => x.ProductCategoryID);


        }
    }
}
