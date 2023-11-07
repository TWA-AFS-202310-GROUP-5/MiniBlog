using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Stores;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Services
{
    public class UserService
    {
        private readonly IUserRepository userRepository = null!;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<List<User>> GetAll()
        {
            return await userRepository.GetUsers();
        }

    }
}
