using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Services;
using MiniBlog.Stores;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MiniBlogTest.ServiceTest;

public class ArticleServiceTest
{
    [Fact]
    public void Should_create_article_and_user_when_invoke_CreateArticle_given_article()
    {
        // given
        var newArticle = new Article("Jerry", "Let's code", "c#");
        var userStore = new UserStore();
        var mock = new Mock<IArticleRepository>();
        mock.Setup(repository => repository.CreateArticle(newArticle)).Returns(Task.FromResult(newArticle));
        var articleService = new ArticleService(mock.Object, userStore);

        // when
        var addedArticle = articleService.CreateArticle(newArticle);

        // then
        mock.Verify(repo => repo.CreateArticle(newArticle), Times.Once());
        Assert.Equal(3, userStore.Users.Count);
    }

    [Fact]
    public void Should_create_only_article_when_invoke_CreateArticle_given_article_with_exist_author()
    {
        // given
        var newArticle = new Article("Andrew", "Let's code", "c#");
        var userStore = new UserStore();
        var mock = new Mock<IArticleRepository>();
        mock.Setup(repository => repository.CreateArticle(newArticle)).Returns(Task.FromResult(newArticle));
        var articleService = new ArticleService(mock.Object, userStore);

        // when
        var addedArticle = articleService.CreateArticle(newArticle);

        // then
        mock.Verify(repo => repo.CreateArticle(newArticle), Times.Once());
        Assert.Equal(2, userStore.Users.Count);
    }
}
