using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Stores;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlogTest
{
    public static class MockGenerator
    {
        public static Mock<IArticleRepository> MockArticleRepositoryCreateArticle(Article article)
        {
            var mock = new Mock<IArticleRepository>();
            mock.Setup(repository => repository.CreateArticle(article)).Returns(Task.FromResult(article));
            return mock;
        }

        public static Mock<IUserRepository> MockUserRepositoryGetTwoUsers()
        {
            var users = new List<User>
            {
                new User("Andrew", "1@1.com"),
                new User("William", "2@2.com"),
            };
            var mock = new Mock<IUserRepository>();
            mock.Setup(repository => repository.GetUsers()).Returns(Task.FromResult(users));
            return mock;
        }
    }
}
