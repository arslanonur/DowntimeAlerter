using System.Collections.Generic;
using System.Threading.Tasks;
using DowntimeAlerter.Core.Models;

namespace DowntimeAlerter.Core.Services
{
    public interface INotificationLogsService
    {
        Task<IEnumerable<NotificationLogs>> GetLogs();
        Task<NotificationLogs> CreateNotificationLog(NotificationLogs newNotificationLog);
    }
}