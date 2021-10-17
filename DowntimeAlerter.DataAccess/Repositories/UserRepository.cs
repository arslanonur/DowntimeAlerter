using System.Linq;
using System.Threading.Tasks;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DowntimeAlerter.DataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DowntimeAlerterDbContext context)
            : base(context)
        {
        }

        private DowntimeAlerterDbContext DowntimeAlerterDbContext => Context as DowntimeAlerterDbContext;

        public async Task<User> GetUserAsync(User user)
        {
            return await DowntimeAlerterDbContext.Users
                .Where(w => w.UserName == user.UserName && w.Password == user.Password).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUserName(User user)
        {
            return await DowntimeAlerterDbContext.Users
                .Where(w => w.UserName == user.UserName).FirstOrDefaultAsync();
        }
    }
}