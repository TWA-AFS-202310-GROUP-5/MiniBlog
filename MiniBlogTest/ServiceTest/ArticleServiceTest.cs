using MiniBlog.Model;
using MiniBlog.Services;
using MiniBlog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MiniBlogTest.ServiceTest
{
    public class ArticleServiceTest
    {
        [Fact]
        public async Task Should_create_article_when_invoke_CreateArticle_given_input_articleAsync()
        {
            var newArticle = new Article("alice", "how to cook", "cooooooking");
            var articleStore = new ArticleStore();
            var userStore = new UserStore();
            var articleService = new ArticleService(articleStore, userStore);
            var articleCountBefore = articleStore.Articles.Count;

            var addedArticle = await articleService.CreateNewArticleAsync(newArticle);

            Assert.Equal(newArticle, addedArticle);
            Assert.Equal(articleCountBefore + 1, articleStore.Articles.Count());
        }

        [Fact]
        public async Task Should_find_correct_article_when_invoke_FindArticleById_given_input_idAsync()
        {
            var newArticle = new Article("alice", "how to cook", "cooooooking");
            var articleStore = new ArticleStore();
            var userStore = new UserStore();
            var articleService = new ArticleService(articleStore, userStore);

            var addedArticle = await articleService.CreateNewArticleAsync(newArticle);
            var foundArticle = articleService.FindArticleById(newArticle.Id);

            Assert.Equal(addedArticle.Id, foundArticle.Id);
            Assert.Equal(addedArticle.Title, foundArticle.Title);
        }
    }
}
