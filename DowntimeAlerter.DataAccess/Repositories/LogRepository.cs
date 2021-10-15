using System.Collections.Generic;
using System.Threading.Tasks;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.Core.Repositories;
using DowntimeAlerter.DataAccess;
using DowntimeAlerter.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DowntimeAlerter.Data.Repositories
{
    public class LogRepository : Repository<Logs>, ILogsRepository
    {
        public LogRepository(DowntimeAlerterDbContext context)
            : base(context)
        { }

        private DowntimeAlerterDbContext DowntimeAlerterDbContext => Context as DowntimeAlerterDbContext;

        public async Task<IEnumerable<Logs>> GetLogsAsync()
        {
            return await DowntimeAlerterDbContext.Logs.ToListAsync();
        }

        public async Task<Logs> GetLog(int id)
        {
            return await DowntimeAlerterDbContext.Logs
                .SingleOrDefaultAsync(m => m.Id == id);
        }
    }
}