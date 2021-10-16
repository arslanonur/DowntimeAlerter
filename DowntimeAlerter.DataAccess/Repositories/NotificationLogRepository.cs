using System.Collections.Generic;
using System.Threading.Tasks;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.Core.Repositories;
using DowntimeAlerter.DataAccess;
using DowntimeAlerter.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DowntimeAlerter.DataAccess.Repositories
{
    internal class NotificationLogRepository : Repository<NotificationLogs>, INotificationLogsRepository
    {
        public NotificationLogRepository(DowntimeAlerterDbContext context)
             : base(context)
        {
        }

        private DowntimeAlerterDbContext DowntimeAlerterDbContext => Context as DowntimeAlerterDbContext;

        public async Task<IEnumerable<NotificationLogs>> GetAllNotificationLogsAsync()
        {
            return await DowntimeAlerterDbContext.NotificationLogs.ToListAsync();
        }
    }
}