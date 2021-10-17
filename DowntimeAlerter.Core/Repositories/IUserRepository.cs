using System.Threading.Tasks;
using DowntimeAlerter.Core.Models;

namespace DowntimeAlerter.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserAsync(User user);
        Task<User> GetUserByUserName(User user);

    }
}