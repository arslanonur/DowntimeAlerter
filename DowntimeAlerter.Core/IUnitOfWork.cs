using System;
using System.Threading.Tasks;
using DowntimeAlerter.Core.Repositories;

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