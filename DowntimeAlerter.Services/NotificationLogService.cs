using System.Collections.Generic;
using System.Threading.Tasks;
using DowntimeAlerter.Core;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.Core.Services;

namespace DowntimeAlerter.Services
{
    public class NotificationLogService : INotificationLogsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<NotificationLogs>> GetLogs()
        {
            return await _unitOfWork.NotificationLogs.GetAllNotificationLogsAsync();
        }

        public async Task<NotificationLogs> CreateNotificationLog(NotificationLogs notificationLog)
        {
            await _unitOfWork.NotificationLogs.AddAsync(notificationLog);
            await _unitOfWork.CommitAsync();
            return notificationLog;
        }
    }
}