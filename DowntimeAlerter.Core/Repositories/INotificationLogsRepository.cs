using System.Collections.Generic;
using System.Threading.Tasks;
using DowntimeAlerter.Core.Models;

namespace DowntimeAlerter.Core.Repositories
{
    public interface INotificationLogsRepository : IRepository<NotificationLogs>
    {
        Task<IEnumerable<NotificationLogs>> GetAllNotificationLogsAsync();
    }
}