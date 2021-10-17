using System.Collections.Generic;
using System.Threading.Tasks;
using DowntimeAlerter.Core.Models;

namespace DowntimeAlerter.Core.Services
{
    public interface IUserService
    {
        Task<User> GetUserAsync(User user);

        Task<IEnumerable<User>> GetAllUsers();
        Task<User> CreateUser(User newUser);
        Task<User> GetUserById(int id);
        Task<User> GetUserByUserName(User user);
    }
}