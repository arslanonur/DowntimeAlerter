using System.Collections.Generic;
using System.Threading.Tasks;
using DowntimeAlerter.Core.Models;

namespace DowntimeAlerter.Core.Services
{
    public interface ILogService
    {
        Task<IEnumerable<Log>> GetLogs();
        Task<Log> GetLog(int id);
        Task LogInfo(string message);
        Task LogError(string exception, string message);
    }
}