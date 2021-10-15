using System.Collections.Generic;
using System.Threading.Tasks;
using DowntimeAlerter.Core;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.Core.Services;

namespace DowntimeAlerter.Services
{
    public class LogService : ILogsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Logs> GetLog(int id)
        {
            return await _unitOfWork.Logs.GetLog(id);
        }

        public async Task<IEnumerable<Logs>> GetAllLogs()
        {
            return await _unitOfWork.Logs.GetLogsAsync();
        }
    }
}