using MiniBlog.Model;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> userCollecotion;
        public UserRepository(IMongoClient mongoClient)
        {
            var mongoDB = mongoClient.GetDatabase("MiniBlog");
            userCollecotion = mongoDB.GetCollection<User>(User.CollectionName);
        }

        public async Task<User> CreateUser(User user)
        {
            var existUser = await userCollecotion.FindAsync(u => u.Name == user.Name);
            if (existUser == null)
            {
                await userCollecotion.InsertOneAsync(user);
            }
            
            return await userCollecotion.Find(a => a.Name == user.Name).FirstAsync();
        }

        public Task DeleteUserByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetUserByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<User>> GetUsers()
        {
            throw new System.NotImplementedException();
        }
    }
}
