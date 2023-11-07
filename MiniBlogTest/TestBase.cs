﻿using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using MiniBlog;
using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Services;
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

        protected HttpClient GetClient(UserStore userStore = null, IArticleRepository articleRepository = null)
        {
            return Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(
                    services =>
                    {
                        services.AddSingleton<UserStore>(provider =>
                        {
                            return userStore;
                        });
                        services.AddScoped<ArticleService>();
                        services.AddScoped<IArticleRepository>(provider =>
                        {
                            return articleRepository;
                        });
                    });
            }).CreateDefaultClient();
        }
    }
}
