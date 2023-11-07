using System.Net.Http;
using MiniBlog;
using MiniBlog.Stores;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using MiniBlog.Services;

namespace MiniBlogTest
{
    public class TestBase : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public TestBase(CustomWebApplicationFactory<Startup> factory)
        {
            this.Factory = factory;
        }

        protected CustomWebApplicationFactory<Startup> Factory { get; }

        protected HttpClient GetClient(ArticleStore articleRepository, UserStore userRepository)
        {
            return Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(
                    services =>
                    {
                        services.AddSingleton<ArticleStore>(provider =>
                        {
                            return articleRepository;
                        });
                        services.AddSingleton<UserStore>(provider =>
                        {
                            return userRepository;
                        });
                        services.AddScoped<ArticleService>();
                    });
            }).CreateDefaultClient();
        }
    }
}