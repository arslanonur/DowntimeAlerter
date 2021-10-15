using DowntimeAlerter.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Core.Repositories
{
    public interface ISiteEmailRepository : IRepository<SiteEmail>
    {
        Task<IEnumerable<SiteEmail>> GetAllWithSiteAsync();
        Task<SiteEmail> GetWithSiteByIdAsync(int id);
        Task<IEnumerable<SiteEmail>> GetAllWithSiteBySiteIdAsync(int siteId);
        Task<IEnumerable<SiteEmail>> GetAllSitesEmailByEmail(SiteEmail siteEmails);
    }
}
