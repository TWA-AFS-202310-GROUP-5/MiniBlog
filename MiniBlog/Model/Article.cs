using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MiniBlog.Model
{
    public class Article
    {
        public Article()
        {
        }

        public Article(string userName, string title, string content)
        {
            UserName = userName;
            Title = title;
            Content = content;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public static string CollectionName { get; set; } = "Article";
    }
}