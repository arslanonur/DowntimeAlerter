using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DowntimeAlerter.DataAccess.Repositories
{
    public class SiteRepository : Repository<Site>, ISiteRepository
    {
        public SiteRepository(DowntimeAlerterDbContext context)
            : base(context)
        {
        }

        private DowntimeAlerterDbContext DowntimeAlerterDbContext => Context as DowntimeAlerterDbContext;

        public async Task<IEnumerable<Site>> GetAllWithSiteEmailAsync()
        {
            return await DowntimeAlerterDbContext.Sites
                .Include(a => a.Email)
                .ToListAsync();
        }

        public async Task<Site> GetWithSiteEmailByIdAsync(int id)
        {
            return await DowntimeAlerterDbContext.Sites
                .Include(a => a.Email)
                .SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Site> GetSiteByUrl(string url)
        {
            return await DowntimeAlerterDbContext.Sites.Where(x => x.Url == url).FirstOrDefaultAsync();
        }
    }
}