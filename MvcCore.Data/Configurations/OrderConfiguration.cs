using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MvcCore.Data.Entities;

namespace MvcCore.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //Tên Bảng sau khi Generate thành Database
            builder.ToTable("Orders");
            //Khóa chính
            builder.HasKey(x => x.ID);
            //Khóa ngoại và Liên kết khóa ngoại
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.Property(x => x.CreateTime);
            builder.Property(x => x.ShipEmail).IsRequired().IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.ShipAddress).IsRequired().HasMaxLength(200);
            builder.Property(x => x.ShipName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.ShipMobile).IsRequired().HasMaxLength(200);
            builder.HasOne(x => x.AppUsers).WithMany(x => x.Orders).HasForeignKey(x => x.UserID);

        }
    }
}
