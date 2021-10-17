using System.Collections.Generic;
using System.Threading.Tasks;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DowntimeAlerter.DataAccess.Repositories
{
    public class LogRepository : Repository<Log>, ILogRepository
    {
        public LogRepository(DowntimeAlerterDbContext context)
            : base(context)
        { }

        private DowntimeAlerterDbContext DowntimeAlerterDbContext => Context as DowntimeAlerterDbContext;

        public async Task<IEnumerable<Log>> GetLogsAsync()
        {
            return await DowntimeAlerterDbContext.Logs.ToListAsync();
        }

        public async Task<Log> GetLog(int id)
        {
            return await DowntimeAlerterDbContext.Logs
                .SingleOrDefaultAsync(m => m.Id == id);
        }
    }
}