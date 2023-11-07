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

            // when
            var response = await client.GetAsync("/user");

            // then
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(body);
            Assert.True(users.Count == 0);
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
            var client = GetClient(null, null, null, mockUserRepository.Object);

            var userJson = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(userJson, Encoding.UTF8, MediaTypeNames.Application.Json);

            // when
            var registerResponse = await client.PostAsync("/user", content);

            // It fail, please help
            Assert.Equal(HttpStatusCode.OK, registerResponse.StatusCode);

            mockUserRepository.Setup(repository => repository.GetUsersAsync())
                .Returns(Task.FromResult(new List<User> { user }));
            var response = await client.GetAsync("/user");
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(body);
            Assert.True(users.Count == 1);
            Assert.Equal(email, users[0].Email);
            Assert.Equal(userName, users[0].Name);
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

            var client = GetClient(null,null,  mockArticleRepository.Object, mockUserRepository.Object);

            var httpContent = JsonConvert.SerializeObject(article);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var createArticleResponse = await client.PostAsync("/article", content);

            // It fail, please help
            Assert.Equal(HttpStatusCode.Created, createArticleResponse.StatusCode);

            var articleResponse = await client.GetAsync("/article");
            var body = await articleResponse.Content.ReadAsStringAsync();
            var articles = JsonConvert.DeserializeObject<List<Article>>(body);
            Assert.Equal(1, articles.Count);
            Assert.Equal(articleTitle, articles[0].Title);
            Assert.Equal(articleContent, articles[0].Content);
            Assert.Equal(userNameWhoWillAdd, articles[0].UserName);

            var userResponse = await client.GetAsync("/user");
            var usersJson = await userResponse.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(usersJson);

            Assert.True(users.Count == 1);
            Assert.Equal(userNameWhoWillAdd, users[0].Name);
            Assert.Equal("anonymous@unknow.com", users[0].Email);
        }
    }
}
