using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Stores;

namespace MiniBlog.Services;

public class ArticleService
{
    private readonly ArticleStore articleStore = null!;
    private readonly UserStore userStore = null!;
    private readonly IArticleRepository articleRepository = null!;

    public ArticleService(ArticleStore articleStore, UserStore userStore, IArticleRepository articleRepository)
    {
        this.articleStore = articleStore;
        this.userStore = userStore;
        this.articleRepository = articleRepository;
    }

    public async Task<Article?> CreateArticle(Article article)
    {
        userStore.CreateUser(article.UserName);

        return await articleRepository.CreateArticle(article);
    }

    public async Task<List<Article>> GetAll()
    {
        return await articleRepository.GetArticles();
    }

    public async Task<Article?> GetById(Guid id)
    {
        return await articleRepository.GetArticleById(id.ToString());
    }
}
