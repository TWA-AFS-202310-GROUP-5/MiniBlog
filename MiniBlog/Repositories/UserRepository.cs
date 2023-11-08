using MiniBlog.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using SharpCompress.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

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
            await userCollecotion.InsertOneAsync(user);
            return await userCollecotion.Find(a => a.Name == user.Name).FirstOrDefaultAsync();
        }

        public Task DeleteUserByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUserByName(string name)
        {
            return await userCollecotion.Find(a => a.Name == name).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetUsers()
        {
            return await userCollecotion.Find(a => a.Name != null).ToListAsync();
        }

        public async Task<User> UpdateUser(User user)
        {
            var update = Builders<User>.Update.Set(e => e.Name, user.Name);
            return await userCollecotion.FindOneAndUpdateAsync(a => a.Name == user.Name, update);
        }
    }
}
