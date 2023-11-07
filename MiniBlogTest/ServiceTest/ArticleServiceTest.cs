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

        var newArticle = new Article("Jerry", "Let's code", "c#");
        var articleService = new ArticleService(CreateMockWith2ArticlesAndCanCreate(newArticle).Object);
        var articleCountBeforeAddNewOne = articleService.GetAll().Result.Count;
        var userStore = new UserStore();

        // when
        var addedArticle = articleService.CreateArticle(newArticle).Result;

        // then
        Assert.Equal(newArticle.Title, addedArticle.Title);
        Assert.Equal(newArticle.Content, addedArticle.Content);
        Assert.Equal(newArticle.UserName, addedArticle.UserName);
    }

    private Mock<IArticleRepository> CreateMockWith2ArticlesAndCanCreate(Article article)
    {
        var mock = new Mock<IArticleRepository>();
        mock.Setup(repository => repository.CreateArticle(article)).Returns(Task.FromResult(new Article
            {
                UserName = article.UserName,
                Title = article.Title,
                Content = article.Content,
            }));
        mock.Setup(repository => repository.GetArticles()).Returns(Task.FromResult(new List<Article>
            {
                new Article(null, "Happy new year", "Happy 2021 new year"),
                new Article(null, "Happy Halloween", "Halloween is coming"),
            }));
        return mock;
    }
}
