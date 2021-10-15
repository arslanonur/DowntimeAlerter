using DowntimeAlerter.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DowntimeAlerter.Data.Configurations
{
    public class SiteConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(80);
            builder
                .Property(m => m.IntervalTime)
                .IsRequired();
            builder
                .Property(m => m.Url)
                .IsRequired()
                .HasMaxLength(2048);     

            builder
                .ToTable("Site");
        }
    }
}