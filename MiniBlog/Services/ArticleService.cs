using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlog.Services
{
    public class ArticleService
    {
        private readonly ArticleStore articleStore = null!;
        private readonly UserStore userStore = null!;
        private readonly ArticleRepository articleRepository = null!;

        public ArticleService(ArticleStore articleStore, UserStore userStore)
        {
            this.articleStore = articleStore;
            this.userStore = userStore;
        }

        public ArticleService(ArticleStore articleStore, UserStore userStore, ArticleRepository articleRepository)
        {
            this.articleStore = articleStore;
            this.userStore = userStore;
            this.articleRepository = articleRepository;
        }

        public async Task<Article> CreateNewArticleAsync(Article article)
        {
            if (article.UserName != null)
            {
                if (!userStore.Users.Exists(_ => article.UserName == _.Name))
                {
                    userStore.Users.Add(new User(article.UserName));
                }

                var createdArticle = await this.articleRepository.CreateArticle(article);
                return createdArticle;
            }

            return null;
        }

        public Article FindArticleById(string id)
        {
            return articleStore.Articles.FirstOrDefault(article => article.Id == id);
        }

        public async Task<List<Article>> GetAll()
        {
            return await articleRepository.GetAll();
        }
    }
}
