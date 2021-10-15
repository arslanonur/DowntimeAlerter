using DowntimeAlerter.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Core.Repositories
{
    public interface ISiteRepository : IRepository<Site>
    {
        Task<IEnumerable<Site>> GetAllWithSiteEmailAsync();
        Task<Site> GetWithSiteEmailByIdAsync(int id);
        Task<Site> GetSiteByUrl(string url);
    }
}
