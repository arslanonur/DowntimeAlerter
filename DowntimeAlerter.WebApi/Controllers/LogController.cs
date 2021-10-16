using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.Core.Services;
using DowntimeAlerter.WebApi.ActionFilters;
using DowntimeAlerter.WebApi.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DowntimeAlerter.WebApi.Controllers
{    
    public class LogController : Controller
    {
        private readonly ILogger<LogController> _logger;
        private readonly ILogsService _logService;
        private readonly IMapper _mapper;

        public LogController(ILogger<LogController> logger, ILogsService logService, IMapper mapper)
        {
            _logService = logService;
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<LogsDTO>>> GetAllLogs()
        {
            try
            {
                var logs = await _logService.GetLogs();
                var logsDto = _mapper.Map<IEnumerable<Logs>, IEnumerable<LogsDTO>>(logs)
                    .OrderByDescending(o => o.TimeStamp).Take(1000);
                return Json(new {data = logsDto});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new {data = false});
            }
        }

        [HttpGet]
        public async Task<IActionResult> LogDetails(int id)
        {
            try
            {
                var log = await _logService.GetLog(id);
                var logDTO = _mapper.Map<Logs, LogsDTO>(log);
                return View(logDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}