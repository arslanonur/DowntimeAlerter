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

namespace DowntimeAlerter.WebApi.Controllers
{
    [ServiceFilter(typeof(LoginFilterAttribute))]
    public class NotificationLogController : Controller
    {
        private readonly ILogService _logService;
        private readonly IMapper _mapper;
        private readonly INotificationLogsService _notificaitonLogService;


        public NotificationLogController(ILogService logService,
            INotificationLogsService notificaitionLogService, IMapper mapper)
        {
            _notificaitonLogService = notificaitionLogService;
            _logService = logService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var allNotificationLogs = await _notificaitonLogService.GetLogs();
                var mappedNotificationLogs =
                    _mapper.Map<IEnumerable<NotificationLogs>, IEnumerable<NotificationLogDTO>>(allNotificationLogs);
                await _logService.LogInfo("NotificationLog page visited");
                return View(mappedNotificationLogs.OrderByDescending(x => x.Id));
            }
            catch (Exception ex)
            {
                await _logService.LogError(ex.Message, ex.InnerException.Message);
            }

            return View(new NotificationLogDTO());
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<LogDTO>>> GetAllLogs()
        {
            try
            {
                var logs = await _notificaitonLogService.GetLogs();
                var logsDto = _mapper.Map<IEnumerable<NotificationLogs>, IEnumerable<NotificationLogDTO>>(logs)
                    .OrderByDescending(o => o.CheckedDate);
                return Json(new {data = logsDto});
            }
            catch (Exception ex)
            {
                await _logService.LogError(ex.Message, ex.InnerException.Message);
                return Json(new {data = false});
            }
        }
    }
}