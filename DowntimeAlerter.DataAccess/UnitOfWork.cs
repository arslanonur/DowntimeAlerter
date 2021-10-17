using System.Threading.Tasks;
using DowntimeAlerter.Core;
using DowntimeAlerter.Core.Repositories;
using DowntimeAlerter.DataAccess.Repositories;
using DowntimeAlerter.DataAccess;

namespace DowntimeAlerter.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DowntimeAlerterDbContext _context;
        private LogRepository _logRepository;
        private NotificationLogRepository _notificaitonLogRepository;
        private SiteRepository _siteRepository;
        private UserRepository _userRepository;

        public UnitOfWork(DowntimeAlerterDbContext context)
        {
            _context = context;
        }

        public ISiteRepository Site => _siteRepository ??= new SiteRepository(_context);       

        public ILogRepository Log => _logRepository ??= new LogRepository(_context);

        public INotificationLogsRepository NotificationLogs => _notificaitonLogRepository ??= new NotificationLogRepository(_context);

        public IUserRepository Users => _userRepository ??= new UserRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}