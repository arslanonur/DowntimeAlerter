using DowntimeAlerter.Core.Enums;
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
        public DbSet<Log> Logs { get; set; }
        public DbSet<NotificationLogs> NotificationLogs { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Log>().ToTable(nameof(Logs), t => t.ExcludeFromMigrations());

            //builder
            //    .ApplyConfiguration(new LogsConfiguration());

            builder
                .ApplyConfiguration(new SiteConfiguration());

            builder
                .ApplyConfiguration(new NotificationLogsConfiguration());

            builder
                .ApplyConfiguration(new UserConfiguration());

            //example sites 200 and 404
            builder.Entity<Site>().HasData(
                new Site
                {
                    Id = 1, Name = "Google", Url = "https://google.com", IntervalTime = 40,
                    Email = "onur.arrslan@gmail.com"
                });

            builder.Entity<Site>().HasData(
                new Site
                {
                    Id = 2, Name = "Down Site Example", Url = "https://example.org/impolite", IntervalTime = 30,
                    Email = "onur.arrslan@gmail.com"
                });

            var md5AdminPassword = SecurePasswordHasher.CalculateMD5Hash("0606Invicti!");
            builder.Entity<User>().HasData(
                new User
                {
                    Id = 1, Name = "Onur ARSLAN", UserName = "onurarslan", Password = md5AdminPassword,
                    Type = UserType.Admin.GetHashCode()
                }
            );
            var md5UserPassword = SecurePasswordHasher.CalculateMD5Hash("6060User!");
            builder.Entity<User>().HasData(
                new User
                {
                    Id = 2, Name = "User STANDART", UserName = "user", Password = md5UserPassword,
                    Type = UserType.Standart.GetHashCode()
                }
            );
        }
    }
}