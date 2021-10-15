using DowntimeAlerter.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Core.Services
{
    public interface ISiteService
    {
        Task<IEnumerable<Site>> GetAllSites();
        Task<Site> GetSiteById(int id);
        Task<Site> CreateSite(Site newSite);
        Task UpdateSite(Site siteToBeUpdated, Site site);
        Task DeleteSite(Site site);
    }
}
