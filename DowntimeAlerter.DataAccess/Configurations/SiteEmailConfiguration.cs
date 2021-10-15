using DowntimeAlerter.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DowntimeAlerter.Data.Configurations
{
    public class SiteEmailConfiguration : IEntityTypeConfiguration<SiteEmail>
    {
        public void Configure(EntityTypeBuilder<SiteEmail> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

            builder
                .Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasOne(m => m.Site)
                .WithMany(a => a.SiteEmails)
                .HasForeignKey(m => m.SiteId);

            builder
                .ToTable("SiteEmails");
        }
    }
}