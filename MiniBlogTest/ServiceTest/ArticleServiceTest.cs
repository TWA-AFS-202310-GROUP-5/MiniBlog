using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Services;
using MiniBlog.Stores;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MiniBlogTest.ServiceTest;

public class ArticleServiceTest
{
    [Fact]
    public void Should_create_article_when_invoke_CreateArticle_given_input_article()
    {
        // given
        int articleCountBeforeAddNewOne = 0;
        var newArticle = new Article("Jerry", "Let's code", "c#");
        var userStore = new UserStore();
        var mock = new Mock<IArticleRepository>();
        mock.Setup(repository => repository.CreateArticle(newArticle)).Returns(Task.FromResult(new Article("Jerry", "Let's code", "c#")));
        mock.Setup(repository => repository.GetArticles()).Returns(Task.FromResult(new List<Article>
        {
            newArticle,
        }));
        ArticleService articleService = new ArticleService(new ArticleStore(new List<Article>()), userStore, mock.Object);

        // when
        var addedArticle = articleService.CreateArticle(newArticle).Result;

        // then
        Assert.Equal(articleCountBeforeAddNewOne + 1, articleService.GetAll().Result.Count);
        Assert.Equal(newArticle.Title, addedArticle.Title);
        Assert.Equal(newArticle.Content, addedArticle.Content);
        Assert.Equal(newArticle.UserName, addedArticle.UserName);
    }
}
