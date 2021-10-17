using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DowntimeAlerter.WebApi.Models;
using DowntimeAlerter.WebApi.ActionFilters;
using AutoMapper;
using DowntimeAlerter.Core.Services;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.WebApi.DTO;
using DowntimeAlerter.Core.Enums;

namespace DowntimeAlerter.WebApi.Controllers
{
    [ServiceFilter(typeof(LoginFilterAttribute))]
    public class HomeController : Controller
    {
        private readonly ILogService _logService;
        private readonly IMapper _mapper;
        private readonly INotificationLogsService _notificationLogService;
        private readonly ISiteService _siteService;

        public HomeController(ILogService logService, IMapper mapper, INotificationLogsService notificationLogsService, ISiteService siteService)
        {
            _logService = logService;
            _mapper = mapper;
            _notificationLogService = notificationLogsService;
            _siteService = siteService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {                
                await _logService.LogInfo("Entered Home Page");

                var sites = await _siteService.GetAllSites();
                var siteResources = _mapper.Map<IEnumerable<Site>, IEnumerable<SiteDTO>>(sites);
                var notificationLogs = await _notificationLogService.GetLogs();
                var notificationLogsResource = _mapper.Map<IEnumerable<NotificationLogs>, IEnumerable<NotificationLogDTO>>(notificationLogs);
                var downStateCount = notificationLogsResource.Count(x => x.State == StateType.Down.ToString());
                var upStateCount = notificationLogsResource.Count(x => x.State == StateType.Up.ToString());

                ViewBag.SiteCount = siteResources.Count();
                ViewBag.NotificationLogsCount = notificationLogsResource.Count();
                ViewBag.DownStateCount = downStateCount;
                ViewBag.UpStateCount = upStateCount;
            }
            catch (Exception ex)
            {
                await _logService.LogError(ex.Message);
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
