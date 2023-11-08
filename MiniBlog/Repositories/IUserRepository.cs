﻿using MiniBlog.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Repositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetUsers();
        public Task<User> CreateUser(User user);
        public Task<User> GetUserByName(string name);
        public Task DeleteUserByName(string name);
        Task<User> UpdateUser(User user);
    }
}
