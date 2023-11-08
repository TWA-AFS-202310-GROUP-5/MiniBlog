using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;
using System;

namespace MiniBlog.Model
{
    public class User
    {
        public User()
        {
        }

        public User(string name, string email = "anonymous@unknow.com")
        {
            Id = Guid.NewGuid().ToString();
            this.Name = name;
            this.Email = email;
        }

        public static string CollectionName { get; set; } = "User";

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string? Id { get; set; } = null!;
        public string Name { get; set; }

        public string Email { get; set; }
    }
}