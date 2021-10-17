using DowntimeAlerter.Core.Models;
using DowntimeAlerter.Core.Utilities;
using DowntimeAlerter.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DowntimeAlerter.DataAccess
{
    public class DowntimeAlerterDbContext : DbContext
    {
        public DowntimeAlerterDbContext(DbContextOptions<DowntimeAlerterDbContext> options)
             : base(options)
        {
        }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<NotificationLogs> NotificationLogs { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Logs>().ToTable(nameof(Logs), t => t.ExcludeFromMigrations());

            builder
                .ApplyConfiguration(new SiteConfiguration());

            builder
                .ApplyConfiguration(new NotificationLogsConfiguration());

            builder
                .ApplyConfiguration(new UserConfiguration());

            //example sites 200 and 404
            builder.Entity<Site>().HasData(
                new Site { Id = 1, Name = "Google", Url = "https://google.com", IntervalTime = 40 });

            builder.Entity<Site>().HasData(
                new Site { Id = 2, Name = "Down Site Example", Url = "https://example.org/impolite", IntervalTime = 30 });

            var md5Password = SecurePasswordHasher.CalculateMD5Hash("0606Invicti!");
            builder.Entity<User>().HasData(
                new User { Id = 1, Name = "Onur ARSLAN", UserName = "onurarslan", Password = md5Password }
                );
        }
    }
}
