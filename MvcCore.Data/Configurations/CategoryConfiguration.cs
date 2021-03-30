using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MvcCore.Data.Entities;
using MvcCore.Data.Enum;

namespace MvcCore.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //Tên bảng và Key
            builder.ToTable("Category");
            builder.HasKey(x => x.ID);

            //Những trường bắt buộc
            builder.Property(x => x.ParentID).IsRequired();
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(Status.Active);
        }
    }
}
