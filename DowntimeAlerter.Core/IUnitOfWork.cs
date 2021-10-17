using DowntimeAlerter.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ILogRepository Log { get; }
        INotificationLogsRepository NotificationLogs { get; }
        ISiteRepository Site { get; }
        IUserRepository Users { get; }
        Task<int> CommitAsync();
    }
}
