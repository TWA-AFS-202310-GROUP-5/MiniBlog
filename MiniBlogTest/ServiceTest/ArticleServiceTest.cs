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
        var userStore = new UserStore();
        int mockedArticlesNum = 2;
        var articleService = new ArticleService(userStore, CreateMockWith2ArticlesAndCanCreate(newArticle).Object);

        // when
        var addedArticle = articleService.CreateArticle(newArticle).Result;

        // then
        Assert.Equal(mockedArticlesNum + 1, articleService.GetAll().Result.Count);
        Assert.Equal(newArticle.Title, addedArticle.Title);
        Assert.Equal(newArticle.Content, addedArticle.Content);
        Assert.Equal(newArticle.UserName, addedArticle.UserName);
    }

    [Fact]
    public void Should_get_all_article_when_invoke_GetAll()
    {
        // given
        var userStore = new UserStore();
        var articles = new List<Article>
            {
                new Article(null, "Happy new year", "Happy 2021 new year"),
                new Article(null, "Happy Halloween", "Halloween is coming"),
            };
        var articleService = new ArticleService(userStore, CreateMockWithGet2Articles().Object);
        var articleCountBeforeAddNewOne = articleService.GetAll().Result.Count;

        // when
        var gotArticles = articleService.GetAll().Result;

        // then
        Assert.Equal(2, gotArticles.Count);
        Assert.Equal(articles[0].Title, gotArticles[0].Title);
        Assert.Equal(articles[0].Content, gotArticles[0].Content);
        Assert.Equal(articles[0].UserName, gotArticles[0].UserName);
        Assert.Equal(articles[1].Title, gotArticles[1].Title);
        Assert.Equal(articles[1].Content, gotArticles[1].Content);
        Assert.Equal(articles[1].UserName, gotArticles[1].UserName);
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
                article,
            }));
        return mock;
    }

    private Mock<IArticleRepository> CreateMockWithGet2Articles()
    {
        var mock = new Mock<IArticleRepository>();
        mock.Setup(repository => repository.GetArticles()).Returns(Task.FromResult(new List<Article>
            {
                new Article(null, "Happy new year", "Happy 2021 new year"),
                new Article(null, "Happy Halloween", "Halloween is coming"),
            }));
        return mock;
    }
}
