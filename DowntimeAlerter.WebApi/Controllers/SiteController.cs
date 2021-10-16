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
    public class SiteController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISiteService _siteService;

        public SiteController(IMapper mapper, ISiteService siteService)
        {
            _mapper = mapper;
            _siteService = siteService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult AddSite()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateSite(SiteDTO site)
        {
            try
            {
                var mappedSite = _mapper.Map<SiteDTO,Site>(site);
                var createdSite = await _siteService.CreateSite(mappedSite);
                if(createdSite != null)
                {
                    return Json(new { success = true, msg = "The site was added." });
                }           
                else
                {
                    return Json(new { success = false, msg = "Error when adding site!!" });
                }

            }
            catch (Exception ex)
            {

                return Json(new { success = false, msg = ex.Message });
            }
        }
    }
}
