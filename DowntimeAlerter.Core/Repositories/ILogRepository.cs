using System.Collections.Generic;
using System.Threading.Tasks;
using DowntimeAlerter.Core.Models;

namespace DowntimeAlerter.Core.Repositories
{
    public interface ILogRepository : IRepository<Log>
    {
        Task<IEnumerable<Log>> GetLogsAsync();
        Task<Log> GetLog(int id);
    }
}