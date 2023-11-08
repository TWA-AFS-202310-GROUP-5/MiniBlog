using MiniBlog.Model;
using MiniBlog.Repositories;
using System;
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

        public async Task<User> CreateUser(User user)
        {
            return await userRepository.CreateUser(user);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await userRepository.GetUsers();
        }

        public async Task DeleteUserByName(string name)
        {
            await userRepository.DeleteUserByName(name);
        }

        public async Task<User> GetUserByName(string name)
        {
            return await userRepository.GetUserByName(name);
        }

        public async Task<User> UpdateUser(User user)
        {
            return await userRepository.UpdateUser(user);
        }
    }
}
