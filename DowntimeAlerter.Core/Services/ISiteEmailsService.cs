using DowntimeAlerter.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Core.Services
{
    public interface ISiteEmailService
    {
        Task<IEnumerable<SiteEmail>> GetAllWithSite();
        Task<SiteEmail> GetSiteEmailById(int id);
        Task<IEnumerable<SiteEmail>> GetSiteEmailsBySiteId(int siteId);
        Task<SiteEmail> CreateSiteEmail(SiteEmail newSiteEmail);
        Task UpdateSiteEmail(SiteEmail siteEmailToBeUpdated, SiteEmail siteEmail);
        Task DeleteSiteEmail(SiteEmail siteEmail);
        Task<IEnumerable<SiteEmail>> GetAllSiteEmailByEmail(SiteEmail siteEmail);
    }
}
