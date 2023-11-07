using MiniBlog.Model;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Repositories
{
    public class ArticleRepository
    {
        private readonly IMongoCollection<Article> articleCollection;
        public ArticleRepository(IMongoClient mongoClient) 
        {
            var mongoDB = mongoClient.GetDatabase("MiniBlog");
            articleCollection = mongoDB.GetCollection<Article>(Article.CollectionName);
        }

        public async Task<List<Article>> GetAll()
        {
            return await articleCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Article> CreateArticle(Article article)
        {
            var newArticle = new Article(article.UserName, article.Title, article.Content);
            await articleCollection.InsertOneAsync(newArticle);
            return await articleCollection.Find(x => x.UserName == article.UserName).FirstAsync();
        }
    }
}
