using MiniBlog.Model;
using MiniBlog.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<User>> GetAllAsync()
        {
            return await userRepository.GetUsersAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await userRepository.CreateUserAsync(user);
        }

        public async Task<bool> IsUserAlreadyExit(string name)
        {
            var users = await userRepository.GetUsersAsync();
            var result = users.FirstOrDefault(us => us.Name == name);
            return result != null;
        }

        public User Update(User user)
        {
            userRepository.UpdateOne(user);
            return user;
        }

        public void DeleteOne(string name)
        {
            userRepository.DeleteOne(name);

        }

        public async Task<User> GetUserByName(string name)
        {
            
            return await userRepository.GetUserByName(name);
        }
    }
}
