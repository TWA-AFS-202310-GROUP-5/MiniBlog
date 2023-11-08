using Microsoft.AspNetCore.Mvc;
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
        var newUser = new User
        {
            Name = newArticle.UserName
        };
        var articleRepoMock = MockGenerator.MockArticleRepositoryCreateArticle(newArticle);
        var userRepoMock = MockGenerator.MockUserRepositoryGetTwoUsers();
        userRepoMock.Setup(repository => repository.CreateUser(newUser)).Returns(Task.FromResult(newUser));
        userRepoMock.Setup(repository => repository.GetUserByName(newUser.Name)).Returns(Task.FromResult((User)null));
        var articleService = new ArticleService(articleRepoMock.Object, userRepoMock.Object);

        // when
        var addedArticle = articleService.CreateArticle(newArticle);

        // then
        articleRepoMock.Verify(repo => repo.CreateArticle(newArticle), Times.Once());
        userRepoMock.Verify(repo => repo.CreateUser(newUser), Times.Once());
    }

    [Fact]
    public void Should_create_only_article_when_invoke_CreateArticle_given_article_with_exist_author()
    {
        // given
        var newArticle = new Article("Andrew", "Let's code", "c#");
        var newUser = new User
        {
            Name = newArticle.UserName
        };
        var articleRepoMock = MockGenerator.MockArticleRepositoryCreateArticle(newArticle);
        var userRepoMock = MockGenerator.MockUserRepositoryGetTwoUsers();
        userRepoMock.Setup(repository => repository.CreateUser(newUser)).Returns(Task.FromResult(newUser));
        userRepoMock.Setup(repository => repository.GetUserByName(newUser.Name)).Returns(Task.FromResult((User)null));
        var articleService = new ArticleService(articleRepoMock.Object, userRepoMock.Object);

        // when
        var addedArticle = articleService.CreateArticle(newArticle);

        // then
        articleRepoMock.Verify(repo => repo.CreateArticle(newArticle), Times.Once());
        userRepoMock.Verify(repo => repo.CreateUser(newUser), Times.Never);
    }
}
