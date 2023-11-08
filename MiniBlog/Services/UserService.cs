using MiniBlog.Model;
using MiniBlog.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Services
{
    public class UserService
    {
        private readonly IArticleRepository articleRepository = null!;
        private readonly IUserRepository userRepository = null!;

        public UserService(IArticleRepository articleRepository, IUserRepository userRepository)
        {
            this.articleRepository = articleRepository;
            this.userRepository = userRepository;
        }

        public async Task<User?> CreateUser(User user)
        {
            if (await userRepository.GetByName(user.Name) == null)
            {
                await userRepository.Create(new User(user.Name));
            }

            return await this.userRepository.Create(user);
        }

        public async Task<User?> GetByName(string name)
        {
            return await userRepository.GetByName(name);
        }

        public async Task<List<User>> GetAll()
        {
            return await userRepository.GetUsers();
        }
    }
}
