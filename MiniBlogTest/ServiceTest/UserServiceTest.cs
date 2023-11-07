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
    }
}
