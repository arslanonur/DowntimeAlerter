using DowntimeAlerter.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Core.Repositories
{
    public interface ILogsRepository : IRepository<Logs>
    {
        Task<IEnumerable<Logs>> GetLogsAsync();
        Task<Logs> GetLog(int id);
    }
}
