using MiniBlog.Model;
using MiniBlog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniBlog;
using Xunit;
using Newtonsoft.Json;
using MiniBlog.Repositories;
using Moq;
using System.Net.Http;
using System.Net.Mime;
using System.Net;
using MiniBlog.Services;

namespace MiniBlogTest.ServiceTest
{
    public class UserServiceTest : TestBase
    {
        public UserServiceTest(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Should_get_all_users()
        {
            // given
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repository => repository.GetUsersAsync())
                .Returns(Task.FromResult(new List<User>()));

            var client = GetClient(null, null, null, mockUserRepository.Object);
            UserService userService = new UserService(mockUserRepository.Object);
            // when


            var response = await userService.GetAllAsync();

            // then
            Assert.True(response.Count == 0);
        }

        [Fact]
        public async Task Should_register_user_success()
        {
            // given
            var mockUserRepository = new Mock<IUserRepository>();
            var userName = "Tom";
            var email = "a@b.com";
            var user = new User(userName, email);
            mockUserRepository.Setup(repository => repository.GetUsersAsync())
                .Returns(Task.FromResult(new List<User>()));
            mockUserRepository.Setup(repository => repository.CreateUserAsync(user))
                .Returns(Task.FromResult(user));


            UserService userService = new UserService(mockUserRepository.Object);
            // when
            var registerResponse = await userService.CreateUserAsync(user);



            // It fail, please help


            mockUserRepository.Setup(repository => repository.GetUsersAsync())
                .Returns(Task.FromResult(new List<User> { user }));
            var response = await userService.GetAllAsync();
            Assert.True(response.Count == 1);
            Assert.Equal(email, response[0].Email);
            Assert.Equal(userName, response[0].Name);
        }


        [Fact]
        public async void Should_create_article_and_register_user_correct()
        {
            var mockArticleRepository = new Mock<IArticleRepository>();
            var mockUserRepository = new Mock<IUserRepository>();
            string userNameWhoWillAdd = "Tom";
            string articleContent = "What a good day today!";
            string articleTitle = "Good day";
            Article article = new Article(userNameWhoWillAdd, articleTitle, articleContent);
            mockArticleRepository.Setup(repository => repository.GetArticles()).Returns(Task.FromResult(new List<Article>
            {
                article
            }));
            User user = new User("Tom");
            mockArticleRepository.Setup(repository => repository.CreateArticle(article)).Returns(Task.FromResult(article));

            mockUserRepository.Setup(repository => repository.GetUsersAsync())
                .Returns(Task.FromResult(new List<User> { user }));
            mockUserRepository.Setup(repository => repository.CreateUserAsync(user))
                .Returns(Task.FromResult(user));

            UserService userService = new UserService(mockUserRepository.Object);
            var httpContent = JsonConvert.SerializeObject(article);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var createArticleResponse = await userService.CreateUserAsync(user);

            // It fail, please help
            ArticleService articleService = new ArticleService(mockArticleRepository.Object, mockUserRepository.Object);

            var articleResponse = await articleService.GetAll();
            Assert.Equal(1, articleResponse.Count);
            Assert.Equal(articleTitle, articleResponse[0].Title);
            Assert.Equal(articleContent, articleResponse[0].Content);
            Assert.Equal(userNameWhoWillAdd, articleResponse[0].UserName);

            var userResponse = await userService.GetAllAsync();

            Assert.True(userResponse.Count == 1);
            Assert.Equal(userNameWhoWillAdd, userResponse[0].Name);
            Assert.Equal("anonymous@unknow.com", userResponse[0].Email);
        }
    }
}
