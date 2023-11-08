using MiniBlog.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Repositories
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<List<User>> GetUsers();
        Task<User> GetByName(string name);
    }
}