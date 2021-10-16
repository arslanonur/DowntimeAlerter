using System.Collections.Generic;
using System.Threading.Tasks;
using DowntimeAlerter.Core;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.Core.Services;

namespace DowntimeAlerter.Services
{
    public class SiteService : ISiteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SiteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Site> CreateSite(Site newSite)
        {
            await _unitOfWork.Site.AddAsync(newSite);
            await _unitOfWork.CommitAsync();
            return newSite;
        }

        public async Task DeleteSite(Site site)
        {
            _unitOfWork.Site.Remove(site);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Site>> GetAllSites()
        {
            return await _unitOfWork.Site.GetAllAsync();
        }

        public async Task<Site> GetSiteById(int id)
        {
            return await _unitOfWork.Site.GetByIdAsync(id);
        }

        public async Task UpdateSite(Site siteToBeUpdated, Site site)
        {
            siteToBeUpdated.Name = site.Name;
            siteToBeUpdated.IntervalTime = site.IntervalTime;
            siteToBeUpdated.Url = site.Url;
            siteToBeUpdated.Email = site.Email;            
            await _unitOfWork.CommitAsync();
        }

        public async Task<Site> GetSiteByUrl(string site)
        {
            return await _unitOfWork.Site.GetSiteByUrl(site);
        }
    }
}