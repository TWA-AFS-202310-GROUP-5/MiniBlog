using System.Collections.Generic;
using System.Threading.Tasks;
using MiniBlog.Model;

namespace MiniBlog.Repositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetUsersAsync();
        public Task<User> CreateUserAsync(User user);

        void UpdateOne(User user);

        public void DeleteOne(string name);
        Task<User> GetUserByName(string name);
    }
}
