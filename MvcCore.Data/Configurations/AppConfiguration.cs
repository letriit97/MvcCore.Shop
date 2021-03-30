using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MvcCore.Data.Entities;

namespace MvcCore.Data.Configurations
{
    public class AppConfiguration : IEntityTypeConfiguration<AppConfigration>
    {
        public void Configure(EntityTypeBuilder<AppConfigration> builder)
        {
            builder.ToTable("AppConfigration");
            builder.HasKey(x => x.Key);
            builder.Property(x => x.Value).IsRequired();
        }
    }
}
