using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MiniBlog.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MiniBlog.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> userCollection;

        public UserRepository(IMongoClient mongoClient)
        {
            var mongoDatabase = mongoClient.GetDatabase("MiniBlog");

            userCollection = mongoDatabase.GetCollection<User>(User.CollectionName);
        }

        public async Task<List<User>> GetUsersAsync() =>
            await userCollection.Find(_ => true).ToListAsync();

        public async Task<User> CreateUserAsync(User user)
        {
            await userCollection.InsertOneAsync(user);
            return await userCollection.Find(a => a.Name == user.Name).FirstAsync();
        }

        public void UpdateOne(User user)
        {
            var filter = Builders<User>.Filter.Eq("Name", user.Name);
            var update = Builders<User>.Update.Set("Email", user.Email);
            userCollection.UpdateOne(filter, update);
        }

        public void DeleteOne(string name)
        {
            var filter = Builders<User>.Filter.Eq("Name", name);
            userCollection.DeleteOne(filter);
        }

        public async Task<User> GetUserByName(string name)
        {
            var filter = Builders<User>.Filter.Eq("Name", name);
            return await userCollection.Find(filter).FirstAsync();
        }
    }
}
