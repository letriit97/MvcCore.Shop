using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MvcCore.Data.Entities;

namespace MvcCore.Data.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID).UseIdentityColumn();
            builder.Property(x => x.Url).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Captions).HasMaxLength(200).IsRequired(false);

            builder.HasOne(x => x.Products).WithMany(x => x.ProductImages).HasForeignKey(x => x.ProductID);
        }
    }
}
