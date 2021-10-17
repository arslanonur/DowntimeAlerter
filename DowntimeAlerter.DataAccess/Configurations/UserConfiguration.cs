using DowntimeAlerter.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DowntimeAlerter.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(m => m.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(m => m.Password)
                .IsRequired()
                .HasMaxLength(300);

            builder
                .Property(m => m.Type);


            builder
                .ToTable("Users");
        }
    }
}