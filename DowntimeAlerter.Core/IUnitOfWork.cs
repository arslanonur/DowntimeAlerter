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
        ILogsRepository Logs { get; }
        INotificationLogsRepository NotificationLogs { get; }
        ISiteEmailRepository SiteEmail { get; }
        ISiteRepository Site { get; }

        Task<int> CommitAsync();
    }
}
