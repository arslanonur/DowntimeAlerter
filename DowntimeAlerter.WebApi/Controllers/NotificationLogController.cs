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
    [ServiceFilter(typeof(LoginFilterAttribute))]
    public class NotificationLogController : Controller
    {
        private readonly ILogger<NotificationLogController> _logger;
        private readonly IMapper _mapper;
        private readonly INotificationLogsService _notificaitonLogService;


        public NotificationLogController(ILogger<NotificationLogController> logger,
            INotificationLogsService notificaitionLogService, IMapper mapper)
        {
            _notificaitonLogService = notificaitionLogService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var allNotificationLogs = await _notificaitonLogService.GetLogs();
            var mappedNotificationLogs = _mapper.Map<IEnumerable<NotificationLogs>, IEnumerable<NotificationLogDTO>>(allNotificationLogs);
            return View(mappedNotificationLogs.OrderByDescending(x=>x.Id));
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<LogsDTO>>> GetAllLogs()
        {
            try
            {
                var logs = await _notificaitonLogService.GetLogs();
                var logsDto = _mapper.Map<IEnumerable<NotificationLogs>, IEnumerable<NotificationLogDTO>>(logs)
                    .OrderByDescending(o => o.CheckedDate);
                return Json(new { data = logsDto });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new { data = false });
            }
        }
    }
}