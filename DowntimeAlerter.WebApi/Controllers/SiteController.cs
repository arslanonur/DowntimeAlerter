using AutoMapper;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.Core.Services;
using DowntimeAlerter.Core.Utilities;
using DowntimeAlerter.WebApi.ActionFilters;
using DowntimeAlerter.WebApi.DTO;
using DowntimeAlerter.WebApi.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DowntimeAlerter.WebApi.Controllers
{
    [ServiceFilter(typeof(LoginFilterAttribute))]
    public class SiteController : Controller
    {
        private readonly ILogService _logService;
        private readonly IMapper _mapper;
        private readonly ISiteService _siteService;

        public SiteController(ILogService logService, IMapper mapper, ISiteService siteService)
        {
            _logService = logService;
            _mapper = mapper;
            _siteService = siteService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                await _logService.LogInfo("Site page visited.");
                var allSites = await _siteService.GetAllSites();
                var allSitesDTO = new List<SiteDTO>();
                foreach (var site in allSites)
                {
                    var mappedSiteDTO = _mapper.Map<Site, SiteDTO>(site);
                    allSitesDTO.Add(mappedSiteDTO);
                }
                return View(allSitesDTO);
            }
            catch (Exception ex)
            {
                await _logService.LogError(ex.Message, ex.InnerException.Message);
            }

            return View(new List<SiteDTO>());
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
                var validator = new SaveSiteResourceValidator();
                var validationResult = await validator.ValidateAsync(site);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                if (!UrlChecker.CheckUrl(site.Url))
                {
                    return Json(new { success = false, msg = "Incorrect Url format." });
                }

                if (site.IntervalTime < 60)
                {
                    return Json(new { success = false, msg = "The Interval Time must be greater or equal 60 seconds." });
                }

                var mappedSite = _mapper.Map<SiteDTO, Site>(site);

                var existSite = await _siteService.GetSiteByUrl(site.Url);
                if (existSite != null)
                {
                    return Json(new { success = false, msg = "The Url as already exist!" });
                }

                if (!EmailChecker.IsValidEmail(site.Email))
                    return Json(new { success = false, msg = "Incorrect email format!" });

                var createdSite = await _siteService.CreateSite(mappedSite);
                if (createdSite != null)
                {
                    await _logService.LogInfo("New site added. Added site name : " + createdSite.Name);
                    return Json(new { success = true, msg = "The site added." });
                }
                else
                {
                    return Json(new { success = false, msg = "Error when adding site!!" });
                }

            }
            catch (Exception ex)
            {
                await _logService.LogError(ex.Message, ex.InnerException.Message);
                return Json(new { success = false, msg = ex.Message });
            }
        }

        public async Task<ActionResult> EditSite(int id)
        {
            try
            {
                var site = await _siteService.GetSiteById(id);
                var mappedSite = _mapper.Map<Site, SiteDTO>(site);
                return View(mappedSite);
            }
            catch (Exception ex)
            {
                await _logService.LogError(ex.Message, ex.InnerException.Message);
                return View(new SiteDTO());
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSite(SiteDTO siteDTO)
        {
            try
            {
                if (siteDTO.Id <= 0)
                    return Json(new { success = true, msg = "Please select a site!" });

                if (!UrlChecker.CheckUrl(siteDTO.Url))
                    return Json(new { data = false, msg = "Incorrect Url format." });

                if (siteDTO.IntervalTime < 60)
                    return Json(new { success = false, msg = "The Interval Time must be greater or equal 60 seconds." });

                var siteToBeUpdated = await _siteService.GetSiteById(siteDTO.Id);
                if (siteToBeUpdated == null)
                    return Json(new { success = false, msg = "Site was not found!" });

                if (siteToBeUpdated != null)
                {
                    var mappedSite = _mapper.Map<SiteDTO, Site>(siteDTO);
                    await _siteService.UpdateSite(siteToBeUpdated, mappedSite);
                    await _logService.LogInfo("Site updated. Updated site name : " + mappedSite.Name);
                    return Json(new { success = false, msg = "The site updated successfuly" });
                }
                else
                {
                    return Json(new { success = false, msg = "Site was not found !" });
                }
            }
            catch (Exception ex)
            {
                await _logService.LogError(ex.Message, ex.InnerException.Message);
                return Json(new { success = false, msg = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteSite(int id)
        {
            try
            {
                var site = await _siteService.GetSiteById(id);
                if (site != null)
                {
                    await _siteService.DeleteSite(site);
                    return Json(new { success = false, msg = "The site deleted successfuly" });
                }

                return Json(new { success = false, msg = "Site was not found !" });
            }
            catch (Exception ex)
            {
                await _logService.LogError(ex.Message, ex.InnerException.Message);
                return Json(new { success = false, msg = ex.Message });
            }
        }
    }
}
