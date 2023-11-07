using MiniBlog.Model;
using MongoDB.Driver;

namespace MiniBlog.Repositories
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> userRepository;
        public UserRepository(IMongoClient mongoClient)
        {
            var mongoDB = mongoClient.GetDatabase("MiniBlog");
            userRepository = mongoDB.GetCollection<User>(User.CollectionName);
        }
    }
}
