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

            var client = GetClient(new ArticleStore(), new UserStore(new List<User>()), null, mockUserRepository.Object);

            // when
            var response = await client.GetAsync("/user");

            // then
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(body);
            Assert.True(users.Count == 0);
        }
    }
}
