using System;
using System.Collections.Generic;
using MiniBlog.Model;

namespace MiniBlog.Stores
{
    public class UserStore
    {
        public UserStore()
        {
            Users = new List<User>
            {
                new User("Andrew", "1@1.com"),
                new User("William", "2@2.com"),
            };
        }

        public UserStore(List<User> users)
        {
            Users = users;
        }

        public List<User> Users { get; set; }

        public User? CreateUser(string username)
        {           
            
            if (Users.Find(_ => _.Name == username) != null)
            {
                return null;
            }

            User user = new User(username);
            Users.Add(user);
            return user;
        }
    }
}
