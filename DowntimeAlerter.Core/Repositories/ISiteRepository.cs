using System.Collections.Generic;
using System.Threading.Tasks;
using DowntimeAlerter.Core.Models;

namespace DowntimeAlerter.Core.Repositories
{
    public interface ISiteRepository : IRepository<Site>
    {
        Task<IEnumerable<Site>> GetAllWithSiteEmailAsync();
        Task<Site> GetWithSiteEmailByIdAsync(int id);
        Task<Site> GetSiteByUrl(string url);
    }
}