using AutoMapper;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.Core.Services;
using DowntimeAlerter.WebApi.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DowntimeAlerter.WebApi.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogService _logService;
        private readonly IMapper _mapper;

        public LogController(ILogService logService, IMapper mapper)
        {
            _logService = logService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                await _logService.LogInfo("Application Logs page visited");
                var allUser = await _logService.GetLogs();
                var mappedAllUserDTO = _mapper.Map<IEnumerable<Log>, IEnumerable<LogDTO>>(allUser).OrderByDescending(x=>x.Id);

                return View(mappedAllUserDTO);
            }
            catch (Exception ex)
            {
                await _logService.LogError(ex.Message, ex.InnerException.Message);
            }

            return View(new UserDTO());
        }
    }
}
