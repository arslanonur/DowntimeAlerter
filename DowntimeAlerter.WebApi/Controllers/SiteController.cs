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
        public async Task<IActionResult> Index()
        {
            var allSites = await _siteService.GetAllSites();
            var allSitesDTO = new List<SiteDTO>();
            foreach (var site in allSites)
            {
                var mappedSiteDTO = _mapper.Map<Site, SiteDTO>(site);
                allSitesDTO.Add(mappedSiteDTO);
            }

            return View(allSitesDTO);
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
                var mappedSite = _mapper.Map<SiteDTO, Site>(site);
                var createdSite = await _siteService.CreateSite(mappedSite);
                if (createdSite != null)
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

        public async Task<ActionResult> EditSite(int id)
        {
            var site = await _siteService.GetSiteById(id);
            var mappedSite = _mapper.Map<Site, SiteDTO>(site);
            return View(mappedSite);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSite(SiteDTO siteDTO)
        {
            try
            {
                var siteToBeUpdated = await _siteService.GetSiteById(siteDTO.Id);
                if (siteToBeUpdated != null)
                {
                    var mappedSite = _mapper.Map<SiteDTO, Site>(siteDTO);
                    await _siteService.UpdateSite(siteToBeUpdated, mappedSite);
                    return Json(new { success = false, msg = "The site updated successfuly" });
                }
                else
                {
                    return Json(new { success = false, msg = "Site was not found !" });
                }


            }
            catch (Exception ex)
            {
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
                return Json(new { success = false, msg = ex.Message });
            }
        }
    }
}
