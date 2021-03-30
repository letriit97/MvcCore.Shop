using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MvcCore.Data.Entities;

namespace MvcCore.Data.Configurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Languages>
    {
        public void Configure(EntityTypeBuilder<Languages> builder)
        {
            builder.ToTable("Languages");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).IsRequired().IsUnicode(false).HasMaxLength(5);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(20);
        }
    }
}
