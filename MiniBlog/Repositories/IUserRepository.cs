using MiniBlog.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Repositories
{
    public interface IUserRepository
    {
        public Task<User> CreateUser(User user);

        public Task<User?> GetUserByName(string username);

        public Task<List<User>> GetUsers();
    }
}
