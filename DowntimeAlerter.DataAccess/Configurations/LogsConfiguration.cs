using DowntimeAlerter.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DowntimeAlerter.DataAccess.Configurations
{
    public class LogsConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

            builder
                .Property(m => m.Level)                
                .HasMaxLength(50);

            builder
                .Property(m => m.MessageTemplate)                
                .HasMaxLength(50);

            builder
                .Property(m => m.Message)                
                .HasMaxLength(500);

            builder
                .Property(m => m.TimeStamp)
                .IsRequired();
            builder
                .ToTable("Logs");
        }
    }
}