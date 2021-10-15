using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.Core.Repositories;
using DowntimeAlerter.DataAccess;
using DowntimeAlerter.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DowntimeAlerter.Data.Repositories
{
    public class SiteEmailRepository : Repository<SiteEmail>, ISiteEmailRepository
    {
        public SiteEmailRepository(DowntimeAlerterDbContext context)
            : base(context)
        {
        }

        private DowntimeAlerterDbContext DowntimeAlerterDbContext => Context as DowntimeAlerterDbContext;

        public async Task<IEnumerable<SiteEmail>> GetAllWithSiteAsync()
        {
            return await DowntimeAlerterDbContext.SiteEmails
                .Include(m => m.Site)
                .ToListAsync();
        }

        public async Task<IEnumerable<SiteEmail>> GetAllWithSiteBySiteIdAsync(int siteId)
        {
            return await DowntimeAlerterDbContext.SiteEmails
                .Include(m => m.Site)
                .Where(m => m.SiteId == siteId)
                .ToListAsync();
        }

        public async Task<SiteEmail> GetWithSiteByIdAsync(int id)
        {
            return await DowntimeAlerterDbContext.SiteEmails
                .Include(m => m.Site)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<SiteEmail>> GetAllSitesEmailByEmail(SiteEmail siteEmail)
        {
            return await DowntimeAlerterDbContext.SiteEmails
                .Where(w => w.Email == siteEmail.Email && w.SiteId == siteEmail.SiteId).ToListAsync();
        }
        

    }
}