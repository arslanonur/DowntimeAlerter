using DowntimeAlerter.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Core.Services
{
    public interface ILogService
    {
        Task<IEnumerable<Log>> GetLogs();
        Task<Log> GetLog(int id);
        Task LogInfo(string message);
        Task LogError(string exception);
    }
}
