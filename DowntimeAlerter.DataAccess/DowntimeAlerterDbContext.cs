using DowntimeAlerter.Core.Models;
using DowntimeAlerter.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.DataAccess
{
    public class DowntimeAlerterDbContext : DbContext
    {
        public DbSet<Site> Sites { get; set; }
        public DbSet<SiteEmail> SiteEmails { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<NotificationLogs> NotificationLogs { get; set; }

        public DowntimeAlerterDbContext(DbContextOptions<DowntimeAlerterDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SiteConfiguration());
            modelBuilder.ApplyConfiguration(new SiteEmailConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationLogsConfiguration());
        }
    }
}
