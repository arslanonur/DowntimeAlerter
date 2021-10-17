using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DowntimeAlerter.Core;
using DowntimeAlerter.Core.Enums;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.Core.Services;

namespace DowntimeAlerter.Services
{
    public class LogService : ILogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task LogError(string exception)
        {
            var log = new Log {
                Exception = exception,
                Level = LogTypes.Error.ToString(),
                Message = string.Empty,
                TimeStamp = DateTime.Now,
                MessageTemplate = string.Empty                
            };

            await CreateLog(log);   
        }
        public async Task LogInfo(string message)
        {
            var log = new Log
            {
                Exception = string.Empty,
                Level = LogTypes.Information.ToString(),
                Message = message,
                TimeStamp = DateTime.Now,
                MessageTemplate = string.Empty
            };

            await CreateLog(log);            
        }    
        
        public async Task<Log> CreateLog(Log log)
        {
            await _unitOfWork.Log.AddAsync(log);
            await _unitOfWork.CommitAsync();
            return log;
        }
        public async Task<Log> GetLog(int id)
        {
            return await _unitOfWork.Log.GetLog(id);
        }

        public async Task<IEnumerable<Log>> GetLogs()
        {
            return await _unitOfWork.Log.GetLogsAsync();
        }

    }
}